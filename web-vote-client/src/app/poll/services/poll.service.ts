import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PollInfo } from '../interfaces/poll-info.interface';
import { Poll } from '../interfaces/poll.interface';
import { VoteForm } from '../interfaces/vote-form.interface';

@Injectable()
export class PollService {
  constructor(private readonly http: HttpClient) {}

  public createPoll(poll: Poll): Observable<void> {
    return this.http.post<void>(`${environment.baseApiUrl}poll`, poll);
  }

  public updatePoll(poll: Poll): Observable<void> {
    return this.http.put<void>(`${environment.baseApiUrl}poll`, poll);
  }

  public deletePoll(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.baseApiUrl}poll/${id}`);
  }

  public getPollsInfo(): Observable<Array<PollInfo>> {
    return this.http.get<Array<PollInfo>>(
      `${environment.baseApiUrl}poll/polls-info`
    );
  }

  public getPollWithOptions(id: number): Observable<Poll> {
    return this.http.get<Poll>(`${environment.baseApiUrl}poll/${id}`);
  }
}
