// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    suite: 'SoundSesh',
    environmentName: 'QA',
    identityServer: 'https://is.celestialmediagroupllc.com',
    clientId: 'SoundSesh.Portal',
    websiteUrl: 'https://qa.celestialmediagroupllc.com/',
    callbackRoute: 'auth-callback',

    studioApi: {
      url: 'https://studioapi.celestialmediagroupllc.com/',
      apiExtension: 'api/',
      version: 'v1/',
      scope: 'SoundSesh.Studios.API'
    },
    musicianApi: {
      url: 'https://musicianapi.celestialmediagroupllc.com/',
      apiExtension: 'api/',
      version: 'v1/',
      scope: 'SoundSesh.Musician.API'
    },

    scopes: 'openid profile email roles ',
    responseType: 'id_token token',
    tokenRefreshUri: 'silent-refresh.html'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
