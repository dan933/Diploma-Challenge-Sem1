import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from 'src/app/Services/api.service';


@Component({
  selector: 'app-treatment-form-dialog',
  templateUrl: './treatment-form-dialog.component.html',
  styleUrls: ['./treatment-form-dialog.component.scss']
})
export class TreatmentFormDialogComponent implements OnInit {

  myDatepipe!: any;

  constructor(
    private fb: FormBuilder,
    public api: ApiService,
    public dialog: MatDialog,
    private cookieService: CookieService,
  ) {
  }

  ngOnInit(): void {
    this.userID = + this.cookieService.get('UserID')
    this.getPets()
    this.getProcedures()
  }

  createTreatmentForm = this.fb.group({
    Pet: ['', Validators.required],
    Procedure: ['', Validators.required],
    Date: ['', Validators.required],
    Notes: ['', [Validators.required]],
  })

  pets!:any;
  userID!: any;
  procedures!: any;

  getPets = () => {
    this.api.getPets(this.userID).subscribe({
      next: (resp: any) => { this.pets = resp.Data, console.log(resp.Data) }
    })
  }

  getProcedures = () => {
    this.api.getProcedures().subscribe({
      next: (resp: any) => { this.procedures = resp, console.log(resp) }
    })
  }

  CreateTreatment = () => {
    if (this.createTreatmentForm.valid) {
      let newTreatment = {
        FK_PetID: +this.createTreatmentForm.controls["Pet"].value,
        FK_ProcedureID: +this.createTreatmentForm.controls["Procedure"].value,
        Date: this.createTreatmentForm.controls["Date"].value,
        Notes: this.createTreatmentForm.controls["Notes"].value
      }

      let DateFormat = new Date(newTreatment.Date)
      newTreatment.Date = DateFormat.toISOString().slice(0, 10)
      console.log(newTreatment)

    }
  }

}
