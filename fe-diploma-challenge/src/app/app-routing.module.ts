import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './Components/login-page/login-page.component';
import { SignUpPageComponent } from './Components/sign-up-page/sign-up-page.component';

// Import the authentication guard
import { AuthGuard } from '@auth0/auth0-angular';
import { MyPetsComponent } from './Components/my-pets/my-pets.component';

const routes: Routes = [
  { path: 'login', component: LoginPageComponent },
  { path: 'sign-up', component: SignUpPageComponent },
  { path: 'pets', component: MyPetsComponent, canActivate:[AuthGuard] },
  { path: '**', redirectTo:'/pets'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
