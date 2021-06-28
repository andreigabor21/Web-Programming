<html>
<head>
    <title>Login</title>
</head>
<body>

<input type='text' name="name" id='name' placeholder="Name...">
<input type='text' name="contents" id='contents' placeholder="Contents...">
<button onClick=add()>Add</button>

<ul class="documents">
</ul>
<ul class="movies">
</ul>

<h2>Document with the largest number of authors:</h2>
<div class="most"></div>


</body>
</html>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    function add() {
        var name = $("#name").val();
        var contents =  $("#contents").val();
        console.log(name, contents);
        $.post("add",
            {"name": name, "contents": contents}
        );
    }

    function populate_docs(data){
        console.log(data);
        var channels = "";
        for(let channel of data) {
            channels += "<li>";
            channels += "ID: " + channel.id + " " +
                "Name: " + channel.name + " " +
                "Contents: " + channel.contents + " " +
                "</li>";
        }
        $(".documents").html(channels);
    }

    function populate_movies(data){
        console.log(data);
        var channels = "";
        for(let channel of data) {
            channels += "<li>";
            channels += "ID: " + channel.id + " " +
                "Title: " + channel.title + " " +
                "Duration: " + channel.duration + " " +
                "</li>";
        }
        $(".movies").html(channels);
    }

    $(document).ready(async function(){
        await $.get("docs", data => populate_docs(data));
        await $.get("movies", data => populate_movies(data));
        interleave();
    });

    function interleave() {
        var $list = $('ul:eq(1)').detach().children();
        $('ul:eq(0) li').after(function (i) {
            return $list.eq(i);
        });
    }

    $(document).ready(function(){
        $.get("most", data => $(".most").html(data));
    });


</script>
