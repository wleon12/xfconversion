using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace XFConversion
{
    public partial class MainPage : ContentPage
    {
        //public static string clientId = "your-client-id";
        //public static string authority = "https://login.windows.net/common";
        //public static string returnUri = "your-redirct-uri";
        //private const string graphResourceUri = "https://graph.windows.net";
        private AuthenticationResult authResult = null;
        public MainPage()
        {
            InitializeComponent();
          
        }
        
        

        private async void ButtonAuthenticate_OnClicked(object sender, EventArgs e)
        {
            await Authenticate();

        }

        private async Task Authenticate()
        {
            try
            {
                var auth = DependencyService.Get<IAuthenticator>();
                var data = await auth.Authenticate(Configuration.Authority, Configuration.Authority, Configuration.ClientId, Configuration.RedirectUri);
                var userName = data.UserInfo.GivenName + " " + data.UserInfo.FamilyName;
                await DisplayAlert("Token", userName, "Ok", "Cancel");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
