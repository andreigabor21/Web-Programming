package ro.ubb.lab8_good.controller;

import ro.ubb.lab8_good.model.Authenticator;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;

@WebServlet(urlPatterns = "/LoginController")
public class LoginController extends HttpServlet {

    private int players;

    public LoginController() {
        super();
        this.players = 0;
    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {
        String username = request.getParameter("username");
        String password = request.getParameter("password");
        System.out.println(username + " " + password);
        Authenticator authenticator = new Authenticator();
        String result = authenticator.authenticate(username, password);
        if(result.equals("error")) {
            request.getRequestDispatcher("login-error.jsp")
                    .forward(request, response);
        }
        else {
            RequestDispatcher rd = null;

            if (this.players < 2) {
                this.players += 1;
                rd = request.getRequestDispatcher("/success.jsp");

            } else {
                rd = request.getRequestDispatcher("/error.jsp");
            }
//            System.out.println(this.players);
            rd.forward(request, response);
        }
    }

}
