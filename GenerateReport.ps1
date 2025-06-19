$reportPath = "$PSScriptRoot\TestResults\report.cobertura.xml"

# Create coverage XML file
dotnet run --project "$PSScriptRoot\Lab_MooGameTests\Lab_MooGameTests.csproj" --coverage --coverage-output $reportPath --coverage-output-format cobertura


# Generate HTML report from the coverage XML file
$reportDirectory = "$PSScriptRoot\CoverageReport"

reportgenerator -reports:$reportPath -targetdir:$reportDirectory -reporttypes:Html

# Open the generated HTML report in the default web browser
Invoke-Item -Path:"$reportDirectory\index.html"