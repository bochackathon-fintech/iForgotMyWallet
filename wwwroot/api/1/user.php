<?php
 
class User {
   
   private $db;

   //Constructor - Open DB Connection
   function __construct() {

      $this->db = new mysqli('127.0.0.1','root','root','iForgotMyWallet');
      $this->db->autocommit(FALSE);
      $this->helper = new Helper();
   }

   //Destructor - Close DB Conection
   function __destruct(){
    
      $this->db->close();
   }
    // Main method 
  public  function getUserData($user_id) {
      $result_json = "{\"name\":";

      if ($stmt = $this->db->prepare("SELECT name,surname FROM tbl_user WHERE user_id = ?")){
          $stmt->bind_param("i",$user_id);
          $stmt->execute();
          $stmt->bind_result($name,$surname);
          while ($stmt->fetch()) {
            //printf("%s %s\n", $name, $surname);
            //echo $name . $surname;
            $nameStr=$name . " " . $surname;
          }
          $result_json = $result_json . "\"" . $nameStr . "\"," . "\"accounts\":[";
          $stmt->close();
      }

      if ($stmt = $this->db->prepare("SELECT acc_id,account_id,view_id FROM tbl_account WHERE user_id = ?")){
          $stmt->bind_param("i",$user_id);
          $stmt->execute();
          $stmt->bind_result($acc_id,$account_id,$view_id);
                     $stmt->store_result();
          $rows = $stmt->num_rows;
          $count = 1;
          while ($stmt->fetch()) {
            $result_json = $result_json . "{\"acc_id\":\"" . $acc_id . "\"," . "\"account_id\":\"" . $account_id . "\"," . "\"view_id\":\"" . $view_id . "\"}";
             
             if( $count != $rows ) {
                  $result_json = $result_json . ",";    
              }
              $count++;
              //echo "count " . $count . " num_rows" . $rows;
            //printf("%s %s %s\n", $acc_id, $account_id, $view_id);
           
          }

          $stmt->close();
      }

      $result_json = $result_json . "]}";
      echo $result_json;

  }
}
?>