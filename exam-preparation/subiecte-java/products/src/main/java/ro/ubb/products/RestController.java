package ro.ubb.products;

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
import ro.ubb.products.model.Product;
import ro.ubb.products.repository.OrdersRepository;
import ro.ubb.products.repository.ProductRepository;

@Controller
public class RestController {

    @Autowired
    private ProductRepository productRepository;

    @Autowired
    private OrdersRepository ordersRepository;

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
    public String add(@RequestParam String name,
                                 @RequestParam String description,
                                 HttpSession session) {
        System.out.println("Sent:" + name + " " + description);
        Product build = Product.builder()
                .name(name)
                .description(description)
                .build();
        productRepository.save(build);
        return "home";
    }

    /*@PostMapping("/command")
    public ResponseEntity<?> command(@RequestParam String product,
                      @RequestParam String quantity,
                      HttpSession session) {
        System.out.println("Sent:" + name + " " + description);
        Product build = Product.builder()
                .name(name)
                .description(description)
                .build();
        productRepository.save(build);
        return "home";
    }*/

    @GetMapping("/search")
    public ResponseEntity<String> search(@RequestParam String name) {
        System.out.println("Sent:" + name);
        List<Product> collect = productRepository.findAll()
                .stream()
                .filter(p -> p.getName().startsWith(name))
                .collect(Collectors.toList());
        StringBuilder builder = new StringBuilder();
        for (Product product : collect) {
            builder.append("Product name: ")
                    .append(product.getName())
                    .append("  Description: ")
                    .append(product.getDescription())
                    .append("<br>");
        }
        return new ResponseEntity<>(builder.toString(), HttpStatus.OK);
    }
}
