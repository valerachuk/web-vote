import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormHelperService } from 'src/app/core/services/form-helper.service';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { UserManagementService } from '../../services/user-management.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent {
  constructor(
    private readonly userManagementService: UserManagementService,
    private readonly toastService: GlobalToastService,
    public readonly formHelper: FormHelperService
  ) {}

  public readonly form = new FormGroup({
    oldPassword: new FormControl('', Validators.required),
    newPassword: new FormControl('', Validators.required),
  });

  public get oldPasswordControl(): FormControl {
    return this.form.get('oldPassword') as FormControl;
  }

  public get newPasswordControl(): FormControl {
    return this.form.get('newPassword') as FormControl;
  }

  public onSubmit(): void {
    this.form.markAllAsTouched();

    if (this.form.invalid) {
      return;
    }

    this.form.disable();
    this.userManagementService.changePassword(this.form.value).subscribe(
      () => {
        this.form.enable();
        this.form.reset();
        this.toastService.showSuccess('Your password has been changed');
      },
      (error) => {
        if (error.status === 422) {
          this.form.enable();
          this.oldPasswordControl.setErrors({
            missmatchError: true,
          });
        }
      }
    );
  }
}
