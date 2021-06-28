package ro.ubb.exam.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.exam.model.PublishingHouse;

@Repository
public interface PublishingHouseRepository extends JpaRepository<PublishingHouse, Long> {
}
