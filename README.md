# ReportOTC
A one-time authorization code (OTC) is a code that is valid to authenticate the user identity for only one session. 

# Example
```C#
var otc = new OTC();

Task.Run(async () => {
  var data = await otc.getOneTimeCode();
  Console.WriteLine(data);
});
```
