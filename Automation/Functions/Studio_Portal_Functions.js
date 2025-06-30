import {Selector} from 'testcafe';
import * as GlobalFunctions from '../Functions/Global_Functions.js';

export async function studioPortalSignUp(t){
    //Sign Up for Studio Portal
    var name = "Automation" + GlobalFunctions.stringGenerator(t)

    await t
    .click(Selector('a').withText('Signup'))
    .typeText('#automation-name', name)
    .wait(250)
    .typeText('#automation-email', name + "@asdf.com")
    .typeText('#automation-password', "asdfA1!")
    .typeText('#automation-confirmpassword', "asdfA1!")
    .click('#automation-signupsubmit')

    .expect(Selector('.alert.alert-success').visible).ok()
    return name
    ;
}