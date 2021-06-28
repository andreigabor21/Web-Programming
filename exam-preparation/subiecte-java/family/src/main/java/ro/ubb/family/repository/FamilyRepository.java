package ro.ubb.family.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ro.ubb.family.model.FamilyRelation;

@Repository
public interface FamilyRepository extends JpaRepository<FamilyRelation, Long> {

    FamilyRelation findByUserNameAndFatherOrMother(String userName, String father, String mother);

    FamilyRelation findByUserName(String username);
}
