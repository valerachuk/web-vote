import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AnalyticsRoutingModule } from './analytics-routing.module';
import { PollsResultsComponent } from './components/polls-results/polls-results.component';
import { AnalyticsService } from './services/analytics.service';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [PollsResultsComponent, PollsResultsComponent],
  imports: [CommonModule, AnalyticsRoutingModule, SharedModule, FormsModule],
  providers: [AnalyticsService],
})
export class AnalyticsModule {}
