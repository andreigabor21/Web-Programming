package ro.ubb.assets.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONArray;
import org.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.view.RedirectView;
import ro.ubb.assets.dto.AssetDto;
import ro.ubb.assets.model.AppUser;
import ro.ubb.assets.model.Asset;
import ro.ubb.assets.repository.AssetRepository;
import ro.ubb.assets.repository.UserRepository;

import javax.servlet.http.HttpSession;
import java.util.List;

@Controller
public class AssetController {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private AssetRepository assetRepository;

    @RequestMapping("/")
    public String showLoginPage() {
        return "login";
    }

    @GetMapping("/assets")
    public ResponseEntity<List<Asset>> getAssets(HttpSession session) {
        Long id = (Long) session.getAttribute("userId");
        var user = userRepository.findById(id).orElseThrow();
        System.out.println(user);
        if (user != null) {
            return new ResponseEntity<>(assetRepository.findAll(), HttpStatus.OK);
        }
        return null;
    }

    @PostMapping("/login")
    public String login(@RequestParam String username, @RequestParam String password,
                        HttpSession session) {
        AppUser user = userRepository.findByUsernameAndPassword(username, password);
        if (user != null) {
            session.setAttribute("userId", user.getId());
            return "home";
        }
        return "login";
    }

    @RequestMapping(value = "/assets", method = RequestMethod.POST)
    public List<AssetDto> insertAssets(@RequestBody List<AssetDto> data, HttpSession session) {
        System.out.println(data);
        Long id = (Long) session.getAttribute("userId");
        AppUser user = userRepository.findById(id).orElseThrow();
        System.out.println(user);
        if (user != null) {
            for (AssetDto assetDto : data) {
                System.out.println(assetDto);
                Asset asset = new Asset(
                        user.getId(),
                        assetDto.getName(),
                        assetDto.getDescription(),
                        assetDto.getValue()
                );
                System.out.println(asset);
                assetRepository.save(asset);
            }
        }
        return data;
    }

}

    /*@RequestMapping(value = "/assets", method = RequestMethod.POST)
    public void insertAssets(@RequestBody String data, HttpSession session) throws JsonProcessingException {
        System.out.println("received post");
        System.out.println(data);
        ObjectMapper mapper = new ObjectMapper();
        AssetDto[] assets = mapper.readValue(data, AssetDto[].class);
        for(AssetDto asset : assets) {
            System.out.println(asset.toString());*/
//            AppController.addAsset(asset.getUserId(), asset.getName(), asset.getDescription(), asset.getValue());



//        System.out.println(data.getClass().getSimpleName());
//        JSONObject obj = new JSONObject(data);
//        System.out.println(obj);
        /*var userId = Long.parseLong((String) session.getAttribute("userId"));
        var user = userRepository.findById(userId)
                .orElseThrow();
        System.out.println(user);
        if (user != null) {
            for (AssetDto assetDto : assetDtos) {
                System.out.println(assetDto);
                assetRepository.save(new Asset(
                        user.getId(),
                        assetDto.getName(),
                        assetDto.getDescription(),
                        assetDto.getValue()
                        ));
            }
        }*/


