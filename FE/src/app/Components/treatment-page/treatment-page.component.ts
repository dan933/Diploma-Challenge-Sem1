import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from 'src/app/Services/api.service';

@Component({
  selector: 'app-treatment-page',
  templateUrl: './treatment-page.component.html',
  styleUrls: ['./treatment-page.component.scss']
})
export class TreatmentPageComponent implements OnInit {

  dataSource!: any;
  displayedColumns = ["ID", "PetID", "PetName", "Date", "Notes", "Payment"];

  userID!:number

  constructor(
    private cookieService: CookieService,
    public api:ApiService
  ) { }

  getTreatments = () => {
    this.api.viewTreatments(this.userID).subscribe({
      next:(resp:any) => { this.dataSource = resp.Data }
    })
  }

  ngOnInit(): void {
    this.userID = + this.cookieService.get('UserID')
    this.getTreatments();



  }

}
