import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Params, Router, RouterModule } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { Car } from 'src/app/interfaces/car';
import { MatCardModule } from '@angular/material/card';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-car-read',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatSortModule, MatCardModule, RouterModule, MatInputModule, MatButtonModule],
  templateUrl: './car-read.component.html',
  styleUrls: ['./car-read.component.css']
})
export class CarReadComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  errorMessage: boolean | undefined;
  cars: Car[] = [];
  dataSource!: MatTableDataSource<Car>;

  mobileColumns: string[] = ['brand', 'model', 'year', 'price'];
  tabletColumns: string[] = ['brand', 'model', 'year', 'price', 'photoUrl'];
  desktopColumns: string[] = [
    'brand',
    'model',
    'year',
    'cc',
    'transmission',
    'seat',
    'price',
    'photoUrl',
    'isVisible'
  ];
  displayedColumns: string[] = this.desktopColumns;

  constructor(private appService: AppService = Inject(AppService),
    private breakpointObserver: BreakpointObserver,
    private router: Router = Inject(Router)) { }

  ngOnInit(): void {
    const userId = localStorage.getItem('id');
    if (userId == null) {
      return;
    } else {
      this.appService.getCarByUserId(parseInt(userId)).subscribe({
        next: (cars) => {
          this.cars = cars;
          this.dataSource = new MatTableDataSource<Car>(this.cars);
          this.dataSource.sort = this.sort;
        },
        error: (error) => {
          if (error) {
            this.errorMessage = true;
          }
        }
      })
    }

    this.breakpointObserver
      .observe([
        Breakpoints.XSmall,
        Breakpoints.Small,
        Breakpoints.Medium,
        Breakpoints.Large,
        Breakpoints.XLarge,
      ])
      .subscribe((result) => {
        if (result.breakpoints[Breakpoints.XSmall]) {
          this.displayedColumns = this.mobileColumns;
        } else if (result.breakpoints[Breakpoints.Small]) {
          this.displayedColumns = this.tabletColumns;
        } else if (result.breakpoints[Breakpoints.Medium]) {
          this.displayedColumns = this.tabletColumns;
        } else if (result.breakpoints[Breakpoints.Large]) {
          this.displayedColumns = this.desktopColumns;
        } else if (result.breakpoints[Breakpoints.XLarge]) {
          this.displayedColumns = this.desktopColumns;
        }
      });
  }
  getData(row: any) {
    const queryParams = {
      carData: JSON.stringify(row),
    }
    this.router.navigate(['crud/car/update'], { queryParams: queryParams });
  }
}
