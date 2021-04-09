import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoutButtonDirective } from './directives/logout-button.directive';
import { SpinnerComponent } from './components/spinner/spinner.component';

@NgModule({
  declarations: [LogoutButtonDirective, SpinnerComponent],
  imports: [CommonModule],
  exports: [LogoutButtonDirective, SpinnerComponent],
})
export class SharedModule {}
