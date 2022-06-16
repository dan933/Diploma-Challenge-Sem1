import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { ApiService } from 'src/app/Services/api.service';


@Component({
  selector: 'app-my-pets',
  templateUrl: './my-pets.component.html',
  styleUrls: ['./my-pets.component.scss']
})
export class MyPetsComponent implements OnInit {

  //how will I differentiate who is an admin and who is not
  // use meta data to differentiate between users


  token = "";

  constructor(
    public auth: AuthService,
    public api: ApiService
  ) { }

  ngOnInit(): void {
    this.api.auth.getAccessTokenSilently().subscribe(
      (resp) => {console.log(resp)}
    )
    this.api.getPets().subscribe(
      (resp) => {console.log(resp)}
    )
  }

}
