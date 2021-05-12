import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChangePasswordForm } from 'src/app/interfaces/change-password-form.interface';
import { PersonInfo } from 'src/app/interfaces/person-info.interface';
import { environment } from 'src/environments/environment';
import { RegisterForm } from '../../interfaces/register-form.interface';

@Injectable()
export class UserManagementService {
  constructor(private readonly http: HttpClient) {}

  public getProfileInfo(): Observable<PersonInfo> {
    return this.http.get<PersonInfo>(`${environment.baseApiUrl}person`);
  }

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

  public registerVotersCsv(file: File): Observable<void> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post<void>(
      `${environment.baseApiUrl}auth/register-csv`,
      formData,
      {
        headers: {
          ngiSkip: '400',
        },
      }
    );
  }

  public changePassword(form: ChangePasswordForm): Observable<void> {
    return this.http.put<void>(`${environment.baseApiUrl}auth`, form, {
      headers: {
        ngiSkip: '422',
      },
    });
  }
}
