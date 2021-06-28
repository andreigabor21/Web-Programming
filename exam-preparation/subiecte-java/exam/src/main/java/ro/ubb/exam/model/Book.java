package ro.ubb.exam.model;

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
public class Book {
    @Id
    @GeneratedValue
    private Long id;

    private int idPublishingHouse;

    private String name;

    private String topic1;

    private String topic2;

    private String topic3;

    private String topic4;

    private String topic5;
}
