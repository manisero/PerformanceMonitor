echo Creating NuGet package from %1
nuget pack %1 -Prop Configuration=Release
