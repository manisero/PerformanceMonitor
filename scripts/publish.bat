SET csprojPath=..\src\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor.csproj
SET packageName=Manisero.PerformanceMonitor

call rebuild.bat %csprojPath%
call nuget_pack.bat %csprojPath%
call nuget_push.bat %packageName%
del %packageName%.*.nupkg
