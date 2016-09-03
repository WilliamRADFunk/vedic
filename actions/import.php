<?php

include_once "Table.php";
include_once "Column.php";

$dbname = $_POST['dbname'];
$password = $_POST['password'];
$username = $_POST['username'];
$hostname = $_POST['hostname'];
echo "\n\nDBNAME: " . $dbname . "\n\n";
echo "\n\nPASSWORD: " . $password . "\n\n";
echo "\n\nUSERNAME: " . $username . "\n\n";
echo "\n\nHOSTNAME: " . $hostname . "\n\n";
// Create connection
$conn = new mysqli($hostname, $username, $password, $dbname);
// Check connection
if ($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error);
}
// User sent an empty string.
if($dbname === "")
{
	echo "Error: empty dbname";
	$conn->close();
}
else if($password === "")
{
	echo "Error: empty password";
	$conn->close();
}
else if($username === "")
{
	echo "Error: empty username";
	$conn->close();
}
else if($hostname === "")
{
	echo "Error: empty hostname";
	$conn->close();
}
// User sent a SELECT query
else
{
	$getTables = "select table_name from information_schema.tables where table_schema='" . $dbname . "'";
	$result = $conn->query($getTables);
	// Query was unvalid
	if(!$result)
	{
		echo "table_names fetch failed: " . $conn->error;
		$conn->close();
	}
	else
	{
		$tableNames = [];
		// Grabs all table names in database.
		while ( $db_row = mysqli_fetch_row($result) )
		{
			for($j = 0; $j < mysqli_num_fields($result); $j++)
			{
				array_push($tableNames, $db_row[$j]);
				echo $db_row[$j];
			}
		}
		$tables = [];
		// Gets all columns and field data for each of the tables.
		for($i = 0; $i < count($tableNames); $i++)
		{
			$tab = new Table;
			$tab->name = $tableNames[$i];

			for($k = 0; $k < mysqli_num_fields($result); $k++)
			{
				$col = new Column;
				$field_info = mysqli_fetch_field($result);
				$col->name = $field_info->name;
				echo "    " . $field_info->name . "    ";

				array_push($tab->columns, $col);
			}

			$counter = 0;
			while ( $db_row = mysqli_fetch_row($result) )
			{
				for($j = 0; $j < mysqli_num_fields($result); $j++)
				{
					array_push($tab->columns[$counter], $db_row[$j]);
					echo "    " . $db_row[$j] . "    ";
				}
				$counter++;
			}
			array_push($tables, $tab);
		}
		$conn->close();
	}
}

?>