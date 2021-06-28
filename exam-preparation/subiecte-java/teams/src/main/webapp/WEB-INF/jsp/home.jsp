<html>
<head>
    <title>Login</title>
</head>
<body>

<ul class="teams">
</ul>

<h2>My teams:</h2>
<ul class="my_teams">
</ul>

<input type='text' name="team" id='team' placeholder="Team Names...">
<input type='text' name="player" id='player' placeholder="Player Name...">
<button onClick=add()>Add</button>

</body>
</html>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    function add() {
        var teams = $("#team").val();
        var player =  $("#player").val();
        console.log(team, player);
        $.post("add",
            {"teams": teams, "player": player}
        );
    }

    function populate(data){
        console.log(data);

        var posts = "";
        for(let post of data) {
            posts += "<li>";
            posts += "captain ID: " + post.captainId + " " + 
                      "name: " + post.name + " " +
                       "description: " + post.description + " " +
                       "members: " + post.members;
            posts += "</li>";
        }
        $(".teams").html(posts);
    }

    $(document).ready(function(){
        $.get("all", data => populate(data));
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

    $(document).ready(function(){
        $.get("myTeam", data => populate_my(data));
    });

</script>
