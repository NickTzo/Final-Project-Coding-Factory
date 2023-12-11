import { Component, Inject, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { MatFormFieldModule } from '@angular/material/form-field';


@Component({
  selector: 'app-car-delete',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatFormFieldModule],
  templateUrl: './car-delete.component.html',
  styleUrls: ['./car-delete.component.css']
})
export class CarDeleteComponent {
  @Input() header = 'Warning';
  @Input() title = 'Are you sure?';
  @Input() body = 'Are you sure you want to delete this car?';

  errorMessage: boolean | undefined;

  constructor(private appService: AppService = Inject(AppService),
    private route: ActivatedRoute,
    private router: Router = Inject(Router)) { }


  onClick(iamSure: boolean) {
    if (iamSure) {
      this.route.queryParams.subscribe((params: Params) => {
        let carId = JSON.parse(params['carId']);
        carId = parseInt(carId)
        this.appService.deleteCar(carId).subscribe({
          next: () => {
            alert("Your car has deleted");
          },
          error: (error) => {
            if (error) {
              this.errorMessage = true;
            }
          },
          complete: () => {
            this.router.navigate(['/crud/car/read']);
          }
        })
      })
    } else {
      this.router.navigate(['/crud/car/read']);
    }
  }
}
