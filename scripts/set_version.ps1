param(
 [string]$version
)

"Setting version to $version"

$assemblyInfoPath = "..\src\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor\Properties\AssemblyInfo.cs"
$content = Get-Content $assemblyInfoPath -Encoding UTF8

$content = $content -replace "AssemblyVersion\(""(.*)""\)", "AssemblyVersion(""$version.*"")"
$content = $content -replace "AssemblyFileVersion\(""(.*)""\)", "AssemblyFileVersion(""$version.*"")"
$content = $content -replace "AssemblyInformationalVersionAttribute\(""(.*)""\)", "AssemblyInformationalVersionAttribute(""$version"")"

Set-Content $assemblyInfoPath $content -Encoding UTF8
