package ro.ubb.journals.model;

import lombok.*;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Journal {
    @Id
    @GeneratedValue
    private Long id;

    private String name;

    public Journal(String name) {
        this.name = name;
    }
}
