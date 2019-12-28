# How to use:
run the `DotNet Injector.exe` file and wait for it to load, use the window selector/finder to select the process to inject itself. after a successful injection you can load your own application to load in the targeted application.

note: This injector works best on .net applications (VB.NET, C#, and some more)


# Building
simply run the Build all.bat file and all the files will be collected in the Output folder

# Modify and run
Head project is the `DotNet Injector` project. Place the required exe and dll files in the bin file from that project, from there you should be able to test it.

required files: 
- AppConnector32.exe
- AppConnector64.exe
- ManagedInjector32.dll
- ManagedInjector64.dll


# AppConnector
this application is an console application that does the injection part

# ManagedInjector
this file is being injected, this file runs an other part of the injectors code `TakeTwo`

# DotNet Injector
This application mainly helps identifying different processes with a quick and easy windowfinder/windowselector

## Might be added:
A way to select a process from a list if the target process doesn't have a window

### Main idea behind this:
The idea to make my own was when I saw the usage of GrayDragon from DigitalBodyguard, I was able to get (some of) the sourcecode and made my own version based on his.
