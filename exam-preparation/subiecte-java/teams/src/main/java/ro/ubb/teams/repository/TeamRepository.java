package ro.ubb.teams.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.teams.model.model.Team;

@Repository
public interface TeamRepository extends JpaRepository<Team, Long> {

    Team findByName(String name);
}
