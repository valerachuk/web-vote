import { NgModule } from '@angular/core';
import { CommonModule, PercentPipe } from '@angular/common';

import { AnalyticsRoutingModule } from './analytics-routing.module';
import { AnalyticsService } from './services/analytics.service';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { FileDownloaderService } from './services/file-downloader.service';
import { PollAnalyticsTemplateComponent } from './components/poll-analytics-template/poll-analytics-template.component';
import { VotesPerOptionComponent } from './components/votes-per-option/votes-per-option.component';
import { VotesPerRegionComponent } from './components/votes-per-region/votes-per-region.component';

@NgModule({
  declarations: [
    PollAnalyticsTemplateComponent,
    VotesPerOptionComponent,
    VotesPerRegionComponent,
  ],
  imports: [CommonModule, AnalyticsRoutingModule, SharedModule, FormsModule],
  providers: [AnalyticsService, FileDownloaderService, PercentPipe],
})
export class AnalyticsModule {}
