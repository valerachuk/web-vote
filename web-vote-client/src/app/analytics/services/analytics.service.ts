import { PercentPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { VotesPerPollOption } from 'src/app/interfaces/votes-per-poll-option.interface';
import { VotesPerRegion } from 'src/app/interfaces/votes-per-region.interface copy';
import { environment } from 'src/environments/environment';
import { FileDownloaderService } from './file-downloader.service';

@Injectable()
export class AnalyticsService {
  constructor(
    private readonly http: HttpClient,
    private readonly fileDownloader: FileDownloaderService,
    private readonly percentPipe: PercentPipe
  ) {}

  public getVotesPerOption(
    pollId: number
  ): Observable<Array<VotesPerPollOption>> {
    return this.http
      .get<Array<VotesPerPollOption>>(
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

  public getVotesPerRegion(pollId: number): Observable<Array<VotesPerRegion>> {
    return this.http
      .get<Array<VotesPerRegion>>(
        `${environment.baseApiUrl}analytic/votes-per-region/${pollId}`
      )
      .pipe(
        map((rows) =>
          rows.map((row) => ({
            ...row,
            votersActivityPercent: this.percentPipe.transform(
              row.votersActivityPercent,
              '1.3-3'
            )!,
            votesPercent: this.percentPipe.transform(
              row.votesPercent,
              '1.3-3'
            )!,
          }))
        )
      );
  }

  public downloadVotesPerRegionCsv(pollId: number): Observable<void> {
    return this.http
      .get(`${environment.baseApiUrl}analytic/votes-per-region/${pollId}/csv`, {
        responseType: 'blob',
        observe: 'response',
      })
      .pipe(map((req) => this.fileDownloader.saveFile(req)));
  }
}
