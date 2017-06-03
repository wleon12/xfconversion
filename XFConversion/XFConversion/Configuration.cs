using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFConversion
{
    public class Configuration
    {
        public const string ClientId = "6a26732c-369b-4609-a0d8-b650662deeb1"; // Put your mobile app ClientId
        public const string Authority = "https://login.windows.net/wleonjordangmail.onmicrosoft.com/"; // Default authority for Azure AD
        public const string Resource = "https://canadawebapi.azurewebsites.net"; // Put your API ID URI 
        public const string RedirectUri = "https://yourtenantname.com/what_you_want"; // Put your mobile app Redirect Uri (declared in Azure AD Apps)
        public const string ApiUri = "https://canadawebapi.azurewebsites.net/"; // Put you API actual URL
        public const string Proxy = "http://10.198.255.114:8888/"; // Put you API actual URL

    }
}
