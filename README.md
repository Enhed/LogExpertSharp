# LogExpertSharp
.NET Standard 2.0 Library to access LogExpert API. https://logexpert.ru/
# Accounting

```c#
const string token = "your token";
var accounting = new Accounting(token);
var users = await accounting.GetUsers();

foreach(var user in users.Take(10))
{
    Console.WriteLine($"{user.Id} = {user.Name}");
}
```
