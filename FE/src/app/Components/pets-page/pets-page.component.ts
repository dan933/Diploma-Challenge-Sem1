import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-pets-page',
  templateUrl: './pets-page.component.html',
  styleUrls: ['./pets-page.component.scss']
})
export class PetsPageComponent implements OnInit {

  userID!: string;

  constructor(
    private cookieService: CookieService,
    public router: Router
  ) {

  }

  ngOnInit(): void {
    this.userID = this.cookieService.get('UserID')

    if (!this.userID) {
      this.router.navigate(['login'])
    }
  }

}
