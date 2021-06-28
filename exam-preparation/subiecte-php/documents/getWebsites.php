<?php

require "connection.php";

$conn = openConnection();

$sql = "SELECT w.id, w.url, COUNT(*) as nr FROM websites w INNER JOIN documents d ON w.id = d.idWebSite GROUP BY w.id, w.url";

$documents = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
//        var_dump($row);
        $documents[$cr]['id'] = $row['id'];
        $documents[$cr]['url'] = $row['url'];
        $documents[$cr]['nr'] = $row['nr'];
        $cr++;
    }
    //    print_r($students);
    echo json_encode($documents);
}
else {
    http_response_code(404);
}