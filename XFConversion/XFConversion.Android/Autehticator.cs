using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using XFConversion.Droid;

[assembly: Dependency(typeof(Authenticator))]
namespace XFConversion.Droid
{
    class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

                var uri = new Uri(returnUri);
                var platformParams = new PlatformParameters((Activity)Forms.Context);
                var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
                return authResult;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}