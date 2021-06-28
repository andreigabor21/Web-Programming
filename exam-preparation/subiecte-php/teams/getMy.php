<?php
require "connection.php";

session_start();
$name = $_SESSION["username"];

$conn = openConnection();

$sql = "SELECT * FROM teams t WHERE t.members LIKE '%$name%'";

$teams = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
//        var_dump($row);
        $teams[$cr]['name'] = $row['name'];
        $cr++;
    }
    //    print_r($students);
    echo json_encode($teams);
}
else {
    http_response_code(404);
}