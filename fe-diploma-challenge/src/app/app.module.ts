import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './Components/login-page/login-page.component';

import { environment } from 'src/environments/environment';

// Import the module from the SDK
import { AuthModule } from '@auth0/auth0-angular';
import { MyPetsComponent } from './Components/my-pets/my-pets.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    MyPetsComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,

    AuthModule.forRoot({
      domain:environment.AUTH0.domain,
      clientId:environment.AUTH0.clientId
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
