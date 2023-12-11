import { Component, EventEmitter, Inject, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User } from 'src/app/interfaces/user';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AppService } from 'src/app/app.service';
import { Router, RouterModule } from '@angular/router';
import { Credentials } from 'src/app/interfaces/credentials';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatCardModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  errorMessage: boolean | undefined;

  constructor(
    private appService: AppService = Inject(AppService),
    private jwtHelperService: JwtHelperService = Inject(JwtHelperService),
    private router: Router = Inject(Router)
  ) { }

  onLogin() {
    this.appService.login(this.form.value as Credentials).subscribe({
      next: (response) => {
        const userId = JSON.stringify(response.id)
        localStorage.setItem('access_token', response.token);
        localStorage.setItem('id', userId);
        const decoded_token = this.jwtHelperService.decodeToken(response.token);
        this.appService.isLoggedIn.next(true);
        this.appService.fullname.next(<string>this.form.value.username);
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


