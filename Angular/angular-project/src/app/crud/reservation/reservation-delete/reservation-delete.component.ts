import { Component, Inject, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-reservation-delete',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatInputModule],
  templateUrl: './reservation-delete.component.html',
  styleUrls: ['./reservation-delete.component.css']
})
export class ReservationDeleteComponent {
  @Input() header = 'Warning';
  @Input() title = 'Are you sure?';
  @Input() body = 'Are you sure you want to delete this reservation?';

  errorMessage: boolean | undefined;

  constructor(private appService: AppService = Inject(AppService),
    private route: ActivatedRoute,
    private router: Router = Inject(Router)) { }


  onClick(iamSure: boolean) {
    if (iamSure) {
      this.route.queryParams.subscribe((params: Params) => {
        let reservationData = JSON.parse(params['reservationData']);
        reservationData.id = parseInt(reservationData.id)
        this.appService.deleteReservation(reservationData.id).subscribe({
          next: () => {
            alert("The reservation has deleted");
          },
          error: (error) => {
            if (error) {
              this.errorMessage = true;
            }
          },
          complete: () => {
            this.router.navigate(['/crud/reservation/read']);
          }
        })
      })
    } else {
      this.router.navigate(['/crud/reservation/read']);
    }
  }
}
