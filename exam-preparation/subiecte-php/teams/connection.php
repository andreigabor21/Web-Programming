<?php
function openConnection() {
    $connection = mysqli_connect("localhost", "root", "root", "teams");
    if(!$connection) {
        die("Could not connect");
    }
    return $connection;
}

function closeConnection(mysqli $conn) {
    $conn->close();
}
?>