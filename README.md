# SharpUpBit
UpBit API with C#

# How to Use?
```CSharp
using SharpUpBit;    // Include this.
using System;

namespace Test {
  internal class Program {
    static void Main(string[] args) {
      string accessKey = "";
      string secretKey = "";

      // Get UpBit OpenAPI Info
      UpBit upbit = new UpBit(accessKey, secretKey);
      var v = upbit.GetMyAPIList();
      Console.WriteLine(v.Result[0].ExpireAt);
    }
  }
}
```
`2023-10-07 오후 10:35:59`

# Library
* [System.IdentityModel.Tokens.Jwt](https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet)
* [Newtonsoft.Json](https://www.newtonsoft.com/json)
