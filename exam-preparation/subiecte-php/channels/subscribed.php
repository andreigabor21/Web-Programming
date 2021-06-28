<?php

require "connection.php";

session_start();
$name = $_SESSION["username"];

$conn = openConnection();

$sql = "SELECT * FROM channels c WHERE c.subscribers LIKE '%$name%'";
$channels = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
        $channels[$cr]['name'] = $row['name'];
        $channels[$cr]['description'] = $row['description'];
        $cr++;
    }
    echo json_encode($channels);
}
else {
    http_response_code(404);
}