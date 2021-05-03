import { assertNotNull } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';
import { DateHelperService } from './date-helper.service';

@Injectable()
export class CreateEditPollValidatorService {
  constructor(private readonly dateHelper: DateHelperService) {}

  public endDateGreaterStartDate(
    form: AbstractControl
  ): ValidationErrors | null {
    const beginsAtControl = form.get('beginsAt');
    const endsAtControl = form.get('endsAt');

    if (
      beginsAtControl?.value?.date == null ||
      beginsAtControl?.value?.time == null ||
      endsAtControl?.value?.date == null ||
      endsAtControl?.value?.time == null ||
      this.dateHelper.dateTimeComparer(
        beginsAtControl.value,
        endsAtControl.value
      ) < 0
    ) {
      return null;
    }

    return {
      endDateNotGreaterStartDate: true,
    };
  }

  public startsAtLeastInOneHour(
    form: AbstractControl
  ): ValidationErrors | null {
    const beginsAtControl = form.get('beginsAt');

    if (
      beginsAtControl?.value?.date == null ||
      beginsAtControl?.value?.time == null ||
      this.dateHelper.dateTimeComparer(
        beginsAtControl.value,
        this.dateHelper.dateTimeUTCNow
      ) >
        60 * 60 * 1000
    ) {
      return null;
    }

    return {
      startsLessThanInOneHour: true,
    };
  }
}
