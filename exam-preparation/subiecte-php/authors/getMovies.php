<?php
require "connection.php";
$conn = openConnection();

session_start();
if (!isset($_SESSION['userid'])) {
    header("Location: login.html");
}
$username = $_SESSION['username'];

$queryMovies = "select * from movies";
$sql = "SELECT a.movieList FROM authors a WHERE a.name = '$username'";
$result = mysqli_query($conn, $sql);
$list = $result->fetch_assoc()["movieList"];
$docs = mysqli_query($conn, $queryMovies);

$index = 0;
$result = [];

while ($rowDoc = mysqli_fetch_array($docs)) {
    $result[$index]['id'] = $rowDoc['id'];
    $result[$index]['title'] = $rowDoc['title'];
    $result[$index]['duration'] = $rowDoc['duration'];
    $index++;
}

$index = 0;
$filtered = [];
$names = explode(",", $list);
foreach ($names as $name) {
    foreach ($result as $doc) {
        if ($name ==$doc['title']) {
            $filtered[$index]['id'] =$doc['id'];
            $filtered[$index]['title'] =$doc['title'];
            $filtered[$index]['duration'] =$doc['duration'];
            $index++;
        }
    }
}

echo json_encode($filtered);
