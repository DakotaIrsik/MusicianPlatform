testcafe

$DateStamp = get-date -uformat "%m-%d-%Y@%H_%M_%S"
Rename-Item -Path "Artifacts\Reports\report.html" -NewName "report-$DateStamp.html"