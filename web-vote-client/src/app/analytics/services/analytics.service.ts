import { PercentPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PollOptionVotes } from 'src/app/interfaces/poll-option-votes.interface';
import { environment } from 'src/environments/environment';
import { FileDownloaderService } from './file-downloader.service';

@Injectable()
export class AnalyticsService {
  constructor(
    private readonly http: HttpClient,
    private readonly fileDownloader: FileDownloaderService,
    private readonly percentPipe: PercentPipe
  ) {}

  public getVotesPerOption(pollId: number): Observable<Array<PollOptionVotes>> {
    return this.http
      .get<Array<PollOptionVotes>>(
        `${environment.baseApiUrl}analytic/votes-per-option/${pollId}`
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

  public downloadVotesPerOptionCsv(pollId: number): Observable<void> {
    return this.http
      .get(`${environment.baseApiUrl}analytic/votes-per-option/${pollId}/csv`, {
        responseType: 'blob',
        observe: 'response',
      })
      .pipe(map((req) => this.fileDownloader.saveFile(req)));
  }
}
