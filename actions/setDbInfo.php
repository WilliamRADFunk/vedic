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
	$isRowExist = "SELECT EXISTS(SELECT * FROM dbases WHERE dbNum = " . $dbNum . ")";
	$getInfo = "";

	$result = $conn->query($isRowExist);

	if(!$result)
	{
		echo "Database connect info save failed: " . $conn->error;
		$conn->close();
	}
	else
	{
		$db_row = mysqli_fetch_row($result);
		if($db_row[0]) $getInfo = "UPDATE dbases SET DatabaseName = '" . $dbname . "', HostName = '" . $hostname . "', Password = '" . $password . "', UserName = '" . $username . "' WHERE dbNum = " . $dbNum;
		else $getInfo = "INSERT INTO dbases (dbNum, DatabaseName, HostName, Password, UserName) VALUES (" . $dbNum . ", '" . $dbname . "', '" . $hostname . "', '" . $password . "', '" . $username . "')";
	}

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