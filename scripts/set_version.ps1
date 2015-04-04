param(
 [string]$version
)

"Setting version to $version"

$assemblyInfoPath = "..\src\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor\Properties\AssemblyInfo.cs"
$content = Get-Content $assemblyInfoPath

$content = $content -replace "AssemblyVersion\(""(.*)""\)", "AssemblyVersion(""$version.*"")"
$content = $content -replace "AssemblyFileVersion\(""(.*)""\)", "AssemblyFileVersion(""$version.*"")"
$content = $content -replace "AssemblyInformationalVersionAttribute\(""(.*)""\)", "AssemblyInformationalVersionAttribute(""$version"")"

$content > $assemblyInfoPath
