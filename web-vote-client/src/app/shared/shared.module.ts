import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoutButtonDirective } from './directives/logout-button.directive';

@NgModule({
  declarations: [LogoutButtonDirective],
  imports: [CommonModule],
  exports: [LogoutButtonDirective],
})
export class SharedModule {}
