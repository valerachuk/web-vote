import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { LoginLayoutComponent } from './components/login-layout/login-layout.component';
import { VoterLayoutComponent } from './components/voter-layout/voter-layout.component';
import { AdminLayoutComponent } from './components/admin-layout/admin-layout.component';
import { ManagerLayoutComponent } from './components/manager-layout/manager-layout.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    LoginLayoutComponent,
    VoterLayoutComponent,
    AdminLayoutComponent,
    ManagerLayoutComponent,
    PageNotFoundComponent,
  ],
  imports: [CommonModule, LayoutRoutingModule, NgbModule, SharedModule],
})
export class LayoutModule {}
