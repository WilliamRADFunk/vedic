<?php

include_once "config.php";

$dbname = $_POST['dbname'];
$password = $_POST['password'];
$username = $_POST['username'];
$hostname = $_POST['hostname'];
$dbNum = $_POST['dbNum'];

/*
if($password != $passwordMain)
{
	echo "Wrong Password";
	return;
}
*/

// Create connection
$conn = new mysqli($hostnameMain, $usernameMain, $passwordMain, $dbnameMain);
// Check connection
if ($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error);
}
// User sent a SELECT query
else
{
	$getInfo = "UPDATE dbases SET DatabaseName = '" . $dbname . "', HostName = '" . $hostname . "', Password = '" . $password . "', Username = '" . $username . "' WHERE dbNum = " . $dbNum;

	if($conn->query($getInfo) !== TRUE)
	{
		echo "Database connect info save failed: " . $conn->error;
		$conn->close();
	}
	else
	{
		echo "Success!";
		$conn->close();
	}
}

?>