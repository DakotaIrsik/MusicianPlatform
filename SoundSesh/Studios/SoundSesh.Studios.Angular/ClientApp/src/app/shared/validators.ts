import { ValidatorFn, AbstractControl, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material';
import { RegularExpressions } from './regex';
import { ValidationError } from './models/validation-error';

export function zipCodeValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const zip = !RegularExpressions.zipCode.test(control.value);
        return zip ? { 'invalidZip': { value: control.value } } : null;
    };
}

export function passwordStrengthValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const password = !RegularExpressions.password.test(control.value);
        return password ? { 'invalidPassword': { value: control.value } } : null;
    };
}

export function phoneNumberValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const phone = !RegularExpressions.phoneNumber.test(control.value);
        return phone ? { 'invalidPhone': { value: control.value } } : null;
    };
}

export class CustomErrorStateMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        const invalidCtrl = !!(control && control.invalid && control.parent.dirty);
        const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);
        return (invalidCtrl || invalidParent);
    }
}

export function emailValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const email = !RegularExpressions.email.test(control.value);
        return email ? { 'invalidEmail': { value: control.value } } : null;
    };
}

export function minValueValidator(minValue: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const tooLow = (control.value) ? +control.value.replace(',', '') < +minValue : false;
        return tooLow ? { 'validationError': new ValidationError(control.value, 'Must be ' + minValue + ' or greater') } : null;
    };
}

export function maxValueValidator(maxValue: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const tooHigh = (control.value) ? +control.value.replace(',', '') > +maxValue : false;
        return tooHigh ? { 'validationError': new ValidationError(control.value, 'Must be ' + maxValue + ' or less') } : null;
    };
}