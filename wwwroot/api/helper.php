<?php
 
class Helper {
   
   //Constructor - Open DB Connection
   function __construct() {
   }

   //Destructor - Close DB Conection
   function __destruct(){
       }

   public function exitJSON($message,$exit_code) {

      $exit_message = "{\"response\":".$exit_code;
      
      if ($exit_code!=1) {
        $exit_message = $exit_message . ",\"message\":\"$message\"";
      }

      $exit_message = $exit_message . "}";

      if ($exit_code!=1) {

          exit($exit_message);
      }
      else {
          echo $exit_message;
      }

   }

}
?>