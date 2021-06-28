<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<html>
<head>
    <title>Login</title>
</head>
<body>

<form action="add" method="post">
    <input type='text' name="name" id='name' placeholder="Name...">
    <input type='text' name="description" id='description' placeholder="description...">
    <button type="submit">Add</button>
</form>

<form action="search" method="get">
    <input type='text' name="name" id='prod_name' placeholder="Product name...">
    <button type="submit">Search</button>
</form>

<form action="command" method="post">
    <input type='text' name="product" id='product_name' placeholder="Product name...">
    <input type='text' name="quantity" id='quantity' placeholder="Quantity...">
    <button type="submit">Command</button>
</form>


<ul class="products">
</ul>

</body>
</html>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    function populate(data){
        console.log(data);

        var products = "";
        for(let product of data) {
            products += "<li>";
            products += "Name: " + product.name + "  " +
                "Description: " + product.description;
            products += "</li>";
        }
        $(".products").html(products);
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

    // function add() {
    //     var name = $("#name").val();
    //     var description =  $("#description").val();
    //     console.log(name, description);
    //     $.post("add",
    //         {"name": name, "description": description}
    //     );
    // }

    // function search() {
    //     var name = $("#prod_name").val();
    //     console.log(name);
    //     $.get("search", {"name": name}
    //     ).done(data => populate(data));
    // }

    function checkNew() {
        $.get("new", function(data, status){
            console.log("Data: " + data + "\nStatus: " + status);
            if (data !== "-")
                alert("Someone added a new article!" + data);
        });
    }

    // setInterval(checkNew, 3000);
</script>

