import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';

@Component({
  selector: 'app-pets-page',
  templateUrl: './pets-page.component.html',
  styleUrls: ['./pets-page.component.scss']
})
export class PetsPageComponent implements OnInit {

  petData: any;
  displayedColumns: string[] = ['PetName', 'Type'];

  constructor(
    private activatedRoute: ActivatedRoute,
    public api: ApiService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next: (param) => {
        this.api.getPets(param['id']).subscribe({
          next:(resp:any) => {this.petData = resp.Data, console.log(resp.Data)}
          })

      },

      complete: () => {

      }
    })
  }

  NavPetTreatment = (row:any) => {
    console.log(row)
    this.router.navigate(['/pet',row,"treatments"])
  }

}
