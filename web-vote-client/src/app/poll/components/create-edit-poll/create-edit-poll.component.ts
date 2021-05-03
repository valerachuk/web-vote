import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { FormHelperService } from 'src/app/core/services/form-helper.service';
import { GlobalToastService } from 'src/app/core/services/global-toast.service';
import { PollService } from '../../../core/services/poll.service';
import { DateHelperService } from '../../services/date-helper.service';
import { map } from 'rxjs/operators';
import { Poll } from 'src/app/interfaces/poll.interface';
import { CreateEditPollValidatorService } from '../../services/create-edit-poll-validator.service';
import { DEFAULT_DATE_TIME_FORMAT } from 'src/app/constants/misc.constant';

@Component({
  selector: 'app-create-edit-poll',
  templateUrl: './create-edit-poll.component.html',
  styleUrls: ['./create-edit-poll.component.css'],
})
export class CreateEditPollComponent implements OnInit {
  constructor(
    public readonly formHelper: FormHelperService,
    public readonly dateHelper: DateHelperService,
    private readonly pollService: PollService,
    private readonly toastService: GlobalToastService,
    private readonly route: ActivatedRoute,
    private readonly myDateValidator: CreateEditPollValidatorService
  ) {}

  public readonly form = new FormGroup({
    id: new FormControl(null),
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    beginsAt: new FormControl(null, Validators.required),
    endsAt: new FormControl(null, Validators.required),
    options: new FormArray([], Validators.required),
  });

  public isEditForm: boolean | null = null;

  public readonly timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;

  public readonly defaultDateTimeFormat = DEFAULT_DATE_TIME_FORMAT;

  public get titleControl(): FormControl {
    return this.form.get('title') as FormControl;
  }

  public get descriptionControl(): FormControl {
    return this.form.get('description') as FormControl;
  }

  public get optionsArray(): FormArray {
    return this.form.get('options') as FormArray;
  }

  public get beginsAtControl(): FormControl {
    return this.form.get('beginsAt') as FormControl;
  }

  public get endsAtControl(): FormControl {
    return this.form.get('endsAt') as FormControl;
  }

  public ngOnInit(): void {
    this.form.setValidators([
      this.myDateValidator.endDateGreaterStartDate.bind(this.myDateValidator),
      this.myDateValidator.startsAtLeastInOneHour.bind(this.myDateValidator),
    ]);

    this.isEditForm = this.route.snapshot.data.isEditForm;

    if (!this.isEditForm) {
      this.addOption();
      return;
    }

    this.form.disable();
    const pollId = +this.route.snapshot.params.id;

    this.pollService
      .getPollWithOptionsAsAdmin(pollId)
      .pipe(
        map((pollDateISO) => ({
          ...pollDateISO,
          beginsAt: this.dateHelper.iso8601ToDateTimeUTC(pollDateISO.beginsAt),
          endsAt: this.dateHelper.iso8601ToDateTimeUTC(pollDateISO.endsAt),
        }))
      )
      .subscribe((poll) => {
        poll.options.forEach(() => {
          this.addOption();
        });
        this.form.patchValue(poll);

        this.form.enable();
      });
  }

  public getOptionTitle(group: AbstractControl): FormControl {
    return group.get('title') as FormControl;
  }

  public getOptionDescription(group: AbstractControl): FormControl {
    return group.get('description') as FormControl;
  }

  public addOption(): void {
    this.optionsArray.push(
      new FormGroup({
        id: new FormControl(null),
        title: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
      })
    );
  }

  public removeOption(idx: number): void {
    this.optionsArray.removeAt(idx);
  }

  public onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid) {
      return;
    }

    const poll = this.form.value;
    this.form.disable();
    const pollFormWithISO8601Date: Poll = {
      ...poll,
      beginsAt: this.dateHelper.dateTimeToISO8601UTC(poll.beginsAt),
      endsAt: this.dateHelper.dateTimeToISO8601UTC(poll.endsAt),
    };

    if (this.isEditForm) {
      this.pollService.updatePoll(pollFormWithISO8601Date).subscribe(() => {
        this.toastService.showSuccess(
          `Poll "${poll.title}" successfuly edited`
        );
        this.form.enable();
      });

      return;
    }

    this.pollService.createPoll(pollFormWithISO8601Date).subscribe(() => {
      this.toastService.showSuccess(`Poll "${poll.title}" successfuly created`);
      this.form.enable();
      this.form.reset();
      this.optionsArray.clear();
      this.addOption();
    });
  }
}
