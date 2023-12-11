import { Component, Inject, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppService } from 'src/app/app.service';
import { OutletContext, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { User } from 'src/app/interfaces/user';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-user-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule],
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent {
  constructor(
    private appService: AppService = Inject(AppService),
    private router: Router
  ) { }

  errorMessage: boolean | undefined;

  form = new FormGroup({
    username: new FormControl({ value: null, disabled: false }, {
      validators: [Validators.required, Validators.min(1), Validators.max(50)],
      asyncValidators: this.validateUsername(),
      updateOn: 'blur'
    }),
    password: new FormControl('', [Validators.required, Validators.min(8), Validators.max(50)]),
    firstname: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    lastname: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl(''),
  })


  private validateUsername(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> => {
      return new Promise((resolve) => {
        this.appService.getUserByUsername(control.value)
          .pipe(
            catchError((error) => {
              return of(null);
            })
          )
          .subscribe((data: any) => {
            if (data && data.username === control.value) {
              resolve({ 'alreadyExist': true });
            } else {
              resolve(null);
            }
          });
      });
    };
  }




  onSubmit() {
    const registerUser = this.form.value;
    this.appService.addUser(<User>registerUser).subscribe({
      next: () => {
        alert("Sing up Completed!!");
      }, error: error => {
        if (error) {
          this.errorMessage = true;
        }
      },
      complete: () => {
        this.router.navigate(['/login']);
      }
    });
  }
}
