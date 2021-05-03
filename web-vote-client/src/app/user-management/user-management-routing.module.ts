import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AUTHORIZE_ROLES } from '../constants/authorize-roles.constant';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { RegisterComponent } from './components/register/register.component';
import { ViewProfileChangePasswordComponent } from './components/view-profile-change-password/view-profile-change-password.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'register',
    pathMatch: 'full',
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.managerAdmin,
    },
    canActivate: [AuthGuardService],
  },
  {
    path: 'profile',
    component: ViewProfileChangePasswordComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.voterManagerAdmin,
    },
    canActivate: [AuthGuardService],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagementRoutingModule {}
