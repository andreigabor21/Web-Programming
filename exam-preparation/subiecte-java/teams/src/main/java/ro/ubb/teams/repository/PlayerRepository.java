package ro.ubb.teams.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.teams.model.model.Player;

@Repository
public interface PlayerRepository extends JpaRepository<Player, Long> {
}
