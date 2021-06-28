<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<html>
<head>
    <title>Login</title>
</head>
<body>
<form action="login" method="post">
    Username:
    <input type="text" name="username">
    <br>
    Parent:
    <input type="text" name="parent">
    <br>
    <button type="submit">Login</button>
</form>
</body>
</html>