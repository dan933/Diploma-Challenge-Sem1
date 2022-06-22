import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from 'src/app/Services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { AddPetPopupFormComponent } from '../add-pet-popup-form/add-pet-popup-form.component';

@Component({
  selector: 'app-pets-table',
  templateUrl: './pets-table.component.html',
  styleUrls: ['./pets-table.component.scss']
})
export class PetsTableComponent implements OnInit {

  dataSource!: any;
  userID!: number;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ["ID", "PetName", "Type"];

  constructor(
    public api: ApiService,
    private cookieService: CookieService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.userID = +this.cookieService.get('UserID');
    this.api.getPets(this.userID).subscribe({
      next: (resp: any) => { this.dataSource = resp.Data, console.log(resp.Data) }
    })

  }

  openCreatePetsForm() {
    const dialogRef = this.dialog.open(AddPetPopupFormComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
