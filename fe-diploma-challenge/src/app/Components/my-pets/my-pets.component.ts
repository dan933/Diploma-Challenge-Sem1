import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-my-pets',
  templateUrl: './my-pets.component.html',
  styleUrls: ['./my-pets.component.scss']
})
export class MyPetsComponent implements OnInit {

  token = "";

  constructor(public auth: AuthService) { }

  ngOnInit(): void {
    this.auth.getAccessTokenSilently().subscribe(
      (resp) => {
        this.token = resp
        console.log(resp)
      }
    )
  }

}
