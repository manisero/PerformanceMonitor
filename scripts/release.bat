if "%1"=="" goto VERSION_ERROR else goto EXECUTE

:EXECUTE
echo Releasing version %1

call publish.bat
goto END

:VERSION_ERROR
echo Version not specified
goto END

:END
