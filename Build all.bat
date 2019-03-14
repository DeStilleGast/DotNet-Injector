@echo off

Title prepairing
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"

mkdir Output

title building ManagedInjector
devenv "DotNet Injector.sln" /build release /project ManagedInjector
move ManagedInjector\Win32\Release\ManagedInjector.dll .\Output\ManagedInjector32.dll

devenv "DotNet Injector.sln" /build release /projectconfig "Release|x64" /project ManagedInjector
move /Y ManagedInjector\x64\Release\ManagedInjector.dll .\Output\ManagedInjector64.dll

title building AppConnector
devenv "DotNet Injector.sln" /build release /project AppConnector
move /Y AppConnector\bin\x86\Release\AppConnector.exe .\Output\AppConnector32.exe

devenv "DotNet Injector.sln" /build "release|x64" /project AppConnector
move /Y AppConnector\bin\Release\AppConnector.exe .\Output\AppConnector64.exe

title building DotNet Injector tool
devenv "DotNet Injector.sln" /build "release|Any CPU" /project "DotNet Injector"
move "DotNet Injector\bin\release\DotNet Injector.exe" ".\Output\DotNet Injector.exe"