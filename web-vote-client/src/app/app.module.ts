import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from './core/core.module';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';
import { LOCAL_STORAGE_JWT_KEY } from './constants/misc.constant';
import { UserManagementService } from './user-management/services/user-management.service';
import { ToastContainerComponent } from './components/toast-container/toast-container.component';

const jwtModule = JwtModule.forRoot({
  config: {
    allowedDomains: [environment.allowedJwtDomain],
    tokenGetter: () => localStorage.getItem(LOCAL_STORAGE_JWT_KEY),
  },
});

@NgModule({
  declarations: [AppComponent, ToastContainerComponent],
  providers: [UserManagementService],
  imports: [BrowserModule, AppRoutingModule, NgbModule, CoreModule, jwtModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
