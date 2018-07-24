@echo off
taskkill /PID %2 /F >nul
echo Updating...
copy %1\
echo Cleaning up...
echo y | del %1\*.* 1>nul
rd %1\
echo Update complete!
echo Application will now restart!
timeout 5 >nul
start Run.bat