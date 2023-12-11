import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-user-delete',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatInputModule],
  templateUrl: './user-delete.component.html',
  styleUrls: ['./user-delete.component.css']
})
export class UserDeleteComponent {
  @Input() header = 'Destructive Action!';
  @Input() title = 'Are you sure you want to delete your profile?';
  @Input() body = 'After this there is nothing that we can do?';
  errorMessage: boolean | undefined;

  constructor(
    private appService: AppService = Inject(AppService),
    private router: Router = Inject(Router)
  ) { }

  onClick(iamSure: boolean) {
    const userId = localStorage.getItem('id');
    if (iamSure && (userId != null)) {
      this.appService.deleteUser(parseInt(userId)).subscribe({
        next: () => {
          if (this.appService.getCarByUserId(parseInt(userId)) && this.appService.getReservationByUserId(parseInt(userId))) {
            this.appService.isLoggedIn.next(false);
            localStorage.removeItem('access_token');
            localStorage.removeItem('id');
            alert("Your account has deleted!!");
            this.router.navigate(['/login']);
          } else {
            alert("Something gone wrong please make sure that you have delete all your Reservation and your Cars Before you delete the User!!");
            this.router.navigate(['/crud/user/read']);
          }
        },
        error: (error) => {
          if (error) {
            this.errorMessage = true;
          }
        }
      })
    } else {
      this.router.navigate(['/login']);
    }
  }
}
