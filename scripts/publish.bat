call nuget_pack.bat ..\src\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor\Manisero.PerformanceMonitor.csproj
call nuget_push.bat Manisero.PerformanceMonitor
del Manisero.PerformanceMonitor.*.nupkg
