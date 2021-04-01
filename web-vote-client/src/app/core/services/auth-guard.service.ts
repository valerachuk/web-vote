import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { ROLE_ROUTE_MAP } from 'src/app/constants/roleRouteMap.constant';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(
    private readonly router: Router,
    private readonly auth: AuthService
  ) {}

  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const currentRole = this.auth.role;
    if (route.data.allowedRole === this.auth.role) {
      return true;
    }

    this.auth.navigateAccordingToRole();

    return false;
  }
}
