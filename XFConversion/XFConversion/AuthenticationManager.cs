using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace XFConversion
{
    public class AuthenticationManager
    {
        private static string _authority;
        private static string _resource;
        private static string _clientId;
        private static string _returnUri;
        private static IPlatformParameters _parameters;
        private string _accessToken;

        public static UserInfo UserInfo { get; private set; }

        public static void SetConfiguration(string authority, string resource, string clientId, string returnUri)
        {
            _authority = authority;
            _resource = resource;
            _clientId = clientId;
            _returnUri = returnUri;

            var authContext = new AuthenticationContext(_authority);
            authContext.TokenCache.Clear();
        }

        public static void SetParameters(IPlatformParameters parameters)
        {
            _parameters = parameters;
        }

        public async Task<bool> LoginAsync()
        {
            _accessToken = await GetAccessTokenAsync();
            return true;
        }

        public void Logout()
        {
            var auth = DependencyService.Get<IAuthenticator>();
            auth.Logout(_authority, _resource, _clientId);
            UserInfo = null;
            _accessToken = null;
           
        }

        public HttpClient CreateHttpClient()
        {
            #region for proxy to see traffic in fiddler 
            //var uri = new Uri(Configuration.ProxyForFiddler);
            //var handler = new HttpClientHandler
            //{
            //    Proxy = new Proxy(uri),
            //    UseProxy = true
            //};

            // var client = new HttpClient(handler);
            #endregion


            var client = new HttpClient();
            client.BaseAddress = new Uri(Configuration.ApiUri);

            if (!string.IsNullOrEmpty(_accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }

            return client;
        }

        private async Task<string> GetAccessTokenAsync()
        {
           
            var auth = DependencyService.Get<IAuthenticator>();
            var authResult = await auth.Authenticate(_authority, _resource, _clientId, _returnUri);
            UserInfo = authResult.UserInfo;
            return authResult.AccessToken;
        }
    }
}
