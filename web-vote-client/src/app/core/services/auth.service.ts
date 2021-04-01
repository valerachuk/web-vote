import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JWTResponse } from 'src/app/interfaces/jwtResponse.interface';
import { SignInForm } from '../../interfaces/signInForm.interface';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LOCAL_STORAGE_JWT_KEY } from '../../constants/misc.constant';
import { UserRole } from 'src/app/constants/userRoles.enum';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { ROLE_ROUTE_MAP } from 'src/app/constants/roleRouteMap.constant';

@Injectable()
export class AuthService {
  constructor(
    private readonly http: HttpClient,
    private readonly jwtHelper: JwtHelperService,
    private readonly router: Router
  ) {}

  public get role(): UserRole {
    if (this.jwt === null || !this.isSignedIn) {
      return UserRole.Unauthorized;
    }

    return this.jwtHelper.decodeToken(this.jwt).role;
  }

  public get isSignedIn(): boolean {
    if (this.jwt === null) {
      return false;
    }

    return !this.jwtHelper.isTokenExpired(this.jwt);
  }

  private get jwt(): string | null {
    return localStorage.getItem(LOCAL_STORAGE_JWT_KEY);
  }

  public signOut(): void {
    localStorage.removeItem(LOCAL_STORAGE_JWT_KEY);
    this.router.navigate(ROLE_ROUTE_MAP[UserRole.Unauthorized]);
  }

  public navigateAccordingToRole(): void {
    this.router.navigate(ROLE_ROUTE_MAP[this.role]);
  }

  public signIn(signinForm: SignInForm): Observable<JWTResponse> {
    return this.http
      .post<JWTResponse>(`${environment.baseApiUrl}auth/login`, signinForm)
      .pipe(
        tap((jwt) => {
          this.saveJwtToLocalStorage(jwt);
        })
      );
  }

  private saveJwtToLocalStorage(jwt: JWTResponse): void {
    localStorage.setItem(LOCAL_STORAGE_JWT_KEY, jwt.accessToken);
  }
}
