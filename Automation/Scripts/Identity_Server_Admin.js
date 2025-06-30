import { Selector } from 'testcafe';
import * as identityServerAdminFunctions from '../Functions/Identity_Sever_Admin_Functions.js';
require('../.testcaferc.json');

fixture `Identity Server Admin`
    .page`https://isadmin.celestialmediagroupllc.com/`;

test('Clients - Manage - Verify Clients Exist', async t => {
     
    await t.maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Verify the clients that are supposed to be there are there
    .expect(Selector('td').withText('SoundSesh.Venues.Website').exists).ok()
    .expect(Selector('td').withText('SoundSesh.Studios.Website').exists).ok()
    .expect(Selector('td').withText('SoundSesh.Fans.Website').exists).ok()
    .expect(Selector('td').withText('SoundSesh.Artists.Website').exists).ok()
    .expect(Selector('td').withText('CelestialMediaGroup.Admin.Website').exists).ok()
    ;
});

test('Clients - Manage - Able to add/edit/delete a new client', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Add new client
    .click(Selector('a').withText(' Add Client'))
    .typeText('#ClientId', "New Client ID")
    .typeText('#ClientName', "New Client Name")
    .click(Selector('h3').withText('Empty - Default'))
    .click(Selector('button').withText('Save Client'))

    //Get back to the list of clients
    .click(Selector('button').withText('Clients/Resources '))
    .click(Selector('a').withText('Clients'))

    //Verify new client is in the table
    .expect(Selector('td').withText('New Client ID').exists).ok()
    .expect(Selector('td').withText('New Client Name').exists).ok()

    //Edit client
    .click(Selector('a').withText('Edit'))
    .typeText('#ClientId', " Edited")
    .typeText('#ClientName', " Edited")
    .click(Selector('button').withText('Save Client'))

    //Get back to the list of clients
    .click(Selector('button').withText('Clients/Resources '))
    .click(Selector('a').withText('Clients'))

    //Verify edited client is in the table
    .expect(Selector('td').withText('New Client ID Edited').exists).ok()
    .expect(Selector('td').withText('New Client Name Edited').exists).ok()

    //Delete client
    .click(Selector('a').withText('Edit'))
    .click(Selector('a').withText('Delete Client'))
    .click(Selector('button').withText('Delete Client'))

    //Verify new client is not in the table
    .expect(Selector('td').withText('New Client ID').exists).notOk()
    .expect(Selector('td').withText('New Client Name').exists).notOk()
    ;
});

test('Clients - Manage - Basics - Verify SoundSesh.Venues.Website has correct Basic Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Venues.Website
    .click(Selector('[id="automation-SoundSesh.Venues.Website"]'))

    //Click Basics
    .click('#nav-basics-tab')

    //Verify the Allowed Scopes exist
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('roles').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('email').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('profile').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('openid').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('SoundSesh.Venues.API').innerText).ok()

    //Verify AllowAccessTokensViaBrowser is true
    .expect(Selector('input[id="AllowAccessTokensViaBrowser"]:checked')).ok()

    //Verify Redirect Uri is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('https://venues.celestialmediagroupllc.com/auth-callback').innerText).ok()

    //Verify Allowed Grant Types is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('implicit').innerText).ok()

    //Click manage Client Secrets
    .click(Selector('a').withText('Manage Client Secrets'))

    //Verify Shared Secret is correct
    .expect(Selector('td').withText('zDSKDp/XIkU3zANc7yHN0vfFOd+KNvrkSbwFcxX2Nqs=').exists).ok()
    ;
});
 
test('Clients - Manage - Basics - Verify SoundSesh.Studios.Website has correct Basic Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Studios.Website
    .click(Selector('[id="automation-SoundSesh.Studios.Website"]'))

    //Click Basics
    .click('#nav-basics-tab')

    //Verify the Allowed Scopes exist
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('roles').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('email').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('profile').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('openid').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('SoundSesh.Studios.API').innerText).ok()

    //Verify AllowAccessTokensViaBrowser is true
    .expect(Selector('input[id="AllowAccessTokensViaBrowser"]:checked')).ok()

    //Verify Redirect Uri is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('https://studios.celestialmediagroupllc.com/auth-callback').innerText).ok()

    //Verify Allowed Grant Types is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('implicit').innerText).ok()

    //Click manage Client Secrets
    .click(Selector('a').withText('Manage Client Secrets'))

    //Verify Shared Secret is correct
    .expect(Selector('td').withText('zDSKDp/XIkU3zANc7yHN0vfFOd+KNvrkSbwFcxX2Nqs=').exists).ok()
});
 
test('Clients - Manage - Basics - Verify SoundSesh.Fans.Website has correct Basic Settings', async t => {

    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Fans.Website
    .click(Selector('[id="automation-SoundSesh.Fans.Website"]'))

    //Click Basics
    .click('#nav-basics-tab')

    //Verify the Allowed Scopes exist
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('roles').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('email').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('profile').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('openid').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('SoundSesh.Fans.API').innerText).ok()

    //Verify AllowAccessTokensViaBrowser is true
    .expect(Selector('input[id="AllowAccessTokensViaBrowser"]:checked')).ok()

    //Verify Redirect Uri is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('https://fans.celestialmediagroupllc.com/auth-callback').innerText).ok()

    //Verify Allowed Grant Types is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('implicit').innerText).ok()

    //Click manage Client Secrets
    .click(Selector('a').withText('Manage Client Secrets'))

    //Verify Shared Secret is correct
    .expect(Selector('td').withText('zDSKDp/XIkU3zANc7yHN0vfFOd+KNvrkSbwFcxX2Nqs=').exists).ok()
    ;
});

test('Clients - Manage - Basics - Verify SoundSesh.Artists.Website has correct Basic Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Artists.Website
    .click(Selector('[id="automation-SoundSesh.Artists.Website"]'))

    //Click Basics
    .click('#nav-basics-tab')

    //Verify the Allowed Scopes exist
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('roles').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('email').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('profile').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('openid').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('SoundSesh.Artists.API').innerText).ok()

    //Verify AllowAccessTokensViaBrowser is true
    .expect(Selector('input[id="AllowAccessTokensViaBrowser"]:checked')).ok()

    //Verify Redirect Uri is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('https://artists.celestialmediagroupllc.com/auth-callback').innerText).ok()

    //Verify Allowed Grant Types is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('implicit').innerText).ok()

    //Click manage Client Secrets
    .click(Selector('a').withText('Manage Client Secrets'))

    //Verify Shared Secret is correct
    .expect(Selector('td').withText('zDSKDp/XIkU3zANc7yHN0vfFOd+KNvrkSbwFcxX2Nqs=').exists).ok()
    ;
});

test('Clients - Manage - Basics - Verify CelestialMediaGroup.Admin.Website has correct Basic Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for CelestialMediaGroup.Admin.Website
    .click(Selector('[id="automation-CelestialMediaGroup.Admin.Website"]'))

    //Click Basics
    .click('#nav-basics-tab')

    //Verify the Allowed Scopes exist
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('roles').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('email').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('profile').innerText).ok()
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('openid').innerText).ok()

    //Verify AllowAccessTokensViaBrowser is true
    .expect(Selector('input[id="AllowAccessTokensViaBrowser"]:checked')).ok()

    //Verify Redirect Uri is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('https://isadmin.celestialmediagroupllc.com/signin-oidc').innerText).ok()

    //Verify Allowed Grant Types is correct
    .expect(Selector('.card-body').find('.row').find('.col-12').find('span').withText('hybrid').innerText).ok()

    //Click manage Client Secrets
    .click(Selector('a').withText('Manage Client Secrets'))

    //Verify Shared Secret is correct
    .expect(Selector('td').withText('zDSKDp/XIkU3zANc7yHN0vfFOd+KNvrkSbwFcxX2Nqs=').exists).ok()
    ;
});

test('Clients - Manage - Authentication/Logout - Verify SoundSesh.Venues.Website has correct Authentication/Logout Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Venues.Website
    .click(Selector('[id="automation-SoundSesh.Venues.Website"]'))

    //Click Authentication/Logout
    .click('#nav-authentication-tab')

    //Verify Front Channel Logout Uri is correct
    var frontChannelValue = await Selector('#FrontChannelLogoutUri').value;

    await t
    .expect(frontChannelValue).eql('https://venues.celestialmediagroupllc.com')

    //Verify Post Logout Redirect Uris is correct
    .expect(Selector('.button__text').withText('https://venues.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Authentication/Logout - Verify SoundSesh.Studios.Website has correct Authentication/Logout Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Studios.Website
    .click(Selector('[id="automation-SoundSesh.Studios.Website"]'))

    //Click Authentication/Logout
    .click('#nav-authentication-tab')

    //Verify Front Channel Logout Uri is correct
    var frontChannelValue = await Selector('#FrontChannelLogoutUri').value;

    await t
    .expect(frontChannelValue).eql('https://studios.celestialmediagroupllc.com')

    //Verify Post Logout Redirect Uris is correct
    .expect(Selector('.button__text').withText('https://studios.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Authentication/Logout - Verify SoundSesh.Fans.Website has correct Authentication/Logout Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Fans.Website
    .click(Selector('[id="automation-SoundSesh.Fans.Website"]'))

    //Click Authentication/Logout
    .click('#nav-authentication-tab')

    //Verify Front Channel Logout Uri is correct
    var frontChannelValue = await Selector('#FrontChannelLogoutUri').value;

    await t
    .expect(frontChannelValue).eql('https://fans.celestialmediagroupllc.com')

    //Verify Post Logout Redirect Uris is correct
    .expect(Selector('.button__text').withText('https://fans.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Authentication/Logout - Verify SoundSesh.Artists.Website has correct Authentication/Logout Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Artists.Website
    .click(Selector('[id="automation-SoundSesh.Artists.Website"]'))

    //Click Authentication/Logout
    .click('#nav-authentication-tab')

    //Verify Front Channel Logout Uri is correct
    var frontChannelValue = await Selector('#FrontChannelLogoutUri').value;

    await t
    .expect(frontChannelValue).eql('https://artists.celestialmediagroupllc.com')

    //Verify Post Logout Redirect Uris is correct
    .expect(Selector('.button__text').withText('https://artists.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Authentication/Logout - Verify CelestialMediaGroup.Admin.Website has correct Authentication/Logout Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for CelestialMediaGroup.Admin.Website
    .click(Selector('[id="automation-CelestialMediaGroup.Admin.Website"]'))

    //Click Authentication/Logout
    .click('#nav-authentication-tab')

    //Verify Front Channel Logout Uri is correct
    var frontChannelValue = await Selector('#FrontChannelLogoutUri').value;

    await t
    .expect(frontChannelValue).eql('https://isadmin.celestialmediagroupllc.com/signout-oidc')

    //Verify Post Logout Redirect Uris is correct
    .expect(Selector('.button__text').withText('https://isadmin.celestialmediagroupllc.com/signout-callback-oidc').innerText).ok()
    ;
});

test('Clients - Manage - Token - Verify SoundSesh.Venues.Website has correct Token Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Venues.Website
    .click(Selector('[id="automation-SoundSesh.Venues.Website"]'))

    //Click Token
    .click('#nav-token-tab')

    //Verify Allowed Cors Origin is correct
    .expect(Selector('.button__text').withText('https://venues.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Token - Verify SoundSesh.Studios.Website has correct Token Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Studios.Website
    .click(Selector('[id="automation-SoundSesh.Studios.Website"]'))

    //Click Token
    .click('#nav-token-tab')

    //Verify Allowed Cors Origin is correct
    .expect(Selector('.button__text').withText('https://studios.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Token - Verify SoundSesh.Fans.Website has correct Token Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Fans.Website
    .click(Selector('[id="automation-SoundSesh.Fans.Website"]'))

    //Click Token
    .click('#nav-token-tab')

    //Verify Allowed Cors Origin is correct
    .expect(Selector('.button__text').withText('https://fans.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Token - Verify SoundSesh.Artists.Website has correct Token Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Artists.Website
    .click(Selector('[id="automation-SoundSesh.Artists.Website"]'))

    //Click Token
    .click('#nav-token-tab')

    //Verify Allowed Cors Origin is correct
    .expect(Selector('.button__text').withText('https://artists.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Token - Verify CelestialMediaGroup.Admin.Website has correct Token Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for CelestialMediaGroup.Admin.Website
    .click(Selector('[id="automation-CelestialMediaGroup.Admin.Website"]'))

    //Click Token
    .click('#nav-token-tab')

    //Verify Allowed Cors Origin is correct
    .expect(Selector('.button__text').withText('https://isadmin.celestialmediagroupllc.com').innerText).ok()
    ;
});

test('Clients - Manage - Consent Screen - Verify SoundSesh.Venues.Website has correct Consent Screen Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Venues.Website
    .click(Selector('[id="automation-SoundSesh.Venues.Website"]'))

    //Click Consent Screen
    .click('#nav-consent-tab')

    //Verify Client Uri is correct
    var clientUri = await Selector('#ClientUri').value

    await t.expect(clientUri).eql('https://venues.celestialmediagroupllc.com')
    ;
});

test('Clients - Manage - Consent Screen - Verify SoundSesh.Studios.Website has correct Consent Screen Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Studios.Website
    .click(Selector('[id="automation-SoundSesh.Studios.Website"]'))

    //Click Consent Screen
    .click('#nav-consent-tab')

    //Verify Client Uri is correct
    var clientUri = await Selector('#ClientUri').value

    await t.expect(clientUri).eql('https://studios.celestialmediagroupllc.com')
    ;
});

test('Clients - Manage - Consent Screen - Verify SoundSesh.Fans.Website has correct Consent Screen Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Fans.Website
    .click(Selector('[id="automation-SoundSesh.Fans.Website"]'))

    //Click Consent Screen
    .click('#nav-consent-tab')

    //Verify Client Uri is correct
    var clientUri = await Selector('#ClientUri').value

    await t.expect(clientUri).eql('https://fans.celestialmediagroupllc.com')
    ;
});

test('Clients - Manage - Consent Screen - Verify SoundSesh.Artists.Website has correct Consent Screen Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for SoundSesh.Artists.Website
    .click(Selector('[id="automation-SoundSesh.Artists.Website"]'))

    //Click Consent Screen
    .click('#nav-consent-tab')

    //Verify Client Uri is correct
    var clientUri = await Selector('#ClientUri').value

    await t.expect(clientUri).eql('https://artists.celestialmediagroupllc.com')
    ;
});

test('Clients - Manage - Consent Screen - Verify CelestialMediaGroup.Admin.Website has correct Consent Screen Settings', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Clients Manage button
    await t
    .click(Selector('a').withText('Manage'))

    //Click Edit for CelestialMediaGroup.Admin.Website
    .click(Selector('[id="automation-CelestialMediaGroup.Admin.Website"]'))

    //Click Consent Screen
    .click('#nav-consent-tab')

    //Verify Client Uri is correct
    var clientUri = await Selector('#ClientUri').value

    await t.expect(clientUri).eql('https://isadmin.celestialmediagroupllc.com')
    ;
});

test('API Resources - Manage - Verify API Resources Exist', async t =>{
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Api Resources Manage button
    await t
    .click(Selector('a').withText('Manage').nth(2))    

    //Verify APIs exist
    .expect(Selector('td').withText('SoundSesh.Venues.API').innerText).ok()
    .expect(Selector('td').withText('SoundSesh.Studios.API').innerText).ok()
    .expect(Selector('td').withText('SoundSesh.Fans.API').innerText).ok()
    .expect(Selector('td').withText('SoundSesh.Artists.API').innerText).ok()
    ;
});

test('API Resources - Manage - API Scopes - Verify SoundSesh.Venues.API has correct API Scopes', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Api Resources Manage button
    await t
    .click(Selector('a').withText('Manage').nth(2))  

    //Click Edit for SoundSesh.Venues.API
    .click(Selector('a').withText('Edit').nth(0))

    //Click Manage API Scopes
    .click(Selector('a').withText('Manage Api Scopes'))

    //Verfiy API Scopes are correct
    .expect(Selector('td').withText('SoundSesh.Venues.API').innerText).ok()
    ;
});

test('API Resources - Manage - API Scopes - Verify SoundSesh.Studios.API has correct API Scopes', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Api Resources Manage button
    await t
    .click(Selector('a').withText('Manage').nth(2))  

    //Click Edit for SoundSesh.Studios.API
    .click(Selector('a').withText('Edit').nth(1))

    //Click Manage API Scopes
    .click(Selector('a').withText('Manage Api Scopes'))

    //Verfiy API Scopes are correct
    .expect(Selector('td').withText('SoundSesh.Studios.API').innerText).ok()
    ;
});

test('API Resources - Manage - API Scopes - Verify SoundSesh.Fans.API has correct API Scopes', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Api Resources Manage button
    await t
    .click(Selector('a').withText('Manage').nth(2))  

    //Click Edit for SoundSesh.Fans.API
    .click(Selector('a').withText('Edit').nth(2))

    //Click Manage API Scopes
    .click(Selector('a').withText('Manage Api Scopes'))

    //Verfiy API Scopes are correct
    .expect(Selector('td').withText('SoundSesh.Fans.API').innerText).ok()
    ;
});

test('API Resources - Manage - API Scopes - Verify SoundSesh.Artists.API has correct API Scopes', async t => {
    await t
    .maximizeWindow()

    //Log into site
    await identityServerAdminFunctions.identityServerAdminLogin(t)

    //Click Api Resources Manage button
    await t
    .click(Selector('a').withText('Manage').nth(2))  

    //Click Edit for SoundSesh.Artists.API
    .click(Selector('a').withText('Edit').nth(3))

    //Click Manage API Scopes
    .click(Selector('a').withText('Manage Api Scopes'))

    //Verfiy API Scopes are correct
    .expect(Selector('td').withText('SoundSesh.Artists.API').innerText).ok()
    ;
}); 