package ro.ubb.assets.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Asset {
    @Id
    @GeneratedValue
    private Long id;

    private Long userId;

    private String name;

    private String description;

    private int value;

    public Asset(Long userId, String name, String description, int value) {
        this.userId = userId;
        this.name = name;
        this.description = description;
        this.value = value;
    }
}
