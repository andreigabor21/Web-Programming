package ro.ubb.topics;

import com.sun.xml.bind.v2.schemagen.xmlschema.Particle;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpSession;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;
import java.util.stream.Collectors;

import org.springframework.stereotype.Controller;
import ro.ubb.topics.model.Post;
import ro.ubb.topics.model.Topic;
import ro.ubb.topics.model.TopicDto;
import ro.ubb.topics.repository.PostRepository;
import ro.ubb.topics.repository.TopicRepository;

@Controller
public class RestController {

    @Autowired
    private TopicRepository topicRepository;

    @Autowired
    private PostRepository postRepository;

    int length;

    @RequestMapping("/")
    public String showLoginPage() {
        return "login";
    }

    @PostMapping("/login")
    public String login(@RequestParam String username, HttpSession session) {
        System.out.println(username);
        session.setAttribute("userName", username);

        length = postRepository.findAll().size();

        return "home";
    }

    @GetMapping("/posts")
    public ResponseEntity<List<Post>> getArticles() {
        List<Post> all = postRepository.findAll();
        return new ResponseEntity<>(all, HttpStatus.OK);
    }

    public int getCurrentDay() {
        Calendar calendar = Calendar.getInstance(TimeZone.getDefault());
        //getTime() returns the current date in default time zone
        Date date = calendar.getTime();
        return calendar.get(Calendar.DATE);
    }

    @PutMapping("/update/{id}")
    @Transactional
    public ResponseEntity<?> update(@PathVariable Long id,
                                    @RequestBody Post data,
                                    HttpSession session) {
        System.out.println(id);
        System.out.println(data);
        var postToUpdate = postRepository.getById(id);
        postToUpdate.setUserName((String)session.getAttribute("userName"));
        postToUpdate.setText(data.getText());
        postToUpdate.setTopicId(data.getTopicId());
        postToUpdate.setDate(getCurrentDay());
        postRepository.save(postToUpdate);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PostMapping("/add")
    public ResponseEntity<?> add(@RequestParam String topicName,
                                 @RequestParam String text,
                                 HttpSession session) {
        System.out.println("Sent:" + topicName + " " + text);
        var topic = topicRepository.findByTopicName(topicName);
        System.out.println(topic);
        if (topic == null) {
            Topic newTopic = new Topic();
            newTopic.setTopicName(topicName);
            topicRepository.save(newTopic);
        }
        Post post = new Post();
        var topicId = topicRepository.findByTopicName(topicName).getId();
        post.setUserName((String)session.getAttribute("userName"));
        post.setDate(getCurrentDay());
        post.setTopicId(Math.toIntExact(topicId));
        post.setText(text);

        postRepository.save(post);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @GetMapping("/new")
    public ResponseEntity<String> newlyAdded(HttpSession session) {
        int localLength = postRepository.findAll().size();
        if(localLength > length) {
            length = localLength;
            String userName = (String) session.getAttribute("userName");
            var post = postRepository.findAll()
                    .stream()
                    .sorted((art1, art2) -> art2.getId().compareTo(art1.getId()))
                    .findFirst()
                    .orElseThrow();
            System.out.println(post);
            if (!post.getUserName().equals(userName))
                return new ResponseEntity<>(post.toString(), HttpStatus.OK);
        }
        return new ResponseEntity<>("-", HttpStatus.OK);
    }


}
