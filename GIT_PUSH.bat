@echo off

setlocal

set GIT_PATH="..\..\RUN\Git\bin\git.exe"

set BRANCH = "origin"

GIT add -A
GIT  commit -am "Auto-committed on %date% - %time%"
GIT  pull %BRANCH%
GIT  push %BRANCH%

color 0B

set /p DUMMY=--------------Git push TAMAMLANDI---------------


