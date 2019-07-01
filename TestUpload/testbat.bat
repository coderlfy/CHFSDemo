set  p=0
:a
set /a p=%p%+1
start "" "TestUpload.exe"
if %p%==10 (
    goto :b
)
goto :a
:b