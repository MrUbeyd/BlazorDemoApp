using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace BlazorDemoApp.Models
{
    public class GoogleReCaptchaServices
    {
        private ReCHAPTCHASettings _settings;

        public GoogleReCaptchaServices(IOptions<ReCHAPTCHASettings> settings)
        {
            // inject keys from appsettings.json
            _settings = settings.Value;
        }



        public virtual async Task<GoogleResponse> VerifyReCaptcha(string _token)
        {
            GoogleReCaptchaData _data = new GoogleReCaptchaData
            {
                response = _token,
                secret = _settings.ReCHAPTCHA_secret_key
            };

            HttpClient client = new HttpClient();

            //rspns = responsa but different than GoogleReChaptchaData response
            var rspns = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?=secret{_data.secret}&response={_data.response}");

            var convertResponse = JsonConvert.DeserializeObject<GoogleResponse>(rspns);

            return convertResponse;



        }
    }

    public class GoogleReCaptchaData
    {
        public string response { get; set; } //Token

        public string secret { get; set; } //Secret Key
    }

    public class GoogleResponse
    {
        public bool success { get; set; }

        public double score { get; set; }

        public string action { get; set; }

        public DateTime challenge_ts { get; set; }

        public string hostname { get; set; }
    }
}