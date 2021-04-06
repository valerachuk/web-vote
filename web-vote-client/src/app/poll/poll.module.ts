import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PollRoutingModule } from './poll-routing.module';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { PollService } from './services/poll.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [CreateEditPollComponent],
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
