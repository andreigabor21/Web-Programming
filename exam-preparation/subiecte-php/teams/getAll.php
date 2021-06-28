<?php
require "connection.php";

$conn = openConnection();

$sql = "SELECT * FROM teams";

$teams = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
//        var_dump($row);
        $teams[$cr]['captainId'] = $row['captainId'];
        $teams[$cr]['name'] = $row['name'];
        $teams[$cr]['description'] = $row['description'];
        $teams[$cr]['members'] = $row['members'];
        $cr++;
    }
    //    print_r($students);
    echo json_encode($teams);
}
else {
    http_response_code(404);
}