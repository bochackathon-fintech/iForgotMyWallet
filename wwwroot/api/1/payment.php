<?php
 
class Payment {
   
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
  public  function setPaymentRequest($user_id,$acc_id,$account_id,$amount,$description,$name) {
     if ($stmt = $this->db->prepare("INSERT INTO tbl_payment (to_acc_id,to_account_id,amount,description,user_id,name) VALUES (?,?,?,?,?,?)")){
                $stmt->bind_param("isdsis",$acc_id,$account_id,$amount,$description,$user_id,$name);
                $stmt->execute();
                $payment_id=$stmt->insert_id;
                $stmt->close();
            }
            $response = 0;
            //If commit is successfull we updating our local dictionary with views and ids
            if($this->db->commit()){
              $response = $payment_id;
            }
            echo "{\"response\":" . $response . "}";
  }

   public  function setPaymentRequestStatus($payment_id,$status) {
     if ($stmt = $this->db->prepare("UPDATE tbl_payment SET status= ? WHERE payment_id = ?")){
                $stmt->bind_param("ii",$status,$payment_id);
                $stmt->execute();
                $stmt->close();
            }
            $response = 0;
            //If commit is successfull we updating our local dictionary with views and ids
            if($this->db->commit()){
              $response = 1;
            }
            echo "{\"response\":" . $response . "}";
  }
   public  function getPaymentStatus($payment_id) {
      $result_json = "{\"status\":";

      if ($stmt = $this->db->prepare("SELECT status FROM tbl_payment WHERE payment_id = ?")){
          $stmt->bind_param("i",$payment_id);
          $stmt->execute();
          $stmt->bind_result($status);
          while ($stmt->fetch()) {
            //printf("%s %s\n", $name, $surname);
            //echo $name . $surname;
            $statusStr=$status;
          }

          $result_json = $result_json . "\"" . $statusStr . "\"}";
          echo $result_json;
          $stmt->close();
      }
  }

  public  function getMostRecentPaymentRequest() {
      $result_json = "{\"payment_id\":";

      if ($stmt = $this->db->prepare("SELECT payment_id,description,amount,to_account_id,name FROM tbl_payment WHERE status = 0 ORDER BY dt DESC LIMIT 1")){
          $stmt->execute();
          $stmt->bind_result($payment_id,$description,$amount,$to_account_id,$name);
          while ($stmt->fetch()) {
            //printf("%s %s\n", $name, $surname);
            //echo $name . $surname;
            $payment_idStr=$payment_id;
            $descriptionStr = $description;
            $amountStr = $amount;
            $to_account_idStr= $to_account_id;
            $nameStr = $name;
          }

          $result_json = $result_json . $payment_idStr . ","  .  "\"description\":\"" . $descriptionStr . "\"," . "\"amount\":" . $amountStr . "," . "\"to_account_id\":\"" . $to_account_idStr . "\"," . "\"name\":\"" . $nameStr . "\"}";
          echo $result_json;
          $stmt->close();
      }
  }
}
?>