# iForgotMyWallet

The purpose of this project is to eliminate the use of cash, checks and credits cards. We are going to accomplish that using the technologies that our mobiles phones already provide. (Geolocation, Internet Access).
We will cover cases for B2B,B2C,C2C.

The user will be able to:
1) Receive money from or B or C
2) Send money to B or C
3) Share (give access to bank account) account with other users (eg: Father give access to child for 20$ pocket money per week, eg: Boss gives account access to employee to buy hardware for company (on the go-eChecks). eg: Boss gives access to the employee to accept payments on behalf of company).
4) Developed API for IoT. Eg: the user bump his phone to pay the  ticket for concert and as soon as the transaction is completed it will open the door.
5) See the history of transactions from all accounts 

This solution requires no extra hardware (Sender and receiver will use their mobile phones) and enables the product to be used and implemented immediately. It’s compatible for iOS and Android

The “payment procedure” is:
1) User 1 open the apps and ask to receive 50$
2) User 2 open the apps and ask to pay
3a) User 1 + 2 bump the mobile devices. Then User 2 is asked to confirm that he/she wants to pay the amount. We will add some extra layer of security later (like fingerprint validation)
3b) Alternative to 3a we are going to use QR Codes and the User 2 will scan the code and receive the request
3c) Apple has enable the NFC on iOS 11 so we are planning to integrate that also.



How to use our App



Webservice:
Deploy the webservices (wwwroot folder) under a php server. 
Use it with the folwoing functions http://localhost/api/

Example:
Post parameters
method: getUserData
api: 1
userid: 3



Mobile App:
Compile and run with Xamarin Studio. Each platform needs to be run seperatly (iOS / Android)
