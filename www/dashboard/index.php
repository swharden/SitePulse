<?php

header("Cache-Control: no-store, no-cache, must-revalidate, max-age=0");
header("Cache-Control: post-check=0, pre-check=0", false);
header("Pragma: no-cache");

error_reporting(E_ALL);

class LogFile
{
    public $hit_count = 0;
    public $user_count;
    public $user_count_hidden;

    function __construct($path)
    {
        $users = [];
        $users_seen = [];
        $users_hidden = [];

        $lines = explode(PHP_EOL, file_get_contents($path));
        foreach ($lines as $line) {
            $parts = explode(",", $line);

            if (count($parts) != 4) {
                continue;
            }
            $user = $parts[2];
            $users[$user] = null;
            $this->hit_count += 1;

            $code = $parts[3];

            if (str_contains($code, "banner-seen-")) {
                $users_seen[$user] = null;
            } else if (str_contains($code, "banner-hidden-")) {
                $users_hidden[$user] = null;
            }
        }

        $this->user_count = count($users);
        $this->user_count_hidden = count($users_hidden);
    }
}

$logFiles = [];
$dir = new DirectoryIterator('../logs');
foreach ($dir as $fileinfo) {
    if (!$fileinfo->isDot()) {
        $filename = $fileinfo->getFilename();
        $filePath = "../logs/$filename";
        $logFile = new LogFile($filePath);
        $logFiles[] = $logFile;
    }
}

?>

<!doctype html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Bootstrap demo</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <style>
        a {
            text-decoration: none;
        }

        a:hover {
            text-decoration: underline;
        }
    </style>
</head>

<body>
    <div class="container my-5">
        <h1>ScottPlot Banner Dashboard</h1>
        <ul class="my-4" style="list-style-type: none;">
            <?php
            foreach ($logFiles as $logFile) {
                echo "<li><a href='$filePath'>$filename</a> $logFile->hit_count hits across $logFile->user_count users ($logFile->user_count_hidden hidden)</li>";
            }
            ?>
        </ul>
        <footer class="my-5 text-center">
            <?php
            $rand = rand();
            echo "<div style='opacity: 0.5;'><code>$rand</code></div>";
            ?>
        </footer>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>

</html>