import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PollRoutingModule } from './poll-routing.module';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { PollService } from './services/poll.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ViewEditVotePollsListComponent } from './components/view-edit-vote-polls-list/view-edit-vote-polls-list.component';

@NgModule({
  declarations: [CreateEditPollComponent, ViewEditVotePollsListComponent],
  imports: [
    CommonModule,
    PollRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule,
  ],
  providers: [PollService],
})
export class PollModule {}
