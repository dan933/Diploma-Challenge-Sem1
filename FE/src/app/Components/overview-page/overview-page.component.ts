import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {

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
