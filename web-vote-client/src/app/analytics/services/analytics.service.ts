import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PollOptionVote } from '../../interfaces/poll-option-vote.interface';
import { FileDownloaderService } from './file-downloader.service';

@Injectable()
export class AnalyticsService {
  constructor(
    private readonly http: HttpClient,
    private readonly fileDownloader: FileDownloaderService
  ) {}

  public getCountOfVotesPerOption(
    pollId: number
  ): Observable<Array<PollOptionVote>> {
    return this.http.get<Array<PollOptionVote>>(
      `${environment.baseApiUrl}analytic/number-of-votes-per-option/${pollId}`
    );
  }

  public downloadCountOfVotesPerOptionCsv(pollId: number): Observable<void> {
    return this.http
      .get(
        `${environment.baseApiUrl}analytic/number-of-votes-per-option/${pollId}/csv`,
        {
          responseType: 'blob',
          observe: 'response',
        }
      )
      .pipe(map((req) => this.fileDownloader.saveFile(req)));
  }
}
