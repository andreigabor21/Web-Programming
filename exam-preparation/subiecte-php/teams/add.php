<?php

require 'connection.php';
session_start();

if(isset($_SESSION['username']))
{
    $username = $_SESSION["username"];
    $teams = $_POST["teams"];
    $player = $_POST["player"];
    $conn = openConnection();

    //adaugare player daca nu exista
    $sql = "SELECT * FROM players p WHERE p.name = '$player'";
    $result = mysqli_query($conn, $sql);
    $playerId = $result->fetch_assoc();
    if ($playerId == null) {
        $insert = $conn->prepare("INSERT INTO players(name, age) VALUES (?, 21)");
        $insert->bind_param("s", $player);
        $insert->execute();
    }

    ///concatenare la teams
    $player = $_POST["player"] . ";";
    $teamsParse = explode(",", $teams);

    foreach($teamsParse as $team) {
        $update = $conn->prepare("UPDATE teams SET members = CONCAT(members, ?) WHERE name = ?");
        $update->bind_param("ss", $player, $team);
        $update->execute();
    }

}

else
{
    echo "You didn't specify your name.";
}

?>