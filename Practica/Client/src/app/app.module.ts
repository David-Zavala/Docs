import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component'
import ApiService from './_services/api.service';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule
  ],
  exports: [
    HttpClientModule,
    BrowserModule
  ],
  providers: [
    ApiService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
