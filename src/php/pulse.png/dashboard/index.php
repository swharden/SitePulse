<?php

$dir = new DirectoryIterator('../logs');
foreach ($dir as $fileinfo) {
    if (!$fileinfo->isDot()) {
        $filename = $fileinfo->getFilename();
        echo "<a href='../logs/$filename'>$filename</a>";
    }
}
