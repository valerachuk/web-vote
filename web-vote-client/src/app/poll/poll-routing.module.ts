import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AUTHORIZE_ROLES } from '../constants/authorize-roles.constant';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ViewEditVotePollsListComponent } from './components/view-edit-vote-polls-list/view-edit-vote-polls-list.component';

const routes: Routes = [
  {
    path: 'create',
    component: CreateEditPollComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.admin,
    },
  },
  {
    path: 'edit/:id',
    component: CreateEditPollComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.admin,
      isEditForm: true,
    },
  },
  {
    path: 'edit-polls-list',
    component: ViewEditVotePollsListComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.admin,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PollRoutingModule {}
