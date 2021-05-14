<!DOCTYPE html>
<html>
<head>
    <?php include 'imports.php'; ?>
</head>
<body>
<div class="container">
    <div class="row">
        <form method="POST" action="backend/add-recipe.php">
            <div class="form-group">
                <label for="author">Author</label>
                <input type="text" class="form-control" id="author" name="author" placeholder="Enter Author" required>
            </div>
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" class="form-control" id="name" name="name" placeholder="Enter Name" required>
            </div>
            <div class="form-group">
                <label for="type">Type</label>
                <input type="text" class="form-control" id="type" name="type" placeholder="Enter Type" required>
            </div>
            <div class="form-group">
                <label for="recipe">Recipe</label>
                <input type="text" class="form-control" id="recipe" name="recipe" placeholder="Enter Recipe" required>
            </div>
            <button type="submit" class="btn btn-outline-success">Submit</button>
            <a type="button" class="btn btn-outline-dark"  onclick="history.back()">Back</a>
        </form>
    </div>
</div>
</body>
</html>
