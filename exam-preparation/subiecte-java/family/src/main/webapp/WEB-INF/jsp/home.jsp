<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<html>
<head>
    <title>Login</title>
</head>
<body>

<input type='text' id='username' placeholder="Parent Name...">
<input type='text' id='father' placeholder="Grandfather Name...">
<input type='text' id='mother' placeholder="Grandmother Name...">
<button onClick=add()>Add</button>

<h3>Father descending line:</h3>
<button onClick=findForFather()>See Father line</button>
<ul class="father_line"></ul>

<br>
<h3>Mother descending line:</h3>
<button onClick=findForMother()>See Mother line</button>
<ul class="mother_line"></ul>

</body>
</html>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    function findForFather() {
        $.get("father", data => populateFather(data));
    }

    function populateFather(data){
        console.log(data);

        var line = "";
        line += "<li>";
        line += "Father line: " + data;
        line += "</li>";

        $(".father_line").html(line);
    }

    function findForMother() {
        $.get("mother", data => populateMother(data));
    }

    function populateMother(data){
        console.log(data);

        var line = "";
        line += "<li>";
        line += "Mother line: " + data;
        line += "</li>";

        $(".mother_line").html(line);
    }

    function add() {
        var username = $("#username").val();
        var father =  $("#father").val();
        var mother =  $("#mother").val();
        console.log(username, father, mother);
        $.post("add",
            {"username": username, "father": father, "mother": mother}
        );
    }

</script>

