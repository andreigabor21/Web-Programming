package ro.ubb.journals.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import net.bytebuddy.dynamic.loading.InjectionClassLoader;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Article {
    @Id
    @GeneratedValue
    private Long id;

    private String userName;

    private int journalId;

    private String summary;

    private int dateTime;

    public Article(String userName, int journalId, String summary, int dateTime) {
        this.userName = userName;
        this.journalId = journalId;
        this.summary = summary;
        this.dateTime = dateTime;
    }
}
