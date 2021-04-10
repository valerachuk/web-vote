import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  NgbCalendar,
  NgbDateParserFormatter,
} from '@ng-bootstrap/ng-bootstrap';
import { FormHelperService } from 'src/app/core/services/form-helper.service';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { RegisterForm } from '../../../interfaces/register-form.interface';
import { UserManagementService } from '../../services/user-management.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  constructor(
    public readonly ngbCalendar: NgbCalendar,
    public readonly formHelper: FormHelperService,
    private readonly ngbDateParserFormatter: NgbDateParserFormatter,
    private readonly userManagementService: UserManagementService,
    private readonly toastService: GlobalToastService
  ) {}

  public readonly form = new FormGroup({
    fullName: new FormControl('', Validators.required),
    birth: new FormControl(null, Validators.required),
    individualTaxNumber: new FormControl('', Validators.required),
    login: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  public readonly minDate = { year: 1900, month: 1, day: 1 };

  public serverValidationError = '';

  public get fullNameControl(): FormControl {
    return this.form.get('fullName') as FormControl;
  }

  public get birthControl(): FormControl {
    return this.form.get('birth') as FormControl;
  }

  public get individualTaxNumberControl(): FormControl {
    return this.form.get('individualTaxNumber') as FormControl;
  }

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

    const formWithStringDate = Object.assign({}, this.form.value, {
      birth: this.ngbDateParserFormatter.format(this.form.value.birth),
    }) as RegisterForm;

    this.userManagementService.registerVoter(formWithStringDate).subscribe(
      () => {
        this.toastService.showSuccess(
          `Voter "${formWithStringDate.fullName}" successfuly registered`
        );
        this.form.enable();
        this.form.reset();
      },
      (error) => {
        if (error.status === 409) {
          this.serverValidationError = error.error;
          this.form.enable();
        }
      }
    );
  }
}
