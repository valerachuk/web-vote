import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NumberOfVotesPerOptionComponent } from './components/number-of-votes-per-option/number-of-votes-per-option.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'polls-results',
    pathMatch: 'full',
  },
  {
    path: 'number-of-votes-per-option',
    component: NumberOfVotesPerOptionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnalyticsRoutingModule {}
