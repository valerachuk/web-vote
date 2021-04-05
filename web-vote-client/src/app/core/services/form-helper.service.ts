import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable()
export class FormHelperService {
  public isInvalidAndTouched(control: AbstractControl): boolean {
    return control.invalid && control.touched;
  }
}
