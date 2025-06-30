import {Selector} from 'testcafe';
import * as IdentityServerAdminFunctions from '../Functions/Identity_Sever_Admin_Functions.js';
import * as StudioPortalFunctions from '../Functions/Studio_Portal_Functions.js';
require('../.testcaferc.json');

fixture `Studio Portal`
    .page `https://studios.celestialmediagroupllc.com/`;

test('Studio Portal - Sign Up', async t => {
    await t
    .maximizeWindow()

    //Create user
    var name = await StudioPortalFunctions.studioPortalSignUp(t)

    //Log out of Identity Server Admin with new user
    await t
    .navigateTo('https://isadmin.celestialmediagroupllc.com/')
    .click(Selector('a').withText('logout'))
    .click(Selector('button').withText('Yes'))
    .click(Selector('a').withText('logout'))

    //Log in with Admin user
    await IdentityServerAdminFunctions.identityServerAdminLogin(t)

    //Delete Automation user
    await IdentityServerAdminFunctions.identitySeverAdminDeleteUser(t, name)
    ;
});

test('Studio Portal - Log in', async t => {
    await t
    .maximizeWindow()

    //Create a new user
    var name = await StudioPortalFunctions.studioPortalSignUp(t)

    //Click Login link
    await t
    .click('#automation-loginlink')

    //Click Identity Server Login link
    .wait(250)
    .click(Selector('button').withText('LOGIN WITH IDENTITYSERVER'))

    //Verify Consent page displays
    await t
    .expect(Selector('h1').withText('SoundSesh Studios is requesting your permission')).ok()
    .click(Selector('button').withText('Yes, Allow'))

    //Verify Studio Portal page displays
    await t
    .expect(Selector('h1').withText('You currently do not have any registered studios.')).ok()

    //Log out of Identity Server Admin with new user
    //await t
    .navigateTo('https://isadmin.celestialmediagroupllc.com/')
    .click(Selector('a').withText('logout'))
    .click(Selector('button').withText('Yes'))
    .click(Selector('a').withText('logout'))
    
    await IdentityServerAdminFunctions.identityServerAdminLogin(t)
    await IdentityServerAdminFunctions.identitySeverAdminDeleteUser(t, name)
    ;
});