<?php
require "connection.php";

$conn = openConnection();

$id = $_POST["id"];
$keyword1 = $_POST["keyword1"];
$keyword2 = $_POST["keyword2"];
$keyword3 = $_POST["keyword3"];
$keyword4 = $_POST["keyword4"];
$keyword5 = $_POST["keyword5"];

$update = "UPDATE documents " .
            "SET keyword1='$keyword1', keyword2='$keyword2', keyword3='$keyword3', keyword4='$keyword4', keyword5='$keyword5'".
    " WHERE id=$id";
$conn->query($update);

header("Location: index.html");