export const environment = {
  environmentName: 'Production',
  applicationName: 'SoundSesh-Studios',
  production: true,
  identityServer: 'http://localhost:5000',
  apiUrl: 'http://localhost:3000/api/v1/',
  clientId: 'SoundSesh.Studios.Website',
  websiteUrl: 'http://localhost:4200/',
  callbackRoute: 'auth-callback',
  scopes: 'openid profile email roles SoundSesh.Studios.API',
  responseType: 'id_token token',
  tokenRefreshUri: 'silent-refresh.html'
};
