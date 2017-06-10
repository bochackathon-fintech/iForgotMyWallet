<?php
 
class AlertView {
   public $alertview_id;
   public $message;
   public $app_id;
   public $view_id;
   public $okButton;
   public $cancelButton;
   public $trigger;

   function __construct($alertview_id,$message,$app_id,$view_id,$okButton,$cancelButton,$trigger) {
      $this->alertview_id = $alertview_id;
      $this->message= $message;
      $this->app_id = $app_id;
      $this->view_id = $view_id;
      $this->okButton = $okButton;
      $this->cancelButton = $cancelButton;
      $this->trigger = $trigger;
   }
}
?>