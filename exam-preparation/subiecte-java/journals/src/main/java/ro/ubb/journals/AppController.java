package ro.ubb.journals;

import com.sun.xml.bind.v2.schemagen.xmlschema.Particle;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import ro.ubb.journals.model.Article;
import ro.ubb.journals.model.Journal;
import ro.ubb.journals.repository.ArticleRepository;
import ro.ubb.journals.repository.JournalRepository;

import javax.servlet.http.HttpSession;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;
import java.util.stream.Collectors;

@Controller
public class AppController {

    @Autowired
    private ArticleRepository articleRepository;

    @Autowired
    private JournalRepository journalRepository;

    int length;

    @RequestMapping("/")
    public String showLoginPage() {
        return "login";
    }

    @PostMapping("/login")
    public String login(@RequestParam String username, HttpSession session) {
        System.out.println(username);
        session.setAttribute("userName", username);

        length = articleRepository.findAll().size();

        return "home";
    }

    @GetMapping("/articles/{name}")
    public ResponseEntity<List<Article>> getArticles(@PathVariable String name, HttpSession session) {
        System.out.println(name);
        String userName = (String) session.getAttribute("userName");
        System.out.println(userName);
        var journalId = journalRepository.findByName(name).getId();

        List<Article> collect = articleRepository.findAll()
                .stream()
                .filter(article -> article.getJournalId() == journalId)
                .filter(article -> article.getUserName().equals(userName))
                .collect(Collectors.toList());
        System.out.println(collect);

        return new ResponseEntity<>(collect, HttpStatus.OK);
    }

    public int getCurrentDay() {
        Calendar calendar = Calendar.getInstance(TimeZone.getDefault());
        //getTime() returns the current date in default time zone
        Date date = calendar.getTime();
        return calendar.get(Calendar.DATE);
    }

    @PostMapping("/articles")
    public void addArticle(@RequestParam String name,
                           @RequestParam String summary,
                           HttpSession session) {
        System.out.println(name);
        Journal byName = journalRepository.findByName(name);
        Long journalId;
        if(byName == null) {
            Journal journal = new Journal(name);
            Journal save = journalRepository.save(journal);
            journalId = save.getId();
        }
        else {
            journalId = byName.getId();
        }
        int journalIdInt = Math.toIntExact(journalId);
        System.out.println(summary);
        String userName = (String) session.getAttribute("userName");
        System.out.println(userName);

        Article article = new Article(userName, journalIdInt, summary, getCurrentDay());
        System.out.println(article);
        articleRepository.save(article);
    }

    @GetMapping("/new")
    public ResponseEntity<Boolean> wasNewlyAdded(HttpSession session) {
        int localLength = articleRepository.findAll().size();
        if(localLength > length) {
            length = localLength;
            String userName = (String) session.getAttribute("userName");
            var article = articleRepository.findAll()
                    .stream()
                    .sorted((art1, art2) -> art2.getId().compareTo(art1.getId()))
                    .findFirst()
                    .orElseThrow();
            System.out.println(article);
            if (!article.getUserName().equals(userName)) {
                return new ResponseEntity<>(true, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(false, HttpStatus.OK);
            }
        }
        return new ResponseEntity<>(false, HttpStatus.OK);
    }


}
