import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoutButtonDirective } from './directives/logout-button.directive';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { DateTimePickerComponent } from './components/date-time-picker/date-time-picker.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LogoutButtonDirective,
    SpinnerComponent,
    DateTimePickerComponent,
  ],
  imports: [CommonModule, NgbModule, ReactiveFormsModule],
  exports: [LogoutButtonDirective, SpinnerComponent, DateTimePickerComponent],
})
export class SharedModule {}
