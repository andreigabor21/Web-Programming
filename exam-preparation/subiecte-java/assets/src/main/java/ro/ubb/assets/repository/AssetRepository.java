package ro.ubb.assets.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ro.ubb.assets.model.Asset;

public interface AssetRepository extends JpaRepository<Asset, Long> {
}
