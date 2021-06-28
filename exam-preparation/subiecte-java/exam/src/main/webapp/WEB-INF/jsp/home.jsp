<html>
<head>
    <title>Login</title>
</head>
<body>

<input type='text' name="house" id=house placeholder="Publishing house ID...">
<button onClick=remove()>Delete</button>

<br>
<table class="publishers">
</table>


<input type='text' name="topics" id="topics" placeholder="Topics...">
<button onClick=findByTopics()>Find By Topics</button>

<ul class="books"></ul>

</body>
</html>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    function findByTopics() {
        var topics = $("#topics").val();
        console.log(topics);
        $.get("books",
            {"topics": topics},
            data => populate_books(data)
        );
    }

    function populate_books(data){
        console.log(data);
        var books = "";
        for(let book of data) {
            books += "<li>";
            books +=
                "Name: " + book.name + "--" +
                "Topics: " + book.topic1 + ", " +
                book.topic2 + ", " +
                book.topic3 + ", " +
                book.topic4 + ", " +
                book.topic5 + ", " +
                "</li>";
        }
        $(".books").html(books);
    }

    function remove() {
        var id = $("#house").val();
        console.log(id);
        $.post("delete",
            {"id": id},
            _ => location.reload()
        );
    }

    function populate(data){
        console.log(data);

        var publishers = "<tr>\n" +
            "        <th>ID</th>\n" +
            "        <th>Name</th>\n" +
            "        <th>URL</th>\n" +
            "        <th>Books Count</th>\n" +
            "    </tr>";
        for(let publisher of data) {
            publishers += "<tr>" +
                "<td>" + publisher.id + "</td>" +
                "<td>" + publisher.name + "</td>" +
                "<td>" + publisher.url + "</td>" +
                "<td>" + publisher.count + "</td>" +
                "</tr>";
        }
        $(".publishers").html(publishers);
    }

    $(document).ready(function(){
        $.get("publishers", data => populate(data));
    });

    function populate_my(data){
        console.log(data);

        var posts = "";
        for(let post of data) {
            posts += "<li>";
            posts += "name: " + post.name;
            posts += "</li>";
        }
        $(".my_teams").html(posts);
    }

    /*$(document).ready(function(){
        $.get("myTeam", data => populate_my(data));
    });*/

</script>
