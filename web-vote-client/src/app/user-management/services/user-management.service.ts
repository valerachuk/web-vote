import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonInfo } from 'src/app/interfaces/person-info.interface';
import { environment } from 'src/environments/environment';
import { RegisterForm } from '../../interfaces/register-form.interface';

@Injectable()
export class UserManagementService {
  constructor(private readonly http: HttpClient) {}

  public registerVoter(form: RegisterForm): Observable<void> {
    return this.http.post<void>(
      `${environment.baseApiUrl}auth/register`,
      form,
      {
        headers: {
          ngiSkip: '409',
        },
      }
    );
  }

  public getProfileInfo(): Observable<PersonInfo> {
    return this.http.get<PersonInfo>(`${environment.baseApiUrl}person`);
  }
}
