﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry
{
    public static class Constants
    {

#if Debug
        public static string ApplicationURL = @"https://mindurry-dev.azurewebsites.net/api";
         public static string NgcApp = @"https://ngc-app-dev.azurewebsites.net/web/auth/callback#id_token=";

#elif UAT
        public static string ApplicationURL = @"https://mindurry-uat.azurewebsites.net/api";
         public static string NgcApp = @"https://ngc-app-uat.azurewebsites.net/?token=";

#elif Release
        public static string ApplicationURL = @"https://mindurry-dev.azurewebsites.net/api";
        public static string NgcApp = @"https://ngc-app-dev.azurewebsites.net/web/auth/callback#id_token=";

#else
        public static string ApplicationURL = @"https://mindurry-dev.azurewebsites.net/api";
        public static string NgcApp = @"https://ngc-app-dev.azurewebsites.net/web/auth/callback#id_token=";
#endif

        public static string ProfileURL = ApplicationURL + "/api/profile";
        public static string GoogleApiKey = "AIzaSyC3NvEG3aSe66MbwmauzVUZ0SVNblL73CU";

        //Azure B2C Credentials
        public static readonly string Tenant = "appsmatsiya.onmicrosoft.com"; // Domain/resource name from AD B2C
        // DEv 
        public static readonly string ClientID = "c49be7ed-0cda-4111-bfbf-bb90d1ccb1f1"; // Application ID from AD B2C

        //Release PROD
        //public static readonly string ClientID = "818bd674-98dc-4786-a757-095c4bd925f7"; // Application ID from AD B2C

        public static readonly string PolicySignUpSignIn = "B2C_1_Mindurry_SignInSignUp"; // Policy name from AD B2C
        public static readonly string[] Scopes = { "" }; // Leave blank unless additional scopes have been added to AD B2C
        public static string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/"; // Doesn't require editing
        public static string Authority = $"{AuthorityBase}{PolicySignUpSignIn}"; // Doesn't require editing
        public static readonly string URLScheme = "mindurry"; // Custom Redirect URI from AD B2C (without ://auth/)
        public static readonly string RedirectUri = $"{URLScheme}://auth"; // Doesn't require editing
    }

}

