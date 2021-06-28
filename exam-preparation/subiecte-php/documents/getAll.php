<?php
require "connection.php";

$conn = openConnection();

$sql = "SELECT * FROM documents";

$documents = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
//        var_dump($row);
        $documents[$cr]['id'] = $row['id'];
        $documents[$cr]['idWebSite'] = $row['idWebSite'];
        $documents[$cr]['name'] = $row['name'];
        $documents[$cr]['keyword1'] = $row['keyword1'];
        $documents[$cr]['keyword2'] = $row['keyword2'];
        $documents[$cr]['keyword3'] = $row['keyword3'];
        $documents[$cr]['keyword4'] = $row['keyword4'];
        $documents[$cr]['keyword5'] = $row['keyword5'];
        $cr++;
    }
    //    print_r($students);
    echo json_encode($documents);
}
else {
    http_response_code(404);
}