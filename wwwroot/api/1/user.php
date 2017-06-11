<?php
 
class User {
   
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

          $result_json = $result_json . "\"" . $nameStr . "\"}";
          echo $result_json;
          $stmt->close();
      }

  }
}
?>