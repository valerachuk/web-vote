import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VotesPerOptionComponent } from './components/votes-per-option/votes-per-option.component';
import { VotesPerRegionComponent } from './components/votes-per-region/votes-per-region.component';

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
  {
    path: 'votes-per-region',
    component: VotesPerRegionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnalyticsRoutingModule {}
