import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserManagementRoutingModule } from './user-management-routing.module';
import { RegisterComponent } from './components/register/register.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RegionService } from './services/region.service';
import { UserManagementService } from './services/user-management.service';

@NgModule({
  declarations: [RegisterComponent],
  providers: [RegionService, UserManagementService],
  imports: [
    CommonModule,
    UserManagementRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    SharedModule,
  ],
})
export class UserManagementModule {}
