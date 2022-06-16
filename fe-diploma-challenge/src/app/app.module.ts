import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './Components/login-page/login-page.component';

//--------- Form Builder ----------//
import { FormBuilder, Validators, ReactiveFormsModule, FormsModule} from '@angular/forms';

import { environment } from 'src/environments/environment';

// Import the module from the SDK
import { AuthModule } from '@auth0/auth0-angular';
import { MyPetsComponent } from './Components/my-pets/my-pets.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//---------- Materials Module ---------------//
import { AppMaterialModule } from './app-material/app-material.module';

//------ api modules --------//
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';
import { SignUpPageComponent } from './Components/sign-up-page/sign-up-page.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    MyPetsComponent,
    NavBarComponent,
    SignUpPageComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    AppMaterialModule,
    HttpClientModule,


    AuthModule.forRoot({
      domain:environment.AUTH0.domain,
      clientId: environment.AUTH0.clientId,
      redirectUri: environment.AUTH0.redirectUri,
      audience: environment.AUTH0.audience,
      scope: 'read:message',

      httpInterceptor: {
        allowedList: [
          {
            uri: `${environment.apiURL}/*`,
            tokenOptions: {
              audience: environment.AUTH0.audience,
              scope: 'read:message'
            }
          }
        ]
      }
    }),

    BrowserAnimationsModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true },FormBuilder,Validators],
  bootstrap: [AppComponent]
})
export class AppModule { }
