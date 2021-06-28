package ro.ubb.exam.model;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class PublisherDto {
    private int id;

    private String name;

    private String url;

    private int count;
}
