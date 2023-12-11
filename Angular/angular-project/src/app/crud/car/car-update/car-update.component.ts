import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Car } from 'src/app/interfaces/car';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';


@Component({
  selector: 'app-car-update',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule, MatRadioModule],
  templateUrl: './car-update.component.html',
  styleUrls: ['./car-update.component.css']
})
export class CarUpdateComponent implements OnInit {

  constructor(private appService: AppService = Inject(AppService),
    private route: ActivatedRoute,
    private router: Router = Inject(Router)) { }

  errorMessage: boolean | undefined;

  carData: Car = {
    id: 0,
    brand: "",
    model: "",
    cc: "",
    seat: "",
    price: 0,
    transmission: "",
    photoUrl: "",
    year: "",
    isVisible: false
  }

  form = new FormGroup({
    id: new FormControl({ value: 0, disabled: true }, [Validators.required]),
    model: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    year: new FormControl(''),
    cc: new FormControl(''),
    transmission: new FormControl(''),
    price: new FormControl(0),
    seat: new FormControl(''),
    isVisible: new FormControl(false)
  })

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      this.carData = JSON.parse(params['carData']);
      this.form.patchValue(this.carData);
    });
  }

  onUpdate() {
    const car = this.form.value;
    car.id = this.carData.id
    this.appService.updateCar(<Car>car).subscribe({
      next: (car) => {
        alert("Car has updated!!");
      },
      error: (error) => {
        this.errorMessage = true;
      },
      complete: () => {
        this.router.navigate(['/crud/car/read']);
      }
    });
  }

  onDelete() {
    const queryParams = {
      carId: JSON.stringify(this.carData.id),
    }
    this.router.navigate(['/crud/car/delete'], { queryParams: queryParams });
  }
}
