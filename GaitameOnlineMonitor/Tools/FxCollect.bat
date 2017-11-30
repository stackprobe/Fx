CALL C:\home\bat\env\env.bat
C:\Factory\SubTools\nrun.exe /S %mimiko% Fx\snapshot
CALL C:\home\bat\env\sync.bat PULL UPDATE MOVE Fx
IF ERRORLEVEL 1 GOTO END
collcsv.exe
:END
