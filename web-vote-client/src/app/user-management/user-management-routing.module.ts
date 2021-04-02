import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AUTHORIZE_ROLES } from '../constants/authorize-roles.constant';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.managerAdmin,
    },
    canActivate: [AuthGuardService],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagementRoutingModule {}
