using System.Net.Http;
using VOTDC.ViewModels;

namespace VOTDC
{
    public class ApiClient
    {
        public string GetResponse(Search search)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "d10161af8cf44f0c8267d571c682fda4");
            var date = search.StartDate.ToString("MM/dd/yyyy");
            var url = $"https://emfservicesstage-api.azure-api.net/bible/v1/getversesbydate?siteId=1&startdate={date}&PageSize={search.PageSize}";
            var result = client.GetStringAsync(url).GetAwaiter().GetResult();
            return result;
        }
    }
}
