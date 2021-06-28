package ro.ubb.topics.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ro.ubb.topics.model.Topic;

public interface TopicRepository extends JpaRepository<Topic, Long> {

    Topic findByTopicName(String topicName);
}
