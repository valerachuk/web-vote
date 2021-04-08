import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AUTHORIZE_ROLES } from '../constants/authorize-roles.constant';
import { PollsViewType } from '../constants/polls-view-type.enum';
import { AuthGuardService } from '../core/services/auth-guard.service';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { PollVoteFormComponent } from './components/poll-vote-form/poll-vote-form.component';
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
      pollsViewType: PollsViewType.Edit,
    },
  },
  {
    path: 'vote-polls-list',
    component: ViewEditVotePollsListComponent,
    canActivate: [AuthGuardService],
    data: {
      allowedRoles: AUTHORIZE_ROLES.voterManagerAdmin,
      pollsViewType: PollsViewType.Vote,
    },
  },
  {
    path: 'vote/:id',
    component: PollVoteFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PollRoutingModule {}
