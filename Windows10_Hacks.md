# When did my system last reboot?
* Get-EventLog System | Where-Object {$_.EventID -eq "1074" -or $_.EventID -eq "6008" -or $_.EventID -eq "1076"} | ft Machinename, TimeWritten, UserName, EventID, Message -AutoSize -Wrap
* systeminfo | find "System Boot Time"

