import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from 'src/app/Services/api.service';
import { TreatmentFormDialogComponent } from './treatment-form-dialog/treatment-form-dialog.component';

@Component({
  selector: 'app-treatment-page',
  templateUrl: './treatment-page.component.html',
  styleUrls: ['./treatment-page.component.scss']
})
export class TreatmentPageComponent implements AfterViewInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  dataSource!: any;
  displayedColumns = ["ID", "PetID", "PetName", "Date", "Notes", "Payment"];

  userID!:number

  constructor(
    private cookieService: CookieService,
    public api:ApiService,
    public dialog: MatDialog
  ) { }

  getTreatments = () => {
    this.api.viewTreatments(this.userID).subscribe({
      next:(resp:any) => { this.dataSource = new MatTableDataSource<any>(resp.Data) },
      complete:() => { this.dataSource.paginator = this.paginator; }
    })
  }

  openTreatmentForm(){
    const dialogRef = this.dialog.open(TreatmentFormDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  ngAfterViewInit(): void {
    this.userID = + this.cookieService.get('UserID')
    this.getTreatments();
  }

}
