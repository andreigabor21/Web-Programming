package ro.ubb.authors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import ro.ubb.authors.model.Author;
import ro.ubb.authors.model.Document;
import ro.ubb.authors.model.Movie;
import ro.ubb.authors.repository.AuthorRepository;
import ro.ubb.authors.repository.DocumentRepository;
import ro.ubb.authors.repository.MovieRepository;

import javax.print.Doc;
import javax.servlet.http.HttpSession;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Controller
public class RestController {

    @Autowired
    private AuthorRepository authorRepository;

    @Autowired
    private DocumentRepository documentRepository;

    @Autowired
    private MovieRepository movieRepository;

    @RequestMapping("/")
    public String showLoginPage() {
        return "login";
    }

    @PostMapping("/login")
    public String login(@RequestParam String username, HttpSession session) {
        System.out.println(username);
        session.setAttribute("userName", username);

        return "home";
    }

    @PostMapping("/add")
    public ResponseEntity<?> add(@RequestParam String name,
                                 @RequestParam String contents,
                                 HttpSession session) {
        System.out.println("Sent:" + name + " " + contents);
        Document build = Document.builder()
                .name(name)
                .contents(contents)
                .build();
        documentRepository.save(build);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @GetMapping("/docs")
    public ResponseEntity<List<Document>> getDocuments(HttpSession session) {
        String name = (String)session.getAttribute("userName");
        Author byName = authorRepository.findByName(name);
        String documentList = byName.getDocumentList();
        String[] split = documentList.split(",");
        List<Document> res = new ArrayList<>();
        for(var doc : split) {
            Document byName1 = documentRepository.findByName(doc);
            res.add(byName1);
        }
        return new ResponseEntity<>(res, HttpStatus.OK);
    }

    @GetMapping("/movies")
    public ResponseEntity<List<Movie>> getMovies(HttpSession session) {
        String name = (String)session.getAttribute("userName");
        Author byName = authorRepository.findByName(name);
        String movieList = byName.getMovieList();
        String[] split = movieList.split(",");
        List<Movie> res = new ArrayList<>();
        for(var movie : split) {
            Movie found = movieRepository.findByTitle(movie);
            res.add(found);
        }
        return new ResponseEntity<>(res, HttpStatus.OK);
    }

    @GetMapping("/most")
    public ResponseEntity<String> mostAuthor(HttpSession session) {
        var documents = documentRepository.findAll();
        var authors = authorRepository.findAll();
        int max = 0;
        String bestDoc = "";
        for(var doc : documents)
        {
            int count = 0;
            for(var author : authors)
            {
                if (author.getDocumentList().contains(doc.getName()))
                {
                    count++;
                }
            }
            if (count > max)
            {
                max = count;
                bestDoc = doc.getName();
            }
        }
        return new ResponseEntity<>(bestDoc, HttpStatus.OK);
    }


}
