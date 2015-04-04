echo Pushing version %1 to git

git add ../src/*/AssemblyInfo.cs

git commit -m "Release version %1"
git tag -a v%1 -m "Release %1"
git push
git push --tags
