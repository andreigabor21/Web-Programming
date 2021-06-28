<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<html>
<head>
    <title>Login</title>
</head>
<body>

<ul class="posts">
</ul>


<input type='text' name="topicName" id='topicName' placeholder="Topic Name...">
<input type='text' name="text" id='text' placeholder="Text...">
<button onClick=add()>Add</button>

</body>
</html>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    function populate(data){
        console.log(data);

        var posts = "";
        for(let post of data) {
            posts += "<li>";
            posts += "User: " + post.userName + " " +
                "Topic ID: " + post.topicId + " " +
                "Text: " + post.text + " " +
                "Date: " + post.date;
            posts += "<input type='text' name='topicId' id='post" + post.id + "-topicId' placeholder='Topic ID...'>";
            posts += "<input type='text' name='text' id='post" + post.id + "-text' placeholder='Text...'>";
            posts += "<button onClick=update(" + post.id + ")>Update</button>";
            posts += "</li>";
        }
        $(".posts").html(posts);
    }

    $(document).ready(function(){
        $.get("posts", data => populate(data));
    });

    function update(id) {
        var topicId = $("#post"+id+"-topicId").val();
        var text =  $("#post"+id+"-text").val();
        console.log(topicId, text);
        $.ajax({
            type: 'PUT',
            url: 'update/' + id,
            contentType: 'application/json',
            data: JSON.stringify({id: 1, userName: "user", 'topicId': topicId, 'text': text, date: 0})
        }).done(function () {
            console.log('SUCCESS');
        }).fail(function (msg) {
            console.log('FAIL');
        }).always(function (msg) {
            console.log('ALWAYS');
        });
    }

    function add() {
        var topicName = $("#topicName").val();
        var text =  $("#text").val();
        console.log(topicName, text);
        $.post("add",
            {"topicName": topicName, "text": text}
        );
    }

    function checkNew() {
        $.get("new", function(data, status){
            console.log("Data: " + data + "\nStatus: " + status);
            if (data !== "-")
                alert("Someone added a new article!" + data);
        });
    }

    setInterval(checkNew, 3000);
</script>

