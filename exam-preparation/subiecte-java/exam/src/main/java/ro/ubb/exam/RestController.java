package ro.ubb.exam;

import org.apache.coyote.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import ro.ubb.exam.model.Book;
import ro.ubb.exam.model.PublisherDto;
import ro.ubb.exam.model.PublishingHouse;
import ro.ubb.exam.repository.BookRepository;
import ro.ubb.exam.repository.PublishingHouseRepository;

import javax.servlet.http.HttpSession;
import java.util.ArrayList;
import java.util.List;

@Controller
public class RestController {
    @Autowired
    private PublishingHouseRepository publishingHouseRepository;

    @Autowired
    private BookRepository bookRepository;

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

    @PostMapping("/delete")
    public ResponseEntity<?> delete(@RequestParam int id) {
        System.out.println(id);
        List<Book> foundBooks = bookRepository.findByIdPublishingHouse(id);
        System.out.println(foundBooks);
        if (foundBooks.size() == 0)
            publishingHouseRepository.deleteById((long) id);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @GetMapping("/publishers")
    public ResponseEntity<List<PublisherDto>> getPublishers() {
        List<PublishingHouse> all = publishingHouseRepository.findAll();
        List<PublisherDto> res = new ArrayList<>();
        for (PublishingHouse publishingHouse : all) {
            int id = Integer.parseInt(String.valueOf(publishingHouse.getId()));
            int count = bookRepository.findByIdPublishingHouse(id).size();
            PublisherDto build = PublisherDto.builder()
                    .id(id)
                    .name(publishingHouse.getName())
                    .url(publishingHouse.getUrl())
                    .count(count)
                    .build();
            res.add(build);
        }
        System.out.println(res);
        return new ResponseEntity<>(res, HttpStatus.OK);
    }

    @GetMapping("/books")
    public ResponseEntity<List<Book>> getPublishers(@RequestParam String topics) {
        System.out.println(topics);
        String[] split = topics.split(",");
        List<Book> books = bookRepository.findAll();
        List<Book> res = new ArrayList<>();
        for (Book b : books) {
            int matches = 0;
            for (String topic : split) {
                if(b.getTopic1().equals(topic))
                    matches++;
                if(b.getTopic2().equals(topic))
                    matches++;
                if(b.getTopic3().equals(topic))
                    matches++;
                if(b.getTopic4().equals(topic))
                    matches++;
                if(b.getTopic5().equals(topic))
                    matches++;
            }
            if (matches == 3)
                res.add(b);
        }
        System.out.println(res);
        return new ResponseEntity<>(res, HttpStatus.OK);
    }


}
