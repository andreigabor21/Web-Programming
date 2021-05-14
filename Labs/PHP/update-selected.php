<?php

require 'backend/connection.php';

try{
    $id = $_GET['id'];
    $sql ="SELECT * FROM recipes where id=".$id;
    $result = mysqli_query($conn, $sql);
    $result = mysqli_fetch_object($result);
}
catch (Exception $e) {
    echo 'Message: ' .$e->getMessage();
}
?>

<!DOCTYPE html>
<html>
<head>
    <?php include 'imports.php'; ?>
</head>
<body>
<div class="container">
    <div class="row">
        <form method="POST" action="backend/update-recipe.php">
            <input type="hidden" name="id" value="<?php echo $id; ?>" />
            <div class="form-group">
                <label for="author">Author</label>
                <input type="text" class="form-control" id="author" name="author" placeholder="Enter author" value="<?php echo $result->author; ?>" required>
            </div>
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" class="form-control" id="name" name="name" placeholder="Enter name" value="<?php echo $result->name; ?>" required>
            </div>
            <div class="form-group">
                <label for="type">Type</label>
                <input type="text" class="form-control" id="type" name="type" placeholder="Enter type" value="<?php echo $result->type; ?>" required>
            </div>
            <div class="form-group">
                <label for="recipe">Recipe</label>
                <input type="text" class="form-control" id="recipe" name="recipe" placeholder="Enter Recipe" value="<?php echo $result->recipe; ?>" required>
            </div>
            <button type="submit" class="btn btn-primary">Submit</button>
            <a type="button" class="btn btn-danger" onclick="history.back()">Back</a>
        </form>
    </div>
</div>
</body>
</html>