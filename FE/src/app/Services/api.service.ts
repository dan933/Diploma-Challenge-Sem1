import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private http: HttpClient
  ) { }

  login = (owner:any) => {
    return this.http.post(`${environment.apiURL}/owner/login`,owner)
  }

  getPets = (userID: number) => {
    return this.http.get(`${environment.apiURL}/owner/get-pets/${userID}`)
  }

  createPets = (userID: number, pet:any) => {
    return this.http.post(`${environment.apiURL}/owner/${userID}/add-pet`,pet)
  }

  viewTreatments = (userID: number) => {
    return this.http.get(`${environment.apiURL}/owner/${userID}/view-treatments`)
  }

}
