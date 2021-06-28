package ro.ubb.topics.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ro.ubb.topics.model.Post;

public interface PostRepository extends JpaRepository<Post, Long> {
}
