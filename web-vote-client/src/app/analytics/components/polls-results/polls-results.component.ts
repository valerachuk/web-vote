import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { PollService } from 'src/app/core/services/poll.service';
import { PollOptionVote } from 'src/app/interfaces/poll-option-vote.interface';
import { PollTitle } from 'src/app/interfaces/poll-title.interface';
import { AnalyticsService } from '../../services/analytics.service';

@Component({
  selector: 'app-polls-results',
  templateUrl: './polls-results.component.html',
  styleUrls: ['./polls-results.component.css'],
})
export class PollsResultsComponent implements OnInit {
  constructor(
    private readonly pollService: PollService,
    private readonly analyticsService: AnalyticsService
  ) {}

  @ViewChild('pollSelect')
  private pollSelect: ElementRef<HTMLSelectElement> | null = null;

  public pollsTitles$: Observable<Array<PollTitle>> | null = null;
  public pollsOptionsVotes$: Observable<Array<PollOptionVote>> | null = null;
  public isResultsLoading = false;

  public ngOnInit(): void {
    this.pollsTitles$ = this.pollService.getPollsTitles();
  }

  public onResultRequested(): void {
    this.isResultsLoading = true;
    // tslint:disable-next-line: no-non-null-assertion
    const pollId = this.pollSelect!.nativeElement.value;
    this.pollsOptionsVotes$ = this.analyticsService
      .getPollOptionsVotes(+pollId)
      .pipe(
        tap(() => {
          this.isResultsLoading = false;
        })
      );
  }
}
