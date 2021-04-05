import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PollRoutingModule } from './poll-routing.module';
import { CreateEditPollComponent } from './components/create-edit-poll/create-edit-poll.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { PollService } from './services/poll.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [CreateEditPollComponent],
  imports: [
    CommonModule,
    PollRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    HttpClientModule,
  ],
  providers: [PollService],
})
export class PollModule {}
