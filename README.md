# WindowsServiceStartAndExitProcessMonitor

This is a Windows Service that monitors the start and exit of any process whose name and .exe file are known

To install the Windows Service use the following command in CMD (never install Services you don't trust):
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe C:\devwork\c#\ExitStartProcessMonitor\ExitStartProcessMonitor\bin\Debug\ExitStartProcessMonitor.exe

The service has a corresponding eventlog tied to it: ExitStartProcessMonitor.evx
