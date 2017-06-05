using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using XFConversion.UWP;

[assembly: Dependency(typeof(Authenticator))]
namespace XFConversion.UWP
{
    class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {

            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var platformParameters =
                new PlatformParameters(PromptBehavior.Auto, false);
            var uri = new Uri(returnUri);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParameters);
            return authResult;
        }
        public void Logout(string authority, string resource, string clientId)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                var cachedToken = authContext.TokenCache.ReadItems().FirstOrDefault(t => t.Authority == authority && t.ClientId == clientId && t.Resource == resource);

                if (cachedToken != null)
                {
                    authContext.TokenCache.DeleteItem(cachedToken);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}