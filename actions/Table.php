<?php
include_once "Column.php";

class Table
{
	public $columns = []; 
	public $name = ''; 
	
	function stringify()
	{
		$result = $name . ":{";
		for($i = 0; $i < count($columns); $i++)
		{
			$result .= $columns[$i]->stringify();
			if($i !== count($columns) - 1)
			{
				$result .= ",";
			}
		}
		$result .= "}";
		return $result;
	}
}

?>