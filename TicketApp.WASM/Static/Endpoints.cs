using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp_UI.WASM.Static
{
    public static class Endpoints
    {
#if DEBUG
        public static string BaseUrl = "https://localhost:44382/";
#else
        public static string BaseUrl = "https://my-adress.azurewebsites.net/";
#endif
        public static string CustomerEndpoint = $"{BaseUrl}api/customers/";
        public static string TicketEndpoint = $"{BaseUrl}api/tickets/";
        public static string RegisterEndpoint = $"{BaseUrl}api/users/register/";
        public static string LoginEndpoint = $"{BaseUrl}api/users/login/";

    }
}
