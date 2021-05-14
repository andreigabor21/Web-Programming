<?php

require 'connection.php';

try{
    $author = $_POST['author'];
    $name = $_POST['name'];
    $type = $_POST['type'];
    $recipe = $_POST['recipe'];

    $sql ="INSERT INTO recipes(author, name, type, recipe) VALUES ('$author','$name', '$type','$recipe')";

    $result = mysqli_query($conn, $sql);
    $_SESSION['message'] = "Succesful operation!";
}
catch (Exception $e) {
    echo 'Message: ' .$e->getMessage();
}
return header("location: ..");

?>