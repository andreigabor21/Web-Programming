package ro.ubb.authors.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.authors.model.Document;

@Repository
public interface DocumentRepository extends JpaRepository<Document, Long> {
    Document findByName(String name);
}
