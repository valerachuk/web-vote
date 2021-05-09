import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NumberOfVotesPerOptionComponent } from './components/number-of-votes-per-option/number-of-votes-per-option.component';
import { PercentOfVotesPerOptionComponent } from './components/percent-of-votes-per-option/percent-of-votes-per-option.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'number-of-votes-per-option',
    pathMatch: 'full',
  },
  {
    path: 'number-of-votes-per-option',
    component: NumberOfVotesPerOptionComponent,
  },
  {
    path: 'percent-of-votes-per-option',
    component: PercentOfVotesPerOptionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnalyticsRoutingModule {}
