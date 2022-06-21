import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';
import { Cookie } from 'ng2-cookies/ng2-cookies';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  formControlNames = [
    {
    controlName:'FirstName',
    displayedName:'First Name'
    },
    {
    controlName:'SurName',
    displayedName:'Last Name'
    },
    {
    controlName:'Phone',
    displayedName:'Phone Number'
    },
  ]

  userID!: number;

  constructor(
    public fb: FormBuilder,
    public api: ApiService,
    public router: Router,
  ) { }

  registrationForm = this.fb.group({
    FirstName: ['', Validators.required],
    SurName: ['', Validators.required],
    Phone:['', [Validators.required, Validators.pattern(/\d*/)]]
  })



  ngOnInit(): void {
  }

  login = () => {
    if (this.registrationForm.valid) {
      let user = {
        FirstName: this.registrationForm.controls['FirstName'].value.trim(),
        SurName:this.registrationForm.controls['SurName'].value.trim(),
        Phone:this.registrationForm.controls['Phone'].value.trim()
      }

      this.api.login(user).subscribe({
        next: (resp:any) => { this.userID = resp.Data.OwnerID },
        error: (err) => { console.log(err) },
        complete: () => {
          Cookie.set('UserID', `${this.userID}`);
        }
      })
    }


  }

}
