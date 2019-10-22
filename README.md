# blazor3
Encrypt Decrypt File text

This project is to try the cryptograf Library dll. and it's working in .Net Core 

The file name is /Pages/EncryptView.razor

We can define multiple paths for the page. Line 1
```c#
@page "/View"
@page "/"

```
--------------------------------------------------------------------------------------------------------
we can inject clases Singleton for use in many pages. Line 4
```c#
@using blazor3.providers;
@inject ListData dataService;
```
--------------------------------------------------------------------------------------------------------

We can add classes in any Element with @(condition). Line 12
```html
<input type="text" class='form-control @((strkey.Length==16? "" : "border border-danger"))' 
    maxlength="16"  @bind="strkey" id="exampleFormControlInput1" 
    placeholder="key 16 chars" />
```


--------------------------------------------------------------------------------------------------------
Its possible to use a condition to the attribute disable and add event onclick .Line 19 and 22
```html
<button type="button" class="btn btn-success m-3" disabled="@(strkey.Length!=16)" @onclick=EncryptData>Encrypt</button>
<button type="button" class="btn btn-warning m-3" disabled="@(strkey.Length!=16)" @onclick=DecryptData>Decrypt</button>
```
```c#

    public string strkey{get; set;} = "";
    void EncryptData(){
        encryptedText=dataService.Encrypt(plainText,strkey);
    }
    void DecryptData(){
        plainText=dataService.Decrypt(encryptedText,strkey);
    }
```
--------------------------------------------------------------------------------------------------------
for Bind the input to variable in c# code is with @bind="attribute" Line 30, 37  c# code Line 47,48
```html
<textarea class="form-control m-3" @bind="plainText" id="exampleFormControlTextarea1" rows="3"></textarea>

<textarea class="form-control m-3" @bind="encryptedText" id="exampleFormControlTextarea2" rows="3"></textarea>
```
```c#
    public string plainText{get; set;} = "";
    public string encryptedText{get; set;} = "";
```
--------------------------------------------------------------------------------------------------------

Encrypt and Decrypt Class is in /providers/ListData.cs 
```c#
 public class ListData : IListData
    {
        public List<string> lista { get; set; } = new List<string>();

        private byte[] base64tosArrayByte(string strbase64)
        {

            byte[] decodedBytes = Convert.FromBase64String(strbase64);
            return decodedBytes;
        }
        public string Decrypt(string plaintext, string strkey)
        {
            try
            {
                string strResult = "";
                if (strkey.Length > 16)
                    strkey = strkey.Substring(0, 16);
                string straux = "ADOTRMDWOD1QWELK";

                byte[] key = System.Text.Encoding.UTF8.GetBytes(strkey);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(straux);
                byte[] encryptedText = base64tosArrayByte(plaintext);

                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.IV = IV;
                    aes.Key = key;

                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var ms = new System.IO.MemoryStream(encryptedText))
                    {
                        using (var csDecrypt = new System.Security.Cryptography.CryptoStream(ms, decryptor,
                        System.Security.Cryptography.CryptoStreamMode.Read))
                        {
                            using (var swDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                strResult = swDecrypt.ReadToEnd();
                                Console.WriteLine("decrypt : " + strResult);
                            }
                        }
                    }
                }
                return strResult;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }

        public string Encrypt(string plaintext, string strkey)
        {
            try
            {
                string strResult = "";
                if (strkey.Length > 16)
                    strkey = strkey.Substring(0, 16);
                string straux = "ADOTRMDWOD1QWELK";

                byte[] key = System.Text.Encoding.UTF8.GetBytes(strkey);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(straux);
                byte[] encrypted;
                
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.IV = IV;
                    aes.Key = key;
                    var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (var ms = new System.IO.MemoryStream())
                    {
                        using (var csEncrypt = new System.Security.Cryptography.CryptoStream(ms, encryptor,
                        System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plaintext);
                            }
                            encrypted = ms.ToArray();
                            this.lista = new List<string>();
                            strResult = Convert.ToBase64String(encrypted);
                        }
                    }
                }
                return strResult;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }
    }
```



