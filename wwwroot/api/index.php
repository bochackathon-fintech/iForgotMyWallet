<?php
   require_once(__DIR__ . "/helper.php");
   $helper = new Helper();
  // Checks if the required parameters have been passed. Otherwise abort the connection
  if (!isset($_POST["method"]) || !isset($_POST["api"])) {
      $helper->exitJSON("Invalid request. Missing parameters",0);
  }

  //Getting the parameters as local vars
  $method = $_POST["method"];
  $api = $_POST["api"];

  //The available APIs versions
  $availableAPIs = array("1");

  //The available methods
  $availableMethods = array("getUserData","addViews");

  //Checks if the API number passed from parameter is available. Otherwise abort the connection
  if (!in_array($api,$availableAPIs)) {
      $helper->exitJSON("Invalid API number",0);
  }

 //Checks if the method passed is available. Otherwise abort the connection
  if (!in_array($method,$availableMethods)) {
      $helper->exitJSON("Invalid method",0);
  }

  require_once(__DIR__ . "/" . $api . "/user.php");

  switch ($method) {
    case 'getUserData':
        $user_id =  $_POST["user_id"];
        $configuration = new User;
        $result = $configuration->$method($user_id);
      break;
     case 'addViews':
        $view = new View();
        $result = $view->$method();
      break;
    default:
      $helper->exitJSON("default",0);
      break;
    if ($result==0) {
      $helper->exitJSON("Something went wrong",0);
    }
  }
?>
