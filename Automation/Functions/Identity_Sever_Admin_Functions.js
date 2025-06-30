import {Selector} from 'testcafe';

export async function identityServerAdminLogin(t){
    //Log into site
    await t
    .typeText('#Username', "Admin")
    .typeText('#Password', "Pa$$word123")
    .click(Selector('button').withText('Login'))
};

export async function identitySeverAdminDeleteUser(t, name){
    //Delete Automation user
    await t
    .click(Selector('a').withText('IdentityServer Admin'))
    .click(Selector('a').withText('Manage').nth(4))
    .click(Selector('tbody').find('td').withText(name).sibling('td').find('a').nth(1))
    .click(Selector('button').withText('Delete User'))

    //Verify Automation user isn't in the user table
    .expect(Selector('tbody').find('tr').find('td').withText(name).exists).notOk()
    ;
}