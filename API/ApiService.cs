using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitx_Api;

namespace Boost
{
    public class ApiService
    {
        public Fitx_Api.Client _apiClient { get; private set; }

        public ApiService()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.0.155/") };
            _apiClient = new Fitx_Api.Client(httpClient);
        }
    }
}
