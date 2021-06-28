package ro.ubb.journals.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.journals.model.Journal;

@Repository
public interface JournalRepository extends JpaRepository<Journal, Long> {

    Journal findByName(String name);
}
