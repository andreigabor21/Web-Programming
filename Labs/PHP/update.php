<?php
require 'backend/connection.php';
$sql ="SELECT * FROM recipes";
$result = mysqli_query($conn, $sql);
?>

<!DOCTYPE html>
<html>
<head>
    <?php include 'imports.php'; ?>

</head>
<body>
<div class="container mt-10">
    <div class="row">
        <ul class="list-group" style="margin-top:100px;">
            <?php
            while ($row = mysqli_fetch_object($result)) {
                echo '<li class="list-group-item">'.
                    'Author: '. $row->author . ',' .
                    '    Name: ' . $row->name . ',' .
                    '    Type: ' . $row->type . ',' .
                    '    Recipe: ' . $row->recipe .
                    '<a href="./update-selected.php?id='.$row->id. '" class="float-end btn btn-danger">Edit</a> </li>';
            }
            ?>
        </ul>
        <a href="index.php" type="button" class="btn btn-danger">Back</a>
    </div>
</div>
</body>
</html>