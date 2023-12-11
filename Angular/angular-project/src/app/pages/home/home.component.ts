import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ListCarsComponent } from 'src/app/crud/utils/list-cars/list-cars.component';
import { MatCard, MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Search } from 'src/app/interfaces/search';
import { DatePipe } from '@angular/common';
import { AppService } from 'src/app/app.service';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatButtonModule, MatDividerModule, MatIconModule, ListCarsComponent, MatCardModule, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  @Output() search = new EventEmitter<Search>();

  constructor(
    private router: Router = Inject(Router),
    private appService: AppService
  ) { }

  searchModel: Search = {
    brand: '',
    endDate: '',
    startDate: ''
  };

  form = new FormGroup({
    brand: new FormControl(''),
    startDate: new FormControl(new Date, [Validators.required]),
    endDate: new FormControl(new Date, [Validators.required])
  });

  onSearch() {
    const datePipe = new DatePipe('en-US');
    const searchModel: any = {};

    searchModel.endDate = datePipe.transform(this.form.value?.endDate, 'yyyy-MM-ddTHH:mm:ss');
    searchModel.startDate = datePipe.transform(this.form.value?.startDate, 'yyyy-MM-ddTHH:mm:ss');

    searchModel.brand = this.form.value?.brand;

    this.router.navigate(['/crud/utils/list-cars'], { queryParams: searchModel });
  }
}
