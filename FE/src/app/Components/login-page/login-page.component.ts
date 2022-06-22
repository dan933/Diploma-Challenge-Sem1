import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from 'src/app/Services/api.service';
//deply
@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  userID: string = "";

  constructor(
    public fb: FormBuilder,
    public api: ApiService,
    public router: Router,
    private cookieService: CookieService
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
          console.log(this.userID)
          this.cookieService.set('UserID', this.userID);
          this.router.navigate(['pets'])
        }
      })
    }


  }

}
