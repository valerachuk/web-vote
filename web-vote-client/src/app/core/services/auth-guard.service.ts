import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
} from '@angular/router';
import { UserRole } from 'src/app/constants/user-roles.enum';
import { AuthService } from './auth.service';
import { GlobalToastService } from './global-toast.service';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(
    private readonly auth: AuthService,
    private readonly toastService: GlobalToastService
  ) {}

  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const currentRole = this.auth.role;
    if (route.data.allowedRoles.includes(currentRole)) {
      return true;
    }

    if (currentRole === UserRole.Unauthorized) {
      this.toastService.showInfo('Login again, please');
    }

    this.auth.navigateAccordingToRole();

    return false;
  }
}
