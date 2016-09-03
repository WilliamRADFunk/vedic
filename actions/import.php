<?php

include_once "Table.php";
include_once "Column.php";

$dbname = $_POST['dbname'];
$password = $_POST['password'];
$username = $_POST['username'];
$hostname = $_POST['hostname'];
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
			}
		}
		$tables = [];
		// Gets all columns and field data for each of the tables.
		for($i = 0; $i < count($tableNames); $i++)
		{
			$getTableData = "select * from " . $tableNames[$i];
			$result = $conn->query($getTableData);
			if(!$result)
			{
				echo "table data fetch failed: " . $conn->error;
				$conn->close();
			}
			else
			{
				$tab = new Table;
				$tab->name = $tableNames[$i];

				for($k = 0; $k < mysqli_num_fields($result); $k++)
				{
					$col = new Column;
					$field_info = mysqli_fetch_field($result);
					$col->name = $field_info->name;
					array_push($tab->columns, $col);
				}

				while ( $db_row = mysqli_fetch_row($result) )
				{
					for($j = 0; $j < mysqli_num_fields($result); $j++)
					{
						array_push($tab->columns[$j]->fields, $db_row[$j]);
					}
				}
				array_push($tables, $tab);
			}
		}

		$jsonString = "database:{";
		for($a = 0; $a < count($tables); $a++)
		{
			$jsonString .= $tables[$a]->name . ":{";
			for($b = 0; $b < count($tables[$a]->columns); $b++)
			{
				$jsonString .= $tables[$a]->columns[$b]->name . ":[";
				for($c = 0; $c < count($tables[$a]->columns[$b]->fields); $c++)
				{
					$jsonString .= $tables[$a]->columns[$b]->fields[$c];
					if($c !== count($tables[$a]->columns[$b]->fields) - 1)
					{
						$jsonString .= ",";
					}
				}
				$jsonString .= "]";	

				if($b !== count($tables[$a]->columns) - 1)
				{
					$jsonString .= ",";
				}
			}
			$jsonString .= "}";

			if($a !== count($tables) - 1)
			{
				$jsonString .= ",";
			}
		}
		$jsonString .= "}";
		echo $jsonString;
		$conn->close();
	}
}

?>