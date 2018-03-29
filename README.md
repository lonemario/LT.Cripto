# LT.Cripto
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/LT.Cripto/)

A Library to Crypt and Decript Text in a simply way

## Prerequisites

### .NETStandard 2.0

### Example 
```c#
//Generate an init 16 chars long vector for test
string IV = "abcdefghilmnopqr";
//Generate random Password 16 chars long
string Password = "myPasswordLen_16";
CriptoHelper cripto = new CriptoHelper(IV);

//Encrypt
var stringEncrypted = cripto.EncryptString("Hello", Password);

//Decrypt
var stringDecrypted = ripto.DecryptString(stringEncrypted, Password)

```