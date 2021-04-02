import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';
import { HttpClientModule } from '@angular/common/http';
import { FormHelperService } from './services/form-helper.service';
import { GlobalToastService } from './services/global-toast.service';

@NgModule({
  providers: [
    AuthService,
    AuthGuardService,
    FormHelperService,
    GlobalToastService,
  ],
  imports: [CommonModule, HttpClientModule],
})
export class CoreModule {}
