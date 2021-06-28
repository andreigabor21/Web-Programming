<?php

require "connection.php";
$conn = openConnection();

session_start();
if (!isset($_SESSION['userid'])) {
    header("Location: login.html");
}
$userid = $_SESSION['userid'];

$name = $_POST["name"];
$description = $_POST["description"];
$value = $_POST["value"];

$insert = "INSERT INTO assets (userid, name, description, value) VALUES
        (" . $userid . ",'" . $name . "','" . $description . "'," . $value . ")";
$conn->query($insert);

$sql = "SELECT * FROM assets WHERE userid = " . $userid;

$assets = [];
if($result = mysqli_query($conn, $sql)) {
    $cr = 0;
    while($row = mysqli_fetch_assoc($result)) {
        $assets[$cr]['name'] = $row['name'];
        $assets[$cr]['description'] = $row['description'];
        $assets[$cr]['value'] = $row['value'];
        $cr++;
    }
    echo json_encode($assets);
}
else {
    http_response_code(404);
}