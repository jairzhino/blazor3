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
@inject IJSRuntime JsRuntime;
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
```
```c#
    public string plainText{get; set;} = "";
    public string encryptedText{get; set;} = "";
```
--------------------------------------------------------------------------------------------------------



