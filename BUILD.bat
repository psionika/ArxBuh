@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

REM Build 
nuget.exe restore ArxBuh.sln
"%programfiles(x86)%\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe" ArxBuh.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false 

"%programfiles(x86)%\Inno Setup 5\ISCC.exe" ArxBuh.Installer/ArxBuh.iss

pause