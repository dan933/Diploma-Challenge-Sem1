import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { ApiService } from 'src/app/Services/api.service';

export interface Pets {
  ownerId: number,
  petName: string,
  type:string
}

@Component({
  selector: 'app-pets-table',
  templateUrl: './pets-table.component.html',
  styleUrls: ['./pets-table.component.scss']
})



export class PetsTableComponent implements OnInit {

  // @Output() selectedPet = new EventEmitter<any>();

  constructor(
    public auth: AuthService,
    public api: ApiService
  ) { }

  petData: Pets[] = []

  displayedColumns: string[] = ['Pet Name', 'type']

  // selectedRow: any;

  // selectRow = (row: any) => {
  //   console.log(row)
  //   this.selectedRow = row;
  //   // this.selectedPet.emit(this.selectRow)
  // }

  ngOnInit(): void {
    this.api.getPets().subscribe(
      (resp) => {
        this.petData = resp as Pets[]
      }
    )
  }
}
