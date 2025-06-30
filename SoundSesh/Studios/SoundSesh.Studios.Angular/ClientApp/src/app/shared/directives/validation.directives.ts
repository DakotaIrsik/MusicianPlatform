import { AbstractControl, Validator, NG_VALIDATORS } from '@angular/forms';
import { minValueValidator, maxValueValidator } from '../validators';
import { Directive, Input } from '@angular/core';

@Directive({
    selector: '[minValue]',
    providers: [{ provide: NG_VALIDATORS, useExisting: MinValueValidatorDirective, multi: true }]
})
export class MinValueValidatorDirective implements Validator {
    @Input('minValue') model: number;
    validate(control: AbstractControl): { [key: string]: any } | null {
        return this.model ? minValueValidator(this.model)(control)
            : null;
    }
}

@Directive({
    selector: '[maxValue]',
    providers: [{ provide: NG_VALIDATORS, useExisting: MaxValueValidatorDirective, multi: true }]
})
export class MaxValueValidatorDirective implements Validator {
    @Input('maxValue') model: number;
    validate(control: AbstractControl): { [key: string]: any } | null {
        return this.model ? maxValueValidator(this.model)(control)
            : null;
    }
}