<?php

require 'connection.php';

try{
    $id = $_POST['id'];
    $author = $_POST['author'];
    $name = $_POST['name'];
    $type = $_POST['type'];
    $recipe = $_POST['recipe'];
    $sql ="UPDATE recipes 
           SET author='$author', name='$name', type='$type', recipe = '$recipe'
           WHERE id= $id";
    $result = mysqli_query($conn, $sql);
}
catch (Exception $e) {
    echo 'Message: ' .$e->getMessage();
}
return header("location: ../update.php");

?>