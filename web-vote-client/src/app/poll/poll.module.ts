import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PollRoutingModule } from './poll-routing.module';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ViewEditVotePollsListComponent } from './components/view-edit-vote-polls-list/view-edit-vote-polls-list.component';
import { PollVoteFormComponent } from './components/poll-vote-form/poll-vote-form.component';
import { VoteService } from './services/vote.service';

@NgModule({
  declarations: [
    CreateEditPollComponent,
    ViewEditVotePollsListComponent,
    PollVoteFormComponent,
  ],
  imports: [
    CommonModule,
    PollRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule,
  ],
  providers: [VoteService],
})
export class PollModule {}
