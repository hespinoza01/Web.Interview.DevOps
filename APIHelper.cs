using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web
{
    public class APIHelper
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly string _apiUri;

        public APIHelper(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _apiUri = _configuration.GetSection("API:uri").Value;
        }

        public List<SettingValue> GetSettings()
        {
            List<SettingValue> responseAPI = default(List<SettingValue>);
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(_apiUri + "/settings").Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = response.Content.ReadAsStringAsync().Result;
                        responseAPI = JsonSerializer.Deserialize<List<SettingValue>>(content);
                    }
                }
            }

            return responseAPI;
        }

    }
}
