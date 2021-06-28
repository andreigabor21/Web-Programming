<?php
require "connection.php";

$name = $_GET["name"];

$conn = openConnection();

$sql = "SELECT id FROM persons p WHERE p.name = '$name'";
$result = mysqli_query($conn, $sql);
$personId = $result->fetch_assoc()['id'];

$sql = "SELECT * FROM channels c WHERE c.ownerid = $personId";
$channels = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
        $channels[$cr]['ownerid'] = $row['ownerid'];
        $channels[$cr]['name'] = $row['name'];
        $channels[$cr]['description'] = $row['description'];
        $channels[$cr]['subscribers'] = $row['subscribers'];
        $cr++;
    }
    echo json_encode($channels);
}
else {
    http_response_code(404);
}