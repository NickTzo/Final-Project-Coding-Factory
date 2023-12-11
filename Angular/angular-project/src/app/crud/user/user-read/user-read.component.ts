import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { User } from 'src/app/interfaces/user';
import { AppService } from 'src/app/app.service';
import { RouterModule } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-user-read',
  standalone: true,
  imports: [CommonModule,
    MatCardModule, CommonModule,
    MatCardModule, RouterModule, MatInputModule, MatButtonModule],
  templateUrl: './user-read.component.html',
  styleUrls: ['./user-read.component.css']
})
export class UserReadComponent implements OnInit {
  user: User = {
    id: 0,
    username: "",
    firstname: "",
    lastname: "",
    email: "",
    phone: "",
    token: ""
  }
  errorMessage: boolean | undefined;
  constructor(public appService: AppService = Inject(AppService)) { }

  ngOnInit(): void {
    const userId = localStorage.getItem('id');
    if (userId == null) {
      return;
    } else {
      this.appService.getUserById(parseInt(userId)).subscribe({
        next: (user) => {
          this.user = user;
        },
        error: (error) => {
          if (error) {
            this.errorMessage = true;
          }
        }
      })
    }
  }

}

