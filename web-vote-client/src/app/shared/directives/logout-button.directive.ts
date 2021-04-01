import { Directive, HostListener } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';

@Directive({
  selector: '[appLogoutButton]',
})
export class LogoutButtonDirective {
  constructor(private readonly auth: AuthService) {}

  @HostListener('click')
  private onClick(): void {
    this.auth.signOut();
  }
}
