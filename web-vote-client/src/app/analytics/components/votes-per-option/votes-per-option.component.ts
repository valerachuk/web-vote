import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AnalyticsService } from '../../services/analytics.service';
import { DataTableHeader } from 'src/app/shared/components/data-table/data-table-types';

@Component({
  selector: 'app-votes-per-option',
  templateUrl: './votes-per-option.component.html',
  styleUrls: ['./votes-per-option.component.css'],
})
export class VotesPerOptionComponent {
  constructor(public readonly anayticsService: AnalyticsService) {}

  public readonly tableHeader: DataTableHeader = [
    {
      columnName: 'Option name',
      key: 'title',
    },
    {
      columnName: 'Votes count',
      key: 'count',
    },
    {
      columnName: 'Votes percent',
      key: 'percent',
    },
  ];

  public get analyticsRequester(): (pollId: number) => Observable<any> {
    return this.anayticsService.getVotesPerOption.bind(this.anayticsService);
  }

  public get csvAnalyticsDownloader(): (pollId: number) => Observable<any> {
    return this.anayticsService.downloadVotesPerOptionCsv.bind(
      this.anayticsService
    );
  }
}
