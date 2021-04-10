import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PollOptionVote } from '../../interfaces/poll-option-vote.interface';

@Injectable()
export class AnalyticsService {
  constructor(private readonly http: HttpClient) {}

  public getPollOptionsVotes(
    pollId: number
  ): Observable<Array<PollOptionVote>> {
    return this.http.get<Array<PollOptionVote>>(
      `${environment.baseApiUrl}analytics/poll-results/${pollId}`
    );
  }
}
