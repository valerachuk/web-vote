import { NgModule, Optional, SkipSelf } from '@angular/core';
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
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule?: CoreModule) {
    if (parentModule) {
      throw new Error(
        'GreetingModule is already loaded. Import it in the AppModule only'
      );
    }
  }
}
