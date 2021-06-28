<?php
require "connection.php";

$conn = openConnection();

$user = $_POST["username"];

session_start();
$_SESSION['username'] = $user;
header("Location: main.html");


closeConnection($conn);
?>