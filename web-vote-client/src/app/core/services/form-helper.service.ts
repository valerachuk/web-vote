import { Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';

@Injectable()
export class FormHelperService {
  public isInvalidAndTouched(control: FormControl): boolean {
    return control.invalid && control.touched;
  }
}
