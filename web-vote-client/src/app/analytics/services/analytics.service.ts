import { PercentPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PollOptionVotesPercent } from 'src/app/interfaces/poll-option-votes-percent.interface';
import { environment } from 'src/environments/environment';
import { PollOptionVotesNumber } from '../../interfaces/poll-option-votes-count.interface';
import { FileDownloaderService } from './file-downloader.service';

@Injectable()
export class AnalyticsService {
  constructor(
    private readonly http: HttpClient,
    private readonly fileDownloader: FileDownloaderService,
    private readonly percentPipe: PercentPipe
  ) {}

  public getCountOfVotesPerOption(
    pollId: number
  ): Observable<Array<PollOptionVotesNumber>> {
    return this.http.get<Array<PollOptionVotesNumber>>(
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

  public getPercentOfVotesPerOption(
    pollId: number
  ): Observable<Array<PollOptionVotesPercent>> {
    return this.http
      .get<Array<PollOptionVotesPercent>>(
        `${environment.baseApiUrl}analytic/percent-of-votes-per-option/${pollId}`
      )
      .pipe(
        map((rows) =>
          rows.map((row) => ({
            ...row,
            percent: this.percentPipe.transform(row.percent, '1.3-3')!,
          }))
        )
      );
  }

  public downloadPercentOfVotesPerOptionCsv(pollId: number): Observable<void> {
    return this.http
      .get(
        `${environment.baseApiUrl}analytic/percent-of-votes-per-option/${pollId}/csv`,
        {
          responseType: 'blob',
          observe: 'response',
        }
      )
      .pipe(map((req) => this.fileDownloader.saveFile(req)));
  }
}
