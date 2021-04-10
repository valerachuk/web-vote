import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PollsResultsComponent } from './components/polls-results/polls-results.component';

const routes: Routes = [
  { path: 'polls-results', component: PollsResultsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnalyticsRoutingModule {}
