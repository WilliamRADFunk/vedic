<?php
include_once "config.php";

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

echo "<MessageXML>";

echo "<Message>";
echo "<MsgID>Msg</MsgID>";
echo "<From>Funk</From>";
echo "<Email>contact@williamradfunk.com</Email>";
echo "<MsgDate>05/18/2016</MsgDate>";
echo "<MsgTime>01:01:01</MsgTime>";
echo "</Message>";

echo "</MessageXML>";

//Connecting to DB
$conn = new mysqli($hostname, $username, $password, $dbname); or die('Error connecting to mysql');
$result = $conn->query($query);

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "<MessageXML>";
while($row = mysqli_fetch_row($result))
{
 echo "<Data>"."<Email>{$row['email']}</Email>"."<Name>{$row['name']}</Name>"."<Password>{$row['password']}</Password>"."<Question>{$row['question']}</Question>"."<Answer>{$row['answer']}</Answer>"."</Data>";
}
echo "</MessageXML>";
$conn->close();
?>