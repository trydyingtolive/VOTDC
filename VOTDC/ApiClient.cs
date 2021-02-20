using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using VOTDC.Data;
using VOTDC.Models;
using VOTDC.ViewModels;

namespace VOTDC
{
    public class ApiClient
    {
        public List<Verse> GetResponse(SearchViewModel search)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "d10161af8cf44f0c8267d571c682fda4");
            var date = search.StartDate.ToString("MM/dd/yyyy");
            var url = $"https://emfservicesstage-api.azure-api.net/bible/v1/getversesbydate?siteId=1&startdate={date}&PageSize={search.PageSize}";
            var result = client.GetStringAsync(url).GetAwaiter().GetResult();

            var response = JsonConvert.DeserializeObject<ApiResponse>(result);
            var verses = response.Verses;

            var dataContext = new DataContext();
            var verseIds = verses.Select(v => v.Id);
            var existingVerseIds = dataContext.Verses.Where(v => verseIds.Contains(v.Id)).Select(v => v.Id).ToList();
            var newVerses = verses.Where(v => !existingVerseIds.Contains(v.Id));

            foreach (var verse in newVerses)
            {
                if (ValidateVerse(verse))
                {
                    dataContext.Verses.AddRange(verse);
                }
            }
            dataContext.SaveChanges();

            return verses;
        }

        
        private bool ValidateVerse(Verse verse)
        {
            //This always is true.
            //If this was the real world just shoving something in the db from the internet is proably a bad idead
            return true ;
        }
    }
}
