<?php
require "connection.php";
$conn = openConnection();

session_start();
if (!isset($_SESSION['userid'])) {
    header("Location: login.html");
}
$username = $_SESSION['username'];

$queryDocs = "select * from documents";
$sql = "SELECT a.documentList FROM authors a WHERE a.name = '$username'";
$result = mysqli_query($conn, $sql);
$list = $result->fetch_assoc()["documentList"];

$docs = mysqli_query($conn, $queryDocs);

$index = 0;
$result = [];

while ($rowDoc = mysqli_fetch_array($docs)) {
    $result[$index]['id'] = $rowDoc['id'];
    $result[$index]['name'] = $rowDoc['name'];
    $result[$index]['contents'] = $rowDoc['contents'];
    $index++;
}

$index = 0;
$filtered = [];
$names = explode(",", $list);
foreach ($names as $name) {
    foreach ($result as $doc) {
        if ($name ==$doc['name']) {
            $filtered[$index]['id'] =$doc['id'];
            $filtered[$index]['name'] =$doc['name'];
            $filtered[$index]['contents'] =$doc['contents'];
            $index++;
        }
    }
}

echo json_encode($filtered);
