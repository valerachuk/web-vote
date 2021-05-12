import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { LoggingRoutingModule } from './logging-routing.module';
import { RegistrationLogComponent } from './components/registration-log/registration-log.component';
import { SharedModule } from '../shared/shared.module';
import { LoggingService } from './services/logging.service';

@NgModule({
  declarations: [RegistrationLogComponent],
  providers: [LoggingService, DatePipe],
  imports: [CommonModule, LoggingRoutingModule, SharedModule],
})
export class LoggingModule {}
