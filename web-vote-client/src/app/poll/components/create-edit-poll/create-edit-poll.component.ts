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
import { Poll } from '../../interfaces/poll.interface';
import { PollService } from '../../services/poll.service';

@Component({
  selector: 'app-create-edit-poll',
  templateUrl: './create-edit-poll.component.html',
  styleUrls: ['./create-edit-poll.component.css'],
})
export class CreateEditPollComponent implements OnInit {
  constructor(
    public readonly formHelper: FormHelperService,
    private readonly pollService: PollService,
    private readonly toastService: GlobalToastService,
    private readonly route: ActivatedRoute
  ) {}

  public readonly form = new FormGroup({
    id: new FormControl(null),
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    options: new FormArray([], Validators.required),
  });

  public isEditForm: boolean | null = null;

  public ngOnInit(): void {
    this.isEditForm = this.route.snapshot.data.isEditForm;

    if (!this.isEditForm) {
      this.addOption();
      return;
    }

    this.form.disable();
    const pollId = +this.route.snapshot.params.id;

    this.pollService.getPollWithOptions(pollId).subscribe((poll) => {
      poll.options.forEach(() => {
        this.addOption();
      });
      this.form.patchValue(poll);

      this.form.enable();
    });
  }

  public get titleControl(): FormControl {
    return this.form.get('title') as FormControl;
  }

  public get descriptionControl(): FormControl {
    return this.form.get('description') as FormControl;
  }

  public get optionsArray(): FormArray {
    return this.form.get('options') as FormArray;
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

    this.form.disable();

    if (this.isEditForm) {
      console.log(this.form.value);
      this.pollService.updatePoll(this.form.value).subscribe(() => {
        this.toastService.showSuccess(
          `Poll "${this.titleControl.value}" successfuly edited`
        );
        this.form.enable();
      });

      return;
    }

    this.pollService.createPoll(this.form.value).subscribe(() => {
      this.toastService.showSuccess(
        `Poll "${this.titleControl.value}" successfuly created`
      );
      this.form.reset();
      this.optionsArray.clear();
      this.addOption();
      this.form.enable();
    });
  }
}
