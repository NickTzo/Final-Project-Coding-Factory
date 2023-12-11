import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppService } from 'src/app/app.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Reservation } from 'src/app/interfaces/reservation';
import { Car } from 'src/app/interfaces/car';

@Component({
  selector: 'app-reservation-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule],
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})
export class ReservationCreateComponent implements OnInit {
  carData: Reservation = {
    carId: 0,
    userId: 0,
    price: 0
  }

  reservationDates: any = {
    startDate: "",
    endDate: ""
  }

  comfirm: boolean | undefined;
  errorMessage: boolean | undefined;

  form = new FormGroup({
    carId: new FormControl(0),
    userId: new FormControl(0),
    price: new FormControl({ value: 0, disabled: true }),
    startDate: new FormControl(''),
    endDate: new FormControl(''),
  })

  constructor(
    private appService: AppService = Inject(AppService),
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      this.carData.carId = JSON.parse(params['carId']);
      this.carData.userId = JSON.parse(params['userId']);
      this.carData.price = JSON.parse(params['price']);
      this.reservationDates = JSON.parse(params['reservationDates']);
      this.form.patchValue(this.carData)
      this.form.patchValue(this.reservationDates);
    });
  }


  onSubmit() {
    const reservations = <Reservation>this.form.value;
    this.appService.addReservation(reservations).subscribe({
      next: () => {
        let random = Math.floor(Math.random() * 10000 + 1);
        alert("Your comfimation number is " + random);
      },
      error: (error) => {
        if (error) {
          this.errorMessage = true;
        }
      },
      complete: () => {
        this.router.navigate(['']);
      }
    });
  }
}
