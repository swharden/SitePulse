<?php

// serve a real image
header('Content-type: image/png');
readfile('pixel.png');

// log the encounter
$ip2 = $_SERVER['REMOTE_ADDR'];
$query = $_SERVER['QUERY_STRING'];
$timestamp = gmdate("Y-m-d,h-i-s");
$line = "$timestamp,$ip2,$query";
$filename = gmdate("Y-m-d") . ".txt";

if (!is_dir("logs"))
    mkdir("logs");
file_put_contents("logs/$filename", $line . PHP_EOL, FILE_APPEND | LOCK_EX);
