using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private APIHelper apiHelper;
        public string Env { get; set; }
        public string APIUri { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            apiHelper = new APIHelper(logger, configuration);
        }

        public void OnGet()
        {
            List<SettingValue> settings = apiHelper.GetSettings();
            this.Env = settings.Where(x => x.Key == "env").FirstOrDefault().Value;
            this.APIUri = _configuration.GetSection("API:uri").Value;
        }
    }
}
