# LogExpertSharp
.NET Standard 2.0 Library to access LogExpert API.  
Source api documentation http://wiki.denexy.ru/le/API/API/  
For more information on LogExpert visit https://logexpert.ru  
Support email support@logexpert.ru  

## Description of services
Any service requires an authentication token

### Accounting

```c#
using LogExpertSharp.Accounting;

const string token = "your token";
var accounting = new AccountingService(token);
var users = await accounting.GetUsers();

foreach(var user in users)
{
    Console.WriteLine($"{user.Id} = {user.Name}");
}
```

### Alerts

```c#

using LogExpertSharp.Alerts;

var alertService = new AlertService("token");
var unreadedAlerts = await alertService.GetUnreaded();

```