import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginPageComponent } from './Components/login-page/login-page.component';
import { MaterialModule } from './Modules/material/material.module';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { PetsPageComponent } from './Components/pets-page/pets-page.component';
import { PetsTableComponent } from './Components/pets-page/pets-table/pets-table.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    NavBarComponent,
    PetsPageComponent,
    PetsTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  providers: [FormBuilder, CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
