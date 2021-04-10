import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PollTitle } from 'src/app/interfaces/poll-title.interface';
import { environment } from 'src/environments/environment';
import { PollInfo } from '../../interfaces/poll-info.interface';
import { Poll } from '../../interfaces/poll.interface';

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

  public getPollsTitles(): Observable<Array<PollTitle>> {
    return this.http.get<Array<PollTitle>>(
      `${environment.baseApiUrl}poll/polls-titles`
    );
  }

  public getVotablePollsInfo(): Observable<Array<PollInfo>> {
    return this.http.get<Array<PollInfo>>(
      `${environment.baseApiUrl}poll/votable-polls-info`
    );
  }

  public getPollWithOptions(id: number): Observable<Poll> {
    return this.http.get<Poll>(`${environment.baseApiUrl}poll/${id}`);
  }
}
