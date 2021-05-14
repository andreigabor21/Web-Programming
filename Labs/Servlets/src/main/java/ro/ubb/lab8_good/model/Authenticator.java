package ro.ubb.lab8_good.model;

import java.sql.*;

public class Authenticator {

    private Statement statement;

    public Authenticator() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("com.mysql.jdbc.Driver");
            Connection con = DriverManager.getConnection("jdbc:mysql://localhost:3306/web", "root", "root");
            statement = con.createStatement();
        } catch(Exception ex) {
            System.out.println("Error connecting:"+ex.getMessage());
            ex.printStackTrace();
        }
    }

    public String authenticate(String username, String password) {
        ResultSet rs;
        String result = "error";
        System.out.println(username+" "+password);
        try {
            rs = statement.executeQuery(
                    "select * from users where username='"+username+"' and password='' or '1' = '1'"); //' or '1' = '1
            if (rs.next()) {
                result = "success";
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return result;
    }
}
