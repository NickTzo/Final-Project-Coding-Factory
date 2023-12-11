import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { AppService } from 'src/app/app.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Car } from 'src/app/interfaces/car';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-car-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule],
  templateUrl: './car-create.component.html',
  styleUrls: ['./car-create.component.css']
})
export class CarCreateComponent {

  selectedFile: File | undefined;
  imageSrc: string | ArrayBuffer | null = null;

  errorMessage: boolean | undefined;

  constructor(
    private appService: AppService = Inject(AppService),
    private router: Router
  ) { }

  carCreate: Car = {
    brand: "",
    model: "",
    cc: "",
    seat: "",
    price: 0,
    transmission: "",
    photoUrl: "",
    year: "",
    userId: 0,
    isVisible: true,
    photo: undefined
  }

  form = new FormGroup({
    brand: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    model: new FormControl('', [Validators.required, Validators.min(1), Validators.max(50)]),
    year: new FormControl(''),
    cc: new FormControl(''),
    seat: new FormControl(''),
    transmission: new FormControl(''),
    price: new FormControl(0),
    userId: new FormControl(parseInt(localStorage.getItem('id')!)),
    isVisible: new FormControl(true),
  })


  onFileSelected(event: any): void {
    this.selectedFile = event?.target?.files[0] as File;
    if (this.selectedFile) {
      this.displaySelectedImage();
    }
  }

  displaySelectedImage(): void {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imageSrc = e.target.result;
    };
    reader.readAsDataURL(this.selectedFile as Blob);
  }


  onUpload() {
    const car = this.form.value;
    this.appService.addCar(<Car>car, this.selectedFile).subscribe({
      next: () => {
        alert("Your car has registered");
      }, error: error => {
        if (error) {
          this.errorMessage = true;
        }
      }, complete: () => {
        this.router.navigate(['/crud/car/read']);
      }
    });
  }
}
