<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<html>
<head>
    <title>Login</title>
</head>
<body>

<input type="text" name="journalName" id="name">
<button onclick="search()">Search</button>

<ul class="articles">
</ul>

<input type="text" name="summary" id="summary">
<button onclick="add()">Add New Article</button>


</body>
</html>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>
    function search() {
        var name = $("#name").val();
        console.log(name);
        $.get("articles/" + name, function(data, status){
            console.log("Data: " + data + "\nStatus: " + status);
            populate(data);
        });
    }

    function populate(data){
        console.log(data);

        var articles = "";
        for(let article of data) {
            articles += "<li>";
            articles += article.summary + " " + article.dateTime + "</li>";
        }
        $(".articles").html(articles);
    }

    function add() {
        var name = $("#name").val();
        var summary = $("#summary").val();
        console.log(summary);
        $.post("articles",
            {"name": name, "summary": summary},
            _ => location.reload()
        );
    }

    function checkNew() {
        $.get("new", function(data, status){
            console.log("Data: " + data + "\nStatus: " + status);
            if (data === true)
                alert("Someone added a new article!");
        });
    }

    setInterval(checkNew, 3000);

</script>

