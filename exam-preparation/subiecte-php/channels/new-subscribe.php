<?php

require 'connection.php';
session_start();

if(isset($_SESSION['username']))
{
    $username = $_SESSION["username"];
    $channel = $_POST["channel"];
    var_dump($channel);
    $conn = openConnection();

    $stmt = $conn->prepare("SELECT * FROM channels WHERE name = ?");
    $stmt->bind_param("s", $channel);
    $stmt->execute();

    $result = $stmt->get_result();
    $row = $result->fetch_assoc();

    $subscribers = $row["subscribers"];
    $new_subscribers = "";
    if(strpos($subscribers, $username) == false){
        //daca nu e subscribed userul la channel
        $new_subscribers = $subscribers." ".$username.": " . date("d/m/y");
    }
    else {
        $subscribersList = explode(";", $subscribers);
        foreach($subscribersList as $subscription) {
            if (strpos($subscription, $username) == false)
            {
                $new_subscribers = $new_subscribers.$subscription.";";
            }
            else {
                $new_subscribers = $new_subscribers." ".$username.": " . date("d/m/y");
            }
        }
    }

    $update = $conn->prepare("UPDATE channels SET subscribers = ? WHERE name = ?");
    $update->bind_param("ss", $new_subscribers, $channel);
    $update->execute();
}

else
{
    echo "You didn't specify your name.";
}

?>