import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../core/services/auth.service';
import { FormHelperService } from '../core/services/form-helper.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(
    public readonly formHelper: FormHelperService,
    private readonly auth: AuthService
  ) {}

  public serverValidationError = '';

  public readonly form = new FormGroup({
    login: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  public get loginControl(): FormControl {
    return this.form.get('login') as FormControl;
  }

  public get passwordControl(): FormControl {
    return this.form.get('password') as FormControl;
  }

  public onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid) {
      return;
    }

    this.form.disable();
    this.auth.signIn(this.form.value).subscribe(
      () => {
        this.auth.navigateAccordingToRole();
      },
      (error) => {
        if (error.status === 422) {
          this.serverValidationError = error.error;
          this.form.enable();
        }
      }
    );
  }
}
