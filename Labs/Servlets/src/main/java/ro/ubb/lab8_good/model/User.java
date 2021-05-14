package ro.ubb.lab8_good.model;

public class User {

    public Board board;
    private Double id;

    public User(Double id) {
        this.id = id;
        board = new Board();
    }

    public Double getId() {
        return id;
    }
}
