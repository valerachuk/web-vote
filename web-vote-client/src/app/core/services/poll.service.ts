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

  public getPollsTitles(): Observable<Array<PollTitle>> {
    return this.http.get<Array<PollTitle>>(
      `${environment.baseApiUrl}poll/polls-titles`
    );
  }

  public getPendingPolls(): Observable<Array<PollInfo>> {
    return this.http.get<Array<PollInfo>>(
      `${environment.baseApiUrl}poll/pending`
    );
  }

  public getActivePolls(): Observable<Array<PollInfo>> {
    return this.http.get<Array<PollInfo>>(
      `${environment.baseApiUrl}poll/active`
    );
  }

  public getArchivedPolls(): Observable<Array<PollInfo>> {
    return this.http.get<Array<PollInfo>>(
      `${environment.baseApiUrl}poll/archived`
    );
  }

  public getPollWithOptions(id: number): Observable<Poll> {
    return this.http.get<Poll>(`${environment.baseApiUrl}poll/${id}`);
  }

  public getPollWithOptionsAsAdmin(id: number): Observable<Poll> {
    return this.http.get<Poll>(`${environment.baseApiUrl}poll/${id}/as-admin`);
  }
}
