import { NgModule } from '@angular/core';
import { CommonModule, PercentPipe } from '@angular/common';

import { AnalyticsRoutingModule } from './analytics-routing.module';
import { AnalyticsService } from './services/analytics.service';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { FileDownloaderService } from './services/file-downloader.service';
import { DataTableComponent } from './components/data-table/data-table.component';
import { PollAnalyticsTemplateComponent } from './components/poll-analytics-template/poll-analytics-template.component';
import { NumberOfVotesPerOptionComponent } from './components/number-of-votes-per-option/number-of-votes-per-option.component';
import { PercentOfVotesPerOptionComponent } from './components/percent-of-votes-per-option/percent-of-votes-per-option.component';

@NgModule({
  declarations: [
    DataTableComponent,
    PollAnalyticsTemplateComponent,
    NumberOfVotesPerOptionComponent,
    PercentOfVotesPerOptionComponent,
  ],
  imports: [CommonModule, AnalyticsRoutingModule, SharedModule, FormsModule],
  providers: [AnalyticsService, FileDownloaderService, PercentPipe],
})
export class AnalyticsModule {}
