<?php
$dbname = $_POST['dbname'];
$hostname = $_POST['hostname'];
$username = $_POST['username'];
$password = $_POST['password'];

// Create connection
$conn = new mysqli($hostname, $username, $password, $dbname);
// Check connection
if ($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error);
}

$query = $_POST['query'];
echo "\n\nQUERY: " . $query . "\n\n";
// User sent an empty string.
if($query === "")
{
	echo "Error: empty query";
	$conn->close();
}
// User sent a SELECT query
elseif(strlen($query) >= 6 && strpos(strtoupper($query), "SELECT") !== false)
{
	$result = $conn->query($query);
	// Query was unvalid
	if(!$result)
	{
		echo "Invalid query: " . $conn->error;
		$conn->close();
	}
	else
	{
		echo "SELECT QUERY RESULTS";
		echo "\n\n******************************************************************************************\n";
		for($i = 0; $i < mysqli_num_fields($result); $i++)
		{
			$field_info = mysqli_fetch_field($result);
			echo "    " . $field_info->name . "    ";
		}
		echo "\n******************************************************************************************\n";

		while ( $db_row = mysqli_fetch_row($result) )
		{
			for($j = 0; $j < mysqli_num_fields($result); $j++)
			{
				echo "    " . $db_row[$j] . "    ";
			}
			echo "\n------------------------------------------------------------------------------------------\n";
		}
		$conn->close();
	}
}
// User sent a query other than SELECT
elseif(strlen($query) >= 6)
{
	// Query was unvalid
	if($conn->query($query) !== TRUE)
	{
		echo "Invalid query: " . $conn->error;
		$conn->close();
	}
	else
	{
		if(strpos(strtoupper($query), "INSERT") === 0) echo "Your INSERT was a success";
		elseif(strpos(strtoupper($query), "DELETE") === 0) echo "Your DELETE was a success";
		elseif(strpos(strtoupper($query), "UPDATE") === 0) echo "Your UPDATE was a success";
		elseif(strpos(strtoupper($query), "DROP") === 0) echo "Your DROP was a success";
		$conn->close();
	}
}
else
{
	echo "Invalid query.";
	$conn->close();
}

?>