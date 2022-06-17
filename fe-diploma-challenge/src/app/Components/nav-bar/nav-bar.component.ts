import { Component, OnInit } from '@angular/core';

import { AuthService } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  logoutURL = environment.AUTH0.logoutURL;
  isLoggedIn = false;
  userEmail? = "";

  constructor(
    public auth: AuthService
  ) { }

  ngOnInit(): void {
    this.auth.user$.subscribe((resp) => { this.userEmail = resp?.email })
    this.checkLoginStatus()
  }

  checkLoginStatus = () => {
    this.auth.isAuthenticated$.subscribe(
      (resp) => { this.isLoggedIn = resp }
    )
  }

}
