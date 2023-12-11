import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule, JsonPipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Params, Router, RouterModule } from '@angular/router';
import { Car } from 'src/app/interfaces/car';
import { AppService } from 'src/app/app.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-read-car',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatCardModule, RouterModule, MatButtonModule],
  templateUrl: './read-car.component.html',
  styleUrls: ['./read-car.component.css']
})
export class ReadCarComponent implements OnInit {
  carData: Car = {
    brand: "",
    model: "",
    cc: "",
    seat: "",
    price: 0,
    transmission: "",
    photoUrl: "",
    year: ""
  }

  searchData: any = {
    startDate: "",
    endDate: ""
  }

  constructor(private appService: AppService = Inject(AppService),
    private route: ActivatedRoute,
    private router: Router = Inject(Router)) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      this.carData = JSON.parse(params['carData']);
      this.searchData = JSON.parse(params['searchData']);
    });
  }

  onBooked() {
    const queryParams = {
      carId: JSON.stringify(this.carData.id),
      userId: JSON.stringify(this.carData.userId),
      price: JSON.stringify(this.carData.price),
      reservationDates: JSON.stringify(this.searchData)
    }
    if (localStorage.getItem('access_token') != null) {
      this.router.navigate(['crud/reservation/create'], { queryParams: queryParams });
    } else {
      this.router.navigate(['/login']);
    }
  }
}

