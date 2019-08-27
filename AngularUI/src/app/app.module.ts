import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule} from '@angular/router';

import { AppComponent } from './app.component';
import { SharedMaterialModule } from './_shared/modules/shared-material/shared-material.module';
import { SeatsComponent } from './seats/seats.component';
import { LoginComponent } from './login/login.component';
import { AdalModule } from './_modules/adal/adal.module';


@NgModule({
  declarations: [
    AppComponent,
    SeatsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SharedMaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: 'seats', component: SeatsComponent },
      { path: '', pathMatch: 'full', redirectTo: 'login' }
    ]),
    AdalModule.forRoot({
      instance: 'https://cghadfs.cgmh.org.tw/',
      tenant: 'adfs',
      redirectUri: window.location.origin + '/seats',
      cacheLocation: 'localStorage',
      clientId: '4200',
      postLogoutRedirectUri: window.location.origin + '/',
      endpoints: {
        'http://10.31.155.41/demo-primary': 'http://10.31.155.41/demo-primary'
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
