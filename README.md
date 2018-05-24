# WindowsServiceStartAndExitProcessMonitor

This is a Windows Service that monitors the start and exit of any process whose name and .exe file are known (currently monitoring notepad/notepad.exe)

To install the Windows Service use the following command in CMD (never install Services you don't trust):
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe C:\devwork\c#\ExitStartProcessMonitor\ExitStartProcessMonitor\bin\Debug\ExitStartProcessMonitor.exe

The service has a corresponding eventlog tied to it: ExitStartProcessMonitor.evx

Note: the original concept of this project was to create a service that would listen for Visual Studio exit and would change the text editor font on exit. As of writing this (24/05/18) it is not clear how editing the font of the text editor can be done.
