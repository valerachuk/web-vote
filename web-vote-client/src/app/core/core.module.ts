import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  providers: [AuthService, AuthGuardService],
  imports: [CommonModule, HttpClientModule],
})
export class CoreModule {}
