using System.Collections.Generic;
namespace blazor3.providers
{
    public interface IListData
    {
        List<string> lista { get; set; }
        string Encrypt(string plaintext, string strkey);
        string Decrypt(string plaintext, string strkey);
    }
}
