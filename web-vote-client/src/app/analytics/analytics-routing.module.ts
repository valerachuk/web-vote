import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VotesPerOptionComponent } from './components/votes-per-option/votes-per-option.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'votes-per-option',
    pathMatch: 'full',
  },
  {
    path: 'votes-per-option',
    component: VotesPerOptionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnalyticsRoutingModule {}
