import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    public auth: AuthService,
  ) { }

    passwordValidation = {
      errorMessage:""
    }

  checkError = (formControl: any) => {
    let isError = formControl.errors != null ? true : false

    return isError;
  }

  signUpForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    phoneNumber: ['', [Validators.required, Validators.pattern(/\d/)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(7)]],
    confirmPassword:['',[Validators.required, Validators.minLength(7)]]
  })


  checkValue = () => {
  }

  ngOnInit(): void {
  }

  signUp = () => {

    this.signUpForm.markAllAsTouched();
    if (this.signUpForm.controls['password'].value != this.signUpForm.controls['confirmPassword'].value) {
      this.signUpForm.controls['password'].setErrors({'incorrect': true})
      this.signUpForm.controls['confirmPassword'].setErrors({ 'incorrect': true })

      this.passwordValidation.errorMessage = "Passwords don't match."

    } else if (!this.signUpForm.valid) {
      let password = this.signUpForm.controls['password'].value

      let hasUpperCase = /[A-Z]/.test(password);
      let hasLowerCase = /[a-z]/.test(password);
      let hasNumbers = /\d/.test(password);

      if (!hasUpperCase || !hasLowerCase || !hasNumbers) {
        this.passwordValidation.errorMessage = "Password requires Lower case (a-z), upper case (A-Z) and numbers (0-9)"
        this.signUpForm.controls['password'].setErrors({'incorrect': true})
        this.signUpForm.controls['confirmPassword'].setErrors({ 'incorrect': true })

      } else {

        this.signUpForm.controls['password'].setErrors(null)
        this.signUpForm.controls['confirmPassword'].setErrors(null)
        this.passwordValidation.errorMessage = ""
      }

    } else {
      this.signUpForm.controls['password'].setErrors(null)
        this.signUpForm.controls['confirmPassword'].setErrors(null)
        this.passwordValidation.errorMessage = ""
        //register user on auth0
        //register user on sql db
    }
  }


  login = () => {
    this.auth.loginWithRedirect({ appState:{target:'/pets'} })
  }

}
