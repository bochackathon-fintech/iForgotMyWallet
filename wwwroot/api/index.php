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
  $availableMethods = array("getUserData","getAccounts","setPaymentRequest","getPaymentStatus","getMostRecentPaymentRequest","setPaymentRequestStatus");

  //Checks if the API number passed from parameter is available. Otherwise abort the connection
  if (!in_array($api,$availableAPIs)) {
      $helper->exitJSON("Invalid API number",0);
  }

 //Checks if the method passed is available. Otherwise abort the connection
  if (!in_array($method,$availableMethods)) {
      $helper->exitJSON("Invalid method",0);
  }

  require_once(__DIR__ . "/" . $api . "/user.php");
  require_once(__DIR__ . "/" . $api . "/account.php");
  require_once(__DIR__ . "/" . $api . "/payment.php");

  switch ($method) {
    case 'getUserData':
        $user_id =  $_POST["user_id"];
        $configuration = new User;
        $result = $configuration->$method($user_id);
      break;
     case 'getAccounts':
        $user_id =  $_POST["user_id"];
        $view = new Account();
        $result = $view->$method($user_id);
      break;
      case 'setPaymentRequest':
        $acc_id =  $_POST["acc_id"];
        $account_id =  $_POST["account_id"];
        $amount =  $_POST["amount"];
        $description =  $_POST["description"];
        $user_id =  $_POST["user_id"];
        $name =  $_POST["name"];
        $view = new Payment();
        $result = $view->$method($user_id,$acc_id,$account_id,$amount,$description,$name);
      break;
      case 'getPaymentStatus':
        $payment_id =  $_POST["payment_id"];
        $view = new Payment();
        $result = $view->$method($payment_id);
      break;
      case 'getMostRecentPaymentRequest':
        $view = new Payment();
        $result = $view->$method();
      break;
       case 'setPaymentRequestStatus':
        $payment_id =  $_POST["payment_id"];
        $status =  $_POST["status"];
        $view = new Payment();
        $result = $view->$method($payment_id,$status);
      break;
    default:
      $helper->exitJSON("default",0);
      break;
    if ($result==0) {
      $helper->exitJSON("Something went wrong",0);
    }
  }
?>
