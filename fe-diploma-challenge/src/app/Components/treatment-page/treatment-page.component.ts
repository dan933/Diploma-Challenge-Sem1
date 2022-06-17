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
    this.dialog.open(CreateTreatmentFormComponent);
  }

  treatmentData: Treatment[] = []
  displayedColumns:string[] = ["ID", "Owner Id", "Pet Name", "Procedure ID", "Date", "Notes", "Payment",]

  ngOnInit(): void {
    this.api.viewTreatments().subscribe((resp) => this.treatmentData = resp as Treatment[])
  }

}

// [
//   {
//       "ID": 3,
//       "OwnerId": 1,
//       "PetName": "Fluffy",
//       "ProcedureID": 10,
//       "Date": "2018-05-10T00:00:00",
//       "Notes": "Wounds sustained in apparent cat fight.",
//       "Payment": 30
//   }
// ]
