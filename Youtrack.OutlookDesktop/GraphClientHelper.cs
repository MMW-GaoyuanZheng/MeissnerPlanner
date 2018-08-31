using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Collections.Generic;

namespace Meissner.MicrosoftPlanner
{
    class TestUser : IUser
    {
        public string DisplayableId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string IdentityProvider { get; set; }
        public string Identifier { get; set; }
    }
    public static class GraphClientHelper
    {
        private static string ClientId = "e23d2c58-5c43-4974-b11c-eac0b5fd8b14";
        private static String[] Scopes = {
                        "User.Read",
                        "User.ReadBasic.All",
                        "Mail.Send",
                        "Mail.Read",
                        "Group.ReadWrite.All",
                        "Sites.Read.All",
                        "Sites.ReadWrite.All",
                        "Directory.AccessAsUser.All",
                        "Files.ReadWrite",
                        "Files.ReadWrite.AppFolder"
                        };
        private static string username = "Tom@dieterdruckguss.onmicrosoft.com";
        private static string password = "Gao654321";

        public static PublicClientApplication IdentityClientApp = new PublicClientApplication(ClientId);

        public static string TokenForUser = null;
        public static DateTimeOffset Expiration;

        private static GraphServiceClient graphClient = null;

        // Get an access token for the given context and resourceId. An attempt is first made to 
        // acquire the token silently. If that fails, then we try to acquire the token by prompting the user.
        public static GraphServiceClient GetAuthenticatedClient()
        {
            if (graphClient == null)
            {
                // Create Microsoft Graph client.
                try
                {
                    //string authorityUri = "https://login.windows.net/common/oauth2/authorize";
                    //AuthenticationContext authContext = new AuthenticationContext(authorityUri);
                    //AuthenticationResult token = authContext.AcquireToken(resourceUri, clientId, new Uri(redirectUri), PromptBehavior.RefreshSession);

                    graphClient = new GraphServiceClient(
                        "https://graph.microsoft.com/v1.0",
                        new DelegateAuthenticationProvider(
                             async(requestMessage) =>
                            {
                                //var token = await GetTokenForUserAsync(IdentityClientApp);
                                var token = await GetTokenForUserAsync(IdentityClientApp);
                                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                                //requestMessage.Content
                            }));
                    return graphClient;
                }

                catch (Exception ex)
                {
                    Debug.WriteLine("Could not create a graph client: " + ex.Message);
                }
            }
            return graphClient;
        }

        /// <summary>
        /// Get Token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> GetTokenForUserAsync(PublicClientApplication clientApp)
        {
            AuthenticationResult authResult;
            try
            {
                if (clientApp.Users.Count() > 0)
                {
                    //TestUser user = new TestUser()
                    //{
                    //    DisplayableId = "Tom@dieterdruckguss.onmicrosoft.com",
                    //    Name = "Gaoyuan Zheng",
                    //    IdentityProvider = "https://login.microsoftonline.com/f10fab32-7ad4-4f38-b985-dffafd81fb61/v2.0",
                    //    Identifier = "NWU3ZWUxMjgtMDc0Ni00ZGM2LTg5ZWQtYWViYTVlNTE2MWVj.ZjEwZmFiMzItN2FkNC00ZjM4LWI5ODUtZGZmYWZkODFmYjYx",
                    //};
                    //authResult = await IdentityClientApp.AcquireTokenSilentAsync(Scopes, user);

                    authResult = await IdentityClientApp.AcquireTokenSilentAsync(Scopes,clientApp.Users.FirstOrDefault());
                    TokenForUser = authResult.AccessToken;
                }
                else
                {
                    throw new Exception("Get correct access token");
                }
            }
            catch (Exception)
            {
                if (TokenForUser == null || Expiration <= DateTimeOffset.UtcNow.AddMinutes(5))
                {
                    TestUser user = new TestUser()
                    {
                        DisplayableId = "Tom@dieterdruckguss.onmicrosoft.com",
                        Name = "Gaoyuan Zheng",
                        Password="Gao654321",
                        IdentityProvider = "https://login.microsoftonline.com/f10fab32-7ad4-4f38-b985-dffafd81fb61/v2.0",
                        //Identifier = "NWU3ZWUxMjgtMDc0Ni00ZGM2LTg5ZWQtYWViYTVlNTE2MWVj.ZjEwZmFiMzItN2FkNC00ZjM4LWI5ODUtZGZmYWZkODFmYjYx",
                    };
                    authResult = await IdentityClientApp.AcquireTokenAsync(Scopes, user.DisplayableId,);
                    TokenForUser = authResult.AccessToken;
                    Expiration = authResult.ExpiresOn;
                }
            }
            //https://login.microsoftonline.com/f10fab32-7ad4-4f38-b985-dffafd81fb61/v2.0
            //https://login.microsoftonline.com/f10fab32-7ad4-4f38-b985-dffafd81fb61/v2.0"
            //Gaoyuan Zheng
            //"NWU3ZWUxMjgtMDc0Ni00ZGM2LTg5ZWQtYWViYTVlNTE2MWVj.ZjEwZmFiMzItN2FkNC00ZjM4LWI5ODUtZGZmYWZkODFmYjYx"
            //"NWU3ZWUxMjgtMDc0Ni00ZGM2LTg5ZWQtYWViYTVlNTE2MWVj.ZjEwZmFiMzItN2FkNC00ZjM4LWI5ODUtZGZmYWZkODFmYjYx"
            //"Tom@dieterdruckguss.onmicrosoft.com"
            return TokenForUser;
        }
        
        /// <summary>
        /// Signs the user out of the service.
        /// </summary>
        public static void SignOut()
        {
            graphClient = null;
            TokenForUser = null;
        }
    }
}