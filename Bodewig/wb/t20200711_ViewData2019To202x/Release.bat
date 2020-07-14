C:\Factory\Tools\RDMD.exe /RC out

COPY /B Viewer\Viewer\bin\Release\*.exe out
COPY /B Viewer\Viewer\bin\Release\*.dll out

C:\Factory\Tools\xcp.exe doc out

ROBOCOPY C:\temp\Data2019To202x out\Data2019To202x /MIR

C:\Factory\SubTools\zip.exe /O out Viewer_Data2019To202x

PAUSE
