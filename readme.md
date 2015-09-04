Service Monitor
---------------
This code sample, installed as windows service, monitors a server by sending ping commands. If the server fails to reply to a ping this sample sends pre-generated Alerts using the ReachPlus Alerts SDK.

You may change configuration parameters such as username and  password in *ServerMonitor.exe.config*.  After generating binaries use *installutil.exe* to install the service.

**url** : This is URL of ReachPlus Server SDK

**user**: Username of ReachPlus Server SDK user

**password**: Password of ReachPlus Server SDK user

**alertfile**: File name of the alert payload

**serveraddress**: Address of the server to which we want to monitor


```xml
    <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
      <appSettings>
        <add key="url" value="http://192.168.5.166:47665/jobs"/>
        <add key="user" value="admin"/>
        <add key="password" value="alerts"/>
        <add key="alertfile" value="samplealert.xml"/>
        <add key="serveraddress" value="192.168.5.166"/>
      </appSettings>
    </configuration>
```
ReachPlus Alerts server 5.6.0(releasing soon) is required for this samle to work.

