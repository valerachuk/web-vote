import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationLogComponent } from './components/registration-log/registration-log.component';

const routes: Routes = [{ path: '', component: RegistrationLogComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LoggingRoutingModule {}
