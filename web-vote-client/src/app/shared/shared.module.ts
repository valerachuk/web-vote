import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoutButtonDirective } from './directives/logout-button.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [LogoutButtonDirective],
  imports: [CommonModule],
  exports: [LogoutButtonDirective],
})
export class SharedModule {}
