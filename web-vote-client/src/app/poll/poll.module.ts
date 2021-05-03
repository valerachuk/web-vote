import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PollRoutingModule } from './poll-routing.module';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ViewEditVotePollsListComponent } from './components/view-edit-vote-polls-list/view-edit-vote-polls-list.component';
import { ViewVotePollComponent } from './components/view-vote-poll/view-vote-poll.component';
import { VoteService } from './services/vote.service';
import { DateHelperService } from './services/date-helper.service';
import { CreateEditPollValidatorService } from './services/create-edit-poll-validator.service';

@NgModule({
  declarations: [
    CreateEditPollComponent,
    ViewEditVotePollsListComponent,
    ViewVotePollComponent,
  ],
  imports: [
    CommonModule,
    PollRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule,
  ],
  providers: [VoteService, DateHelperService, CreateEditPollValidatorService],
})
export class PollModule {}
