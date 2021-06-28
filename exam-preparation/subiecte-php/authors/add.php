<?php

require "connection.php";
session_start();
$conn = openConnection();

$name = $_POST['name'];
$contents = $_POST['contents'];

$insert = "INSERT INTO documents (name, contents) VALUES
        ('" . $name . "','" . $contents .  "'" . ")";
$conn->query($insert);
