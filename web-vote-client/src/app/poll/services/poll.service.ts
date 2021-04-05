import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Poll } from '../interfaces/poll.interface';

@Injectable()
export class PollService {
  constructor(private readonly http: HttpClient) {}

  public createPoll(poll: Poll): Observable<void> {
    return this.http.post<void>(`${environment.baseApiUrl}poll/create`, poll);
  }
}
