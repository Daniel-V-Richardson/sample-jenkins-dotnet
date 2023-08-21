using Volo.Abp.DependencyInjection;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ABPFramworkProject
{
    public class HelloService : ITransientDependency
    {
        public string SayHi()
        {
            return "SHLOK-AUDITOR";
        }

        public async Task<string> FetchFromChecklist()
        {
            var url = "http://checklist:3000/";
            var parameters = "";

            var client = new System.Net.Http.HttpClient(); 

            client.BaseAddress = new Uri(url);

            System.Net.Http.HttpResponseMessage response = await client.GetAsync(parameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            } 
            
            return "Not reachabale";
        }
    }
}
