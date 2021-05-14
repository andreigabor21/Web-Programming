<?php
require 'connection.php';

    $param =  $_REQUEST["term"];
    $sql = "SELECT * FROM recipes WHERE type LIKE '%{$param}%'";
    $result = mysqli_query($conn, $sql);
    if (mysqli_num_rows($result) > 0) {
        echo '<div class="container mt-10">' .
              '<div class="row">' .
                '<ul class="list-group">';
        while ($row = mysqli_fetch_object($result)) {
            echo '<li class="list-group-item">'.
                'Author: '. $row->author . ',' .
                '    Name: ' . $row->name . ',' .
                '    Type: ' . $row->type . ',' .
                '    Recipe: ' . $row->recipe . '</li>';
        }
        echo '</ul>' . '</div>' . '</div>';
}
?>