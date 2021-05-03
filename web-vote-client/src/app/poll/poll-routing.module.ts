import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AUTHORIZE_ROLES } from '../constants/authorize-roles.constant';
import { PollsViewType } from '../constants/polls-view-type.enum';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ViewVotePollComponent } from './components/view-vote-poll/view-vote-poll.component';
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
    path: 'pending-polls-list',
    component: ViewEditVotePollsListComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.admin,
      pollsViewType: PollsViewType.Pending,
    },
  },
  {
    path: 'active-polls-list',
    component: ViewEditVotePollsListComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.voterManagerAdmin,
      pollsViewType: PollsViewType.Active,
    },
  },
  {
    path: 'archived-polls-list',
    component: ViewEditVotePollsListComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.voterManagerAdmin,
      pollsViewType: PollsViewType.Archive,
    },
  },
  {
    path: 'edit-poll/:id',
    component: CreateEditPollComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.admin,
      isEditForm: true,
    },
  },
  {
    path: 'view-poll/:id',
    component: ViewVotePollComponent,
    data: {
      isViewForm: true,
    },
  },
  {
    path: 'vote-poll/:id',
    component: ViewVotePollComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PollRoutingModule {}
