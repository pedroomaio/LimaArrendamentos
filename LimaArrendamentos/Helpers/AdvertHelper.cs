using LimaArrendamentos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LimaArrendamentos.Helpers
{
    public class AdvertHelper : IAdvertHelper
    {
        public async Task<AdvertViewModel> GetAdApi()
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://backendadverts.herokuapp.com")
                };

                var response = await client.GetAsync("/api/getAds");

                var result = response.Content.ReadAsStringAsync().Result;

                // json -> .net -- gets json information and fills class data
                return JsonConvert.DeserializeObject<AdvertViewModel>(result);
            }
            catch
            {
                return null;
            }
        }
    }
}
