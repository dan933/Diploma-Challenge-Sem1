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

  constructor(
    private fb: FormBuilder,
    public api: ApiService,
    public dialog: MatDialog,
    private cookieService: CookieService,
  ) { }

  ngOnInit(): void {
    this.userID = + this.cookieService.get('UserID')
    this.getPets()
  }
  
  createTreatmentForm = this.fb.group({
    Pet: ['', Validators.required],
    Procedure: ['', Validators.required],
    Date: ['', Validators.required],
    Notes: ['', [Validators.required]],
  })

  pets!:any;
  userID!:any;

  getPets = () => {
    this.api.getPets(this.userID).subscribe({
      next: (resp: any) => { console.log(resp) }
    })
  }

}
