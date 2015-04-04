set csprojPath=..\src\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor.csproj
set packageName=Manisero.PerformanceMonitor

echo Publishing %packageName% package

call rebuild.bat %csprojPath%
call nuget_pack.bat %csprojPath%
call nuget_push.bat %packageName%
del %packageName%.*.nupkg
