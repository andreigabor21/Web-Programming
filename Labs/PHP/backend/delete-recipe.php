<?php

require 'connection.php';

try{
    $id = $_GET['id'];
    $sql ="DELETE FROM recipes where id=".$id;
    $result = mysqli_query($conn, $sql);
}
catch (Exception $e) {
    echo 'Message: ' .$e->getMessage();
}

header("location:".$_SERVER['HTTP_REFERER']);

?>