import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PollInfo } from '../interfaces/poll-info.interface';
import { Poll } from '../interfaces/poll.interface';

@Injectable()
export class PollService {
  constructor(private readonly http: HttpClient) {}

  public createPoll(poll: Poll): Observable<void> {
    return this.http.post<void>(`${environment.baseApiUrl}poll/create`, poll);
  }

  public updatePoll(poll: Poll): Observable<void> {
    return this.http.post<void>(`${environment.baseApiUrl}poll/update`, poll);
  }

  public getPollsInfo(): Observable<Array<PollInfo>> {
    return this.http.get<Array<PollInfo>>(
      `${environment.baseApiUrl}poll/polls-info`
    );
  }

  public getPollWithOptions(id: number): Observable<Poll> {
    return this.http.get<Poll>(
      `${environment.baseApiUrl}poll/poll-with-options/${id}`
    );
  }
}
