# LogExpertSharp
.NET Standard 2.0 Library to access LogExpert API. http://wiki.denexy.ru/le/API/API/

# Description of services
Any service requires an authentication token

## Accounting

```c#
const string token = "your token";
var accounting = new Accounting(token);
var users = await accounting.GetUsers();

foreach(var user in users)
{
    Console.WriteLine($"{user.Id} = {user.Name}");
}
```
