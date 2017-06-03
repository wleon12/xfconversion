using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace XFConversion
{
    public class AuthenticationManager
    {
        private string _authority;
        private string _resource;
        private string _clientId;
        private Uri _redirectUri;
        private static IPlatformParameters _platformParameters;
        private string _accessToken;

        public async Task LoginAsync()
        {
            var authContext =
                new AuthenticationContext(_authority);

            var authResult =
                await authContext.AcquireTokenAsync(
                    _resource,
                    _clientId,
                    _redirectUri,
                    _platformParameters);

            var connectedUser = authResult.UserInfo;
            _accessToken = authResult.AccessToken;
        }

        public static void SetParameters(
            IPlatformParameters platformParameters)
        {
            _platformParameters = platformParameters;
        }
    }
}
