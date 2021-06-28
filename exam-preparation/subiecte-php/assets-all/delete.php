<?php

require "connection.php";
$conn = openConnection();

session_start();
if (!isset($_SESSION['userid'])) {
    header("Location: login.html");
}

$id = $_POST["id"];
echo $id;

try{
    $sql ="DELETE FROM assets where id=" . $id;
    $result = mysqli_query($conn, $sql);
}
catch (Exception $e) {
    http_response_code(404);
}

