package ro.ubb.assets.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ro.ubb.assets.model.AppUser;

public interface UserRepository extends JpaRepository<AppUser, Long> {

    AppUser findByUsernameAndPassword(String username, String password);
}
