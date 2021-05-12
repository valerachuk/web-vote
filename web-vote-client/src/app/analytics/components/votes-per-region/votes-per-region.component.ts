import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AnalyticsService } from '../../services/analytics.service';
import { DataTableHeader } from 'src/app/shared/components/data-table/data-table-types';

@Component({
  selector: 'app-votes-per-region',
  templateUrl: './votes-per-region.component.html',
  styleUrls: ['./votes-per-region.component.css'],
})
export class VotesPerRegionComponent {
  constructor(public readonly anayticsService: AnalyticsService) {}

  public readonly tableHeader: DataTableHeader = [
    {
      columnName: 'Region name',
      key: 'name',
    },
    {
      columnName: 'Votes count',
      key: 'votesCount',
    },
    {
      columnName: 'Citizens count',
      key: 'citizensCount',
    },
    {
      columnName: 'Voters activity',
      key: 'votersActivityPercent',
    },
    {
      columnName: 'Votes percent',
      key: 'votesPercent',
    },
  ];

  public get analyticsRequester(): (pollId: number) => Observable<any> {
    return this.anayticsService.getVotesPerRegion.bind(this.anayticsService);
  }

  public get csvAnalyticsDownloader(): (pollId: number) => Observable<any> {
    return this.anayticsService.downloadVotesPerRegionCsv.bind(
      this.anayticsService
    );
  }
}
