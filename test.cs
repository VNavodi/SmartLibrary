using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var client = new HttpClient();
        var json = "{\"id\":0,\"name\":\"Test Author\",\"biography\":\"\",\"nationality\":\"\"}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync("https://localhost:7022/api/Authors", content);
        Console.WriteLine((int)response.StatusCode);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
