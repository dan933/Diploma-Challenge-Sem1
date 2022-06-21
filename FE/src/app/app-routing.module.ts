import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './Components/login-page/login-page.component';
import { OverviewPageComponent } from './Components/overview-page/overview-page.component';

const routes: Routes = [
  { path: 'login', component: LoginPageComponent },
  { path: 'overview', component: OverviewPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
