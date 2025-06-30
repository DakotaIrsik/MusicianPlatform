###################################################################################################################
###################################################################################################################
################################################################################################################### 
################################################ WARNING ##########################################################
###################################################################################################################
###################################################################################################################
###################################################################################################################
######## Make sure you trust the contents of this file before running it. as it will reopen as administrator. #####
######## Make sure you have an IIS website called "studioapi" and also that the ###################################
######## published api files are located in "E:\Applications\studioapi" ###########################################
######## Naturally you can change these parameters as you see it.##################################################
###################################################################################################################
###################################################################################################################
###################################################################################################################
###################################################################################################################



# Self-elevate the script
if (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] 'Administrator')) {
 if ([int](Get-CimInstance -Class Win32_OperatingSystem | Select-Object -ExpandProperty BuildNumber) -ge 6000) {
  $CommandLine = "-File `"" + $MyInvocation.MyCommand.Path + "`" " + $MyInvocation.UnboundArguments
  Start-Process -FilePath PowerShell.exe -Verb Runas -ArgumentList $CommandLine
  Exit
 }
}


C:\Windows\system32\inetsrv\appcmd stop site /site.name:studioapi

cd E:\Applications\studioapi
dotnet SoundSesh.Studios.API.dll /seed


C:\Windows\system32\inetsrv\appcmd start site /site.name:studioapi