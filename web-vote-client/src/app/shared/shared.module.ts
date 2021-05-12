import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoutButtonDirective } from './directives/logout-button.directive';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { DateTimePickerComponent } from './components/date-time-picker/date-time-picker.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { DataTableComponent } from './components/data-table/data-table.component';

@NgModule({
  declarations: [
    LogoutButtonDirective,
    SpinnerComponent,
    DateTimePickerComponent,
    DataTableComponent,
  ],
  imports: [CommonModule, NgbModule, ReactiveFormsModule],
  exports: [
    LogoutButtonDirective,
    SpinnerComponent,
    DateTimePickerComponent,
    DataTableComponent,
  ],
})
export class SharedModule {}
