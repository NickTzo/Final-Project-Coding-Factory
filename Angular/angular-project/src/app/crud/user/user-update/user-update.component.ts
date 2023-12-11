import { Component, Inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User } from 'src/app/interfaces/user';
import { AppService } from 'src/app/app.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-user-update',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule],
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {
  user: User | undefined;

  constructor(
    private appService: AppService = Inject(AppService),
    private router: Router = Inject(Router)
  ) { }

  form = new FormGroup({
    id: new FormControl({ value: 0, disabled: true }, [Validators.required]),
    username: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    firstname: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    lastname: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl('')
  })
  errorMessage: boolean | undefined;
  ngOnInit(): void {
    const userId = localStorage.getItem('id');
    if (userId == null) {
      this.user = undefined;
    } else {
      this.appService.getUserById(parseInt(userId)).subscribe((user) => {
        this.user = user;
        this.form.patchValue(this.user);
      })
    }
  }
  onUpdate() {
    const user = this.form.value;
    const id = localStorage.getItem('id');
    if (id) { user.id = parseInt(id) }
    this.appService.updateUser(<User>user).subscribe({
      next: () => {
        alert("Your account has updated!");
      },
      error: (error) => {
        console.log(user)
        if (error) {
          this.errorMessage = true;
        }
      },
      complete: () => {
        this.router.navigate(['/crud/user/read']);
      }
    });
  }

}
