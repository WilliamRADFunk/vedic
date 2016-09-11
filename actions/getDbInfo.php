<?php

include_once "config.php";

$password = $_POST['password'];

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
	$getInfo = "select DatabaseName, HostName, UserName, Password from dbases order by 'index' ASC";
	$result = $conn->query($getInfo);

	if(!$result)
	{
		echo "database connect info fetch failed: " . $conn->error;
		$conn->close();
	}
	else
	{
		$reply = "";
		while ( $db_row = mysqli_fetch_row($result) )
		{
			for($j = 0; $j < mysqli_num_fields($result); $j++)
			{
				$reply .= $db_row[$j] . ",";
			}
		}
		echo $reply;
		$conn->close();
	}
}

?>