param(
 [string]$version
)

"Setting version to $version"
"TODO: Implement"

$assemblyInfoPath = "..\src\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor\Properties\AssemblyInfo.cs"
$content = Get-Content $assemblyInfoPath

"test" > $assemblyInfoPath
$content > $assemblyInfoPath
