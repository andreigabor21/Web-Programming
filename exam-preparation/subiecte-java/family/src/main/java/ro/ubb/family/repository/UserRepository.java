package ro.ubb.family.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.family.model.AppUser;

@Repository
public interface UserRepository extends JpaRepository<AppUser, Long> {
}
