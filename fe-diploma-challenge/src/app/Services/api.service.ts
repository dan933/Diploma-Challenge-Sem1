import { Injectable } from '@angular/core';

import { concatMap, tap, pluck } from 'rxjs/operators';

// Import the HttpClient for making API requests
import { HttpClient } from '@angular/common/http';

// Import AuthService from the Auth0 Angular SDK to get access to the user
import { AuthService } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    public auth: AuthService,
    private http: HttpClient
  ) { }

  getPets = () => {
    return this.http.get(`${environment.apiURL}/api/owner/view-pets`)
  }

  signUp = (signUpReq: any) => {
    return this.http.post(`${environment.apiURL}/api/owner/sign-up`, signUpReq)
  }


}
