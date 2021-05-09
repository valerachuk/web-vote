import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { PollService } from 'src/app/core/services/poll.service';
import { PollOptionVotesNumber } from 'src/app/interfaces/poll-option-votes-count.interface';
import { PollTitle } from 'src/app/interfaces/poll-title.interface';
import { DataTableHeader } from '../data-table/data-table-types';

@Component({
  selector: 'app-poll-analytics-template',
  templateUrl: './poll-analytics-template.component.html',
  styleUrls: ['./poll-analytics-template.component.css'],
})
export class PollAnalyticsTemplateComponent implements OnInit {
  constructor(private readonly pollService: PollService) {}

  @Input()
  public analyticsRequester:
    | ((pollId: number) => Observable<any>)
    | null = null;

  @Input()
  public csvAnalyticsDownloader:
    | ((pollId: number) => Observable<any>)
    | null = null;

  @Input()
  public tableHeader: DataTableHeader | null = null;

  @ViewChild('pollSelect')
  private pollSelect: ElementRef<HTMLSelectElement> | null = null;

  public pollsTitles$: Observable<Array<PollTitle>> | null = null;
  public pollAnalyticData$: Observable<Array<any>> | null = null;
  public isResultsLoading = false;
  public isCsvLoading = false;

  public ngOnInit(): void {
    this.pollsTitles$ = this.pollService.getPollsTitles();
  }

  public onResultRequested(): void {
    this.isResultsLoading = true;
    const pollId = this.pollSelect!.nativeElement.value;
    this.pollAnalyticData$ = this.analyticsRequester!(+pollId).pipe(
      tap(() => {
        this.isResultsLoading = false;
      })
    );
  }

  public onCsvRequested(): void {
    const pollId = this.pollSelect!.nativeElement.value;
    this.isCsvLoading = true;
    this.csvAnalyticsDownloader!(+pollId).subscribe(
      () => (this.isCsvLoading = false)
    );
  }
}
