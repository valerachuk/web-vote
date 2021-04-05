import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AUTHORIZE_ROLES } from '../constants/authorize-roles.constant';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { AdminLayoutComponent } from './components/admin-layout/admin-layout.component';
import { LoginLayoutComponent } from './components/login-layout/login-layout.component';
import { ManagerLayoutComponent } from './components/manager-layout/manager-layout.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { VoterLayoutComponent } from './components/voter-layout/voter-layout.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginLayoutComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.unauthorized,
    },
    canActivate: [AuthGuardService],
    children: [
      {
        path: '',
        loadChildren: () =>
          import('../login/login.module').then((m) => m.LoginModule),
      },
    ],
  },
  {
    path: 'voter',
    component: VoterLayoutComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.voter,
    },
    canActivate: [AuthGuardService],
  },
  {
    path: 'manager',
    component: ManagerLayoutComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.manager,
    },
    canActivate: [AuthGuardService],
    children: [
      {
        path: 'user-management',
        loadChildren: () =>
          import('../user-management/user-management.module').then(
            (m) => m.UserManagementModule
          ),
      },
    ],
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    data: {
      allowedRoles: AUTHORIZE_ROLES.admin,
    },
    canActivate: [AuthGuardService],
    children: [
      {
        path: 'user-management',
        loadChildren: () =>
          import('../user-management/user-management.module').then(
            (m) => m.UserManagementModule
          ),
      },
      {
        path: 'poll',
        loadChildren: () =>
          import('../poll/poll.module').then((m) => m.PollModule),
      },
    ],
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutRoutingModule {}
