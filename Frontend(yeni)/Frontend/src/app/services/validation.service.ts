import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor() { }

  GetValidationMessages(f: AbstractControl, name: string, t?: any) {
    if (f.errors) {
      for (let errorName in f.errors) {
        if (errorName == "required")
          return `${name} alanı boş bırakılamaz.`;
        else if (errorName == "email")
          return `E-posta formatı yanlış.`;
        else if (errorName == "pattern" && t && t.getAttribute('customType') == "phone")
          return (`${name} alanı sayı formatında olmalıdır.`)
        else if (errorName == "pattern" && t && t.getAttribute('customType') == "phoneNumber")
          return (`${name} 15551234567 formatta bir veri girilmelidir`)
        else if (errorName == "pattern" && t && t.getAttribute('customType') == "tc")
          return (`${name} alanı 11 karakter ve numerik olmalıdır.`)
        else if (errorName == "pattern" && t && t.getAttribute('customType') == "plate")
          return (`${name} alanına özel karakter kullanılamaz.`)
        else if (errorName == "pattern")
          return (`${name} alanı metin formatında olmalıdır.`)
        else if (errorName == "minlength")
          return name + ' alanı en az ' + f.errors['minlength'].requiredLength + ' karakter olmalıdır.';
        else if (errorName == "min")
          return name + ' alanı en küçük ' + f.errors['min'] + ' değerinde olmalıdır.';
        else if (errorName == "maxlength")
          return name + ' alanı en fazla ' + f.errors['maxlength'].requiredLength + ' karakter olmalıdır.';
        else if (errorName == "max")
          return name + ' alanı en büyük ' + f.errors['max'] + ' değerinde olmalıdır.';
        else if (errorName == "matching")
          return `${name} ile şifre alanları  eşleşmiyor.`;
        else if (errorName == "passwordMismatch")
          return `Test test test`;

      }
    }

    return;
  }


  matchValidator(
    matchTo: string,
    reverse?: boolean
  ): ValidatorFn {
    return (control: AbstractControl):
      ValidationErrors | null => {
      if (control.parent && reverse) {
        const c = (control.parent?.controls as any)[matchTo] as AbstractControl;
        if (c) {
          c.updateValueAndValidity();
        }
        return null;
      }
      return !!control.parent &&
        !!control.parent.value &&
        control.value ===
        (control.parent?.controls as any)[matchTo].value
        ? null
        : { matching: true };
    };
  }
}
