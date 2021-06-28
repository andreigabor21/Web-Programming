package ro.ubb.journals.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.journals.model.Article;

@Repository
public interface ArticleRepository extends JpaRepository<Article, Long> {
}
