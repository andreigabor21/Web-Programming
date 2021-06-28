<?php
require "connection.php";
$conn = openConnection();

session_start();
if (!isset($_SESSION['userid'])) {
    header("Location: login.html");
}
$username = $_SESSION['username'];

$queryDocs = "select * from documents";
$queryAuth = "select * from authors";

$docs = mysqli_query($conn, $queryDocs);
$auth = mysqli_query($conn, $queryAuth);

$index = 0;
$result = [];

while ($rowDoc = mysqli_fetch_array($docs)) {
    $result[$index]['id'] = $rowDoc['id'];
    $result[$index]['name'] = $rowDoc['name'];
    $result[$index]['contents'] = $rowDoc['contents'];
    $index++;
}

$index = 0;
$authors = [];
while ($rowAuth = mysqli_fetch_array($auth)) {
    $authors[$index]['documentList'] = $rowAuth['documentList'];
    $index++;
}

$mostPopularName = "";
$maxAuthors = 0;

foreach ($result as $document) {

    $count = 0;
    foreach ($authors as $author) {
        if (str_contains($author['documentList'], $document['name'])) {
            $count++;
        }
    }

    if ($count > $maxAuthors) {
        $maxAuthors = $count;
        $mostPopularName = $document['name'];
    }
}

echo $mostPopularName;