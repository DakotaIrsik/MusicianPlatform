﻿using System.Collections.Generic;

namespace CelestialMediaGroup.Admin.Configuration.Constants
{
    public class AuthenticationConsts
    {
        public const string IdentityAdminCookieName = "IdentityServerAdmin";
        public const string UserNameClaimType = "name";
        public const string SignInScheme = "Cookies";
        public const string OidcClientId = "CelestialMediaGroup.Admin.Website";
        public const string OidcClientSecret = "Zer0l0g!c";
        public const string OidcAuthenticationScheme = "oidc";
        public const string OidcResponseType = "code id_token";
        public static List<string> Scopes = new List<string> { ScopeOpenId, ScopeProfile, ScopeEmail, ScopeRoles };

        public const string ScopeOpenId = "openid";
        public const string ScopeProfile = "profile";
        public const string ScopeEmail = "email";
        public const string ScopeRoles = "roles";

        public const string RoleClaim = "role";

        public const string AccountLoginPage = "Account/Login";
        public const string AccountAccessDeniedPage = "/Account/AccessDenied/";
    }
}