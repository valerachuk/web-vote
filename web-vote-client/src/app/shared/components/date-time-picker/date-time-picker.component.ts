import {
  Component,
  Input,
  OnDestroy,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator,
  Validators,
} from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { FormHelperService } from 'src/app/core/services/form-helper.service';
import { DateTime } from 'src/app/interfaces/date-time.interface';

@Component({
  selector: 'app-date-time-picker',
  templateUrl: './date-time-picker.component.html',
  styleUrls: ['./date-time-picker.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: DateTimePickerComponent,
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: DateTimePickerComponent,
    },
  ],
})
export class DateTimePickerComponent
  implements OnDestroy, ControlValueAccessor, Validator {
  constructor(
    private readonly modalService: NgbModal,
    public readonly formHelper: FormHelperService
  ) {}

  @ViewChild('dateTimePickerModal')
  private modal: TemplateRef<any> | null = null;

  @Input()
  public title = '';

  public readonly form = new FormGroup({
    date: new FormControl(null, Validators.required),
    time: new FormControl(null, Validators.required),
  });

  private onChangeSubs: Subscription[] = [];

  public get dateControl(): FormControl {
    return this.form.get('date') as FormControl;
  }

  public get timeControl(): FormControl {
    return this.form.get('time') as FormControl;
  }

  public writeValue(value: DateTime): void {
    if (!value) {
      return;
    }
    this.form.setValue(value);
  }

  public registerOnChange(onChange: any): void {
    const sub = this.form.valueChanges.subscribe(onChange);
    this.onChangeSubs.push(sub);
  }

  public registerOnTouched(onTouched: any): void {
    const sub = this.form.valueChanges.subscribe(onTouched);
    this.onChangeSubs.push(sub);
  }

  public setDisabledState(disabled: boolean): void {
    if (disabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  public ngOnDestroy(): void {
    for (const sub of this.onChangeSubs) {
      sub.unsubscribe();
    }
  }

  public validate(control: AbstractControl): ValidationErrors | null {
    if (this.dateControl.valid !== this.timeControl.valid) {
      return {
        dateTimeInvalid: true,
      };
    }

    return null;
  }

  public openModal(): void {
    this.modalService.open(this.modal, {
      centered: true,
    });
  }
}
