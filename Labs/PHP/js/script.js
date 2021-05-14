$(document).ready(function(){
    $('#form-type').on("keyup", function(){
        var inputVal = $(this).val();
        var $result = $("#result");
        if(inputVal.length > 0){
            $.get("Labs/lab6/backend/get_recipe_by_type.php", {term: inputVal}).done(function(data){
                $result.html(data);
            });
        } else{
            $result.empty();
        }
    });


});

