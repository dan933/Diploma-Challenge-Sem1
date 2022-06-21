import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './Components/login-page/login-page.component';
import { PetsPageComponent } from './Components/pets-page/pets-page.component';

const routes: Routes = [
  { path: 'login', component: LoginPageComponent },
  { path: 'pets', component: PetsPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
