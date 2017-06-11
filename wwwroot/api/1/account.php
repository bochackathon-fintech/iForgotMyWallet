<?php
 
class Account {
   
   private $db;

   //Constructor - Open DB Connection
   function __construct() {

      $this->db = new mysqli('127.0.0.1','root','','iforgotmywallet');
      $this->db->autocommit(FALSE);
      $this->helper = new Helper();
   }

   //Destructor - Close DB Conection
   function __destruct(){
    
      $this->db->close();
   }
    // Main method 
  public  function getAccounts($user_id) {
      $result_json = "{\"accounts\":[";

      if ($stmt = $this->db->prepare("SELECT acc_id,account_id,view_id,acount_type,shared_name,nickname FROM tbl_account WHERE user_id = ?")){
          $stmt->bind_param("i",$user_id);
          $stmt->execute();
          $stmt->bind_result($acc_id,$account_id,$view_id,$account_type,$shared_name,$nickname);
                     $stmt->store_result();
          $rows = $stmt->num_rows;
          $count = 1;
          while ($stmt->fetch()) {
            $result_json = $result_json . "{\"acc_id\":\"" . $acc_id . "\"," . "\"account_id\":\"" . $account_id . "\"," . "\"account_type\":\"" . $account_type . "\","  . "\"shared_name\":\"" . $shared_name . "\"," . "\"nickname\":\"" . $nickname . "\"," . "\"view_id\":\"" . $view_id . "\"}";
             
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