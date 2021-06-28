package ro.ubb.family;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import ro.ubb.family.model.FamilyRelation;
import ro.ubb.family.repository.FamilyRepository;
import ro.ubb.family.repository.UserRepository;

import javax.servlet.http.HttpSession;

@Controller
public class RestController {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private FamilyRepository familyRepository;

    @RequestMapping("/")
    public String showLoginPage() {
        return "login";
    }

    @PostMapping("/login")
    public String login(@RequestParam String username,
                        @RequestParam String parent,
                        HttpSession session) {
        FamilyRelation family = familyRepository.findByUserNameAndFatherOrMother(username, parent, parent);
        System.out.println(family);
        if (family != null) {
            session.setAttribute("username", family.getUserName());
            return "home";
        }
        return "login";
    }

    @PostMapping("/add")
    public ResponseEntity<?> addParents(@RequestParam String username,
                             @RequestParam String father,
                             @RequestParam String mother,
                             HttpSession session) {
        if(session.getAttribute("username") == null) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        FamilyRelation build = FamilyRelation.builder()
                .userName(username)
                .father(father)
                .mother(mother)
                .build();
        familyRepository.save(build);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @GetMapping("/father")
    public ResponseEntity<String> getFathers(HttpSession session) {
        String username = (String)session.getAttribute("username");
        if(username == null) {
            return new ResponseEntity<>(null, HttpStatus.BAD_REQUEST);
        }
        System.out.println(username);
        StringBuilder builder = new StringBuilder();
        while(true) {
            var currentUser = familyRepository.findByUserName(username);
            if (currentUser == null) {
                break;
            } else {
              builder.append(currentUser.getFather())
                    .append(";");
              username = currentUser.getFather();
            }
        }
        System.out.println(builder);
        return new ResponseEntity<>(builder.toString(), HttpStatus.OK);
    }

    @GetMapping("/mother")
    public ResponseEntity<String> getMothers(HttpSession session) {
        String username = (String)session.getAttribute("username");
        if(username == null) {
            return new ResponseEntity<>(null, HttpStatus.BAD_REQUEST);
        }
        System.out.println(username);
        StringBuilder builder = new StringBuilder();
        while(true) {
            var currentUser = familyRepository.findByUserName(username);
            if (currentUser == null) {
                break;
            } else {
                builder.append(currentUser.getMother())
                        .append(";");
                username = currentUser.getMother();
            }
        }
        System.out.println(builder);
        return new ResponseEntity<>(builder.toString(), HttpStatus.OK);
    }
}
