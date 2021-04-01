import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserRole } from '../constants/userRoles.enum';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { AdminLayoutComponent } from './components/admin-layout/admin-layout.component';
import { LoginLayoutComponent } from './components/login-layout/login-layout.component';
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
      allowedRole: UserRole.Unauthorized,
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
      allowedRole: UserRole.Voter,
    },
    canActivate: [AuthGuardService],
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    data: {
      allowedRole: UserRole.Admin,
    },
    canActivate: [AuthGuardService],
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
