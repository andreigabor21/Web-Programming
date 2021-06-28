package ro.ubb.products.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.products.model.Orders;

@Repository
public interface OrdersRepository extends JpaRepository<Orders, Long> {
}
