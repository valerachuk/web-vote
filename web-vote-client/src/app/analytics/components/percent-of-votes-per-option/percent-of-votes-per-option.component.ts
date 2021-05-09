import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AnalyticsService } from '../../services/analytics.service';
import { DataTableHeader } from '../data-table/data-table-types';

@Component({
  selector: 'app-percent-of-votes-per-option',
  templateUrl: './percent-of-votes-per-option.component.html',
  styleUrls: ['./percent-of-votes-per-option.component.css'],
})
export class PercentOfVotesPerOptionComponent {
  constructor(public readonly anayticsService: AnalyticsService) {}

  public readonly tableHeader: DataTableHeader = [
    {
      columnName: 'Option id',
      key: 'id',
    },
    {
      columnName: 'Option name',
      key: 'title',
    },
    {
      columnName: 'Percent of total',
      key: 'percent',
    },
  ];

  public get analyticsRequester(): (pollId: number) => Observable<any> {
    return this.anayticsService.getPercentOfVotesPerOption.bind(
      this.anayticsService
    );
  }

  public get csvAnalyticsDownloader(): (pollId: number) => Observable<any> {
    return this.anayticsService.downloadPercentOfVotesPerOptionCsv.bind(
      this.anayticsService
    );
  }
}
