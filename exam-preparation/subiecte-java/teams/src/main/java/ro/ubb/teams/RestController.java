package ro.ubb.teams;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import ro.ubb.teams.model.model.Team;
import ro.ubb.teams.repository.PlayerRepository;
import ro.ubb.teams.repository.TeamRepository;

import javax.servlet.http.HttpSession;
import java.util.List;
import java.util.stream.Collectors;

@Controller
public class RestController {
    @Autowired
    private PlayerRepository playerRepository;

    @Autowired
    private TeamRepository teamRepository;

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

    @GetMapping("/all")
    public ResponseEntity<List<Team>> getAll() {
        return new ResponseEntity<>(this.teamRepository.findAll(),
                HttpStatus.OK);

    }

    @GetMapping("/myTeam")
    public ResponseEntity<List<Team>> getMyTeam(HttpSession session) {
        String name = (String) session.getAttribute("userName");
        System.out.println(name);
        List<Team> all = teamRepository.findAll()
                .stream()
                .filter(t -> t.getMembers().contains(name))
                .collect(Collectors.toList());
        return new ResponseEntity<>(all,
                HttpStatus.OK);
    }

    @PostMapping("/add")
    @Transactional
    public ResponseEntity<?> add(@RequestParam String teams,
                                                @RequestParam String player,
                                                HttpSession session) {
        String name = (String) session.getAttribute("userName");
        System.out.println(name);
        String[] split = teams.split(",");
        for(String teamName : split) {
            System.out.println(teamName);
            if (teamRepository.findByName(teamName) == null) {
                Team newTeam = Team.builder()
                        .name(teamName)
                        .members("")
                        .build();
                teamRepository.save(newTeam);
            }
            Team byName = teamRepository.findByName(teamName);
            StringBuilder members = new StringBuilder(byName.getMembers());
            members.append(player)
                    .append(";");
            byName.setMembers(members.toString());
            teamRepository.save(byName);
        }
        return new ResponseEntity<>(HttpStatus.OK);

    }


}
