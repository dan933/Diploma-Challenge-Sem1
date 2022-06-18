import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from '@auth0/auth0-angular';
import { ApiService } from 'src/app/Services/api.service';
import { CreateTreatmentFormComponent } from './create-treatment-form/create-treatment-form.component';

export interface Treatment
{
    ID: number,
    OwnerId: number,
    PetName: string,
    ProcedureID: number,
    Date: any,
    Notes: string,
    Payment: number
}

@Component({
  selector: 'app-treatment-page',
  templateUrl: './treatment-page.component.html',
  styleUrls: ['./treatment-page.component.scss']
})
export class TreatmentPageComponent implements OnInit {

  constructor(
    public auth: AuthService,
    public api: ApiService,
    public dialog: MatDialog
  ) { }

  openTreatmentForm() {
    const dialogRef = this.dialog.open(CreateTreatmentFormComponent);
    dialogRef.afterClosed().subscribe(
      () => {
        this.api.viewTreatments().subscribe((resp) => this.treatmentData = resp as Treatment[]);
      }
    );

  }

  isAdmin: boolean = false
  role:any = null

  getTreatments = () => {
    this.api.checkRole().subscribe({

      next: (resp: any) => { this.role = resp.claim },

      error: (err) => { console.log(err) },

      complete: () => {
        if (this.role == "write:admin") {
          this.api.adminViewTreatments().subscribe({
            next: (resp) => { this.treatmentData = resp as Treatment[], console.log(resp) },

            complete: () => { }

          })

        } else {

          this.api.viewTreatments().subscribe((resp) => this.treatmentData = resp as Treatment[])

        }
      }
    })
  }

  treatmentData: Treatment[] = []
  displayedColumns:string[] = ["ID", "Owner Id", "Pet Name", "Procedure ID", "Date", "Notes", "Payment",]

  ngOnInit(): void {
    this.getTreatments();
  }

}
