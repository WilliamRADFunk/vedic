<?php
include_once "Table.php";
include_once "Column.php";

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
		$tab = new Table;
		$tab->name = "SelectTable";
		for($i = 0; $i < mysqli_num_fields($result); $i++)
		{
			// To the text box
			$field_info = mysqli_fetch_field($result);
			echo "    " . $field_info->name . "    ";
			// For table constructor
			$col = new Column;
			$col->name = $field_info->name;
			array_push($tab->columns, $col);
		}
		echo "\n******************************************************************************************\n";

		while ( $db_row = mysqli_fetch_row($result) )
		{
			for($j = 0; $j < mysqli_num_fields($result); $j++)
			{
				// To the text box
				echo "    " . $db_row[$j] . "    ";
				// For table constructor
				array_push($tab->columns[$j]->fields, $db_row[$j]);
			}
			echo "\n------------------------------------------------------------------------------------------\n";
		}
		// For table constructor
		$jsonString = "##SelectTable##:{";
		for($b = 0; $b < count($tab->columns); $b++)
		{
			$jsonString .= $tab->columns[$b]->name . ":[";
			for($c = 0; $c < count($tab->columns[$b]->fields); $c++)
			{
				$jsonString .= $tab->columns[$b]->fields[$c];
				if($c !== count($tab->columns[$b]->fields) - 1)
				{
					$jsonString .= ",";
				}
			}
			$jsonString .= "]";	

			if($b !== count($tab->columns) - 1)
			{
				$jsonString .= ",";
			}
			else
			{
				// End of column
			}
		}
		$jsonString .= "}";
		echo $jsonString;

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