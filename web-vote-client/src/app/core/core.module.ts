import { NgModule, Optional, Provider, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';
import { FormHelperService } from './services/form-helper.service';
import { GlobalToastService } from './services/global-toast.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HTTPErrorInterceptor } from './services/http-error.interceptor';
import { PollService } from './services/poll.service';

const authInterceptorProvider: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: HTTPErrorInterceptor,
  multi: true,
};

@NgModule({
  providers: [
    AuthService,
    AuthGuardService,
    FormHelperService,
    GlobalToastService,
    PollService,
    authInterceptorProvider,
  ],
  imports: [CommonModule],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule?: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only'
      );
    }
  }
}
