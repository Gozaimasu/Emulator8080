Remove-Item "*\TestResults" -Recurse;
dotnet test --collect:"XPlat Code Coverage";
$files = Get-ChildItem "coverage.cobertura.xml" -Recurse;
foreach	($file in $files) {
	reportgenerator -reports:$file.FullName -targetdir:"coveragereport" -reporttypes:Html;
}