<?php

class Column
{
	public $fields = []; 
	public $name = ''; 
	
	function stringify()
	{
		$result = $name . ":{";
		for($i = 0; $i < count($fields); $i++)
		{
			$result .= $fields[$i];
			if($i !== count($fields) - 1)
			{
				$result .= ",";
			}
		}
		$result .= "}";
		return $result;
	}
}

?>