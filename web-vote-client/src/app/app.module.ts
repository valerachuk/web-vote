import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from './core/core.module';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';
import { LOCAL_STORAGE_JWT_KEY } from './constants/misc.constant';
import { SharedModule } from './shared/shared.module';

const jwtModule = JwtModule.forRoot({
  config: {
    allowedDomains: [environment.allowedJwtDomain],
    tokenGetter: () => localStorage.getItem(LOCAL_STORAGE_JWT_KEY),
  },
});

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, NgbModule, CoreModule, jwtModule, SharedModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
