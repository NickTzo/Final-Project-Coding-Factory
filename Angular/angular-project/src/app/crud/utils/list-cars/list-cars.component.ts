import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Car } from 'src/app/interfaces/car';
import { AppService } from 'src/app/app.service';
import { MatCardModule } from '@angular/material/card';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { Search } from 'src/app/interfaces/search';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Routes } from '@angular/router'


@Component({
  selector: 'app-list-cars',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatSortModule, MatCardModule],
  templateUrl: './list-cars.component.html',
  styleUrls: ['./list-cars.component.css']
})
export class ListCarsComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;

  search: Search = {}
  cars: Car[] = [];
  dataSource!: MatTableDataSource<Car>;

  mobileColumns: string[] = ['photoUrl', 'brand', 'model', 'price'];
  tabletColumns: string[] = ['photoUrl', 'brand', 'model', 'year', 'price'];
  desktopColumns: string[] = [
    'photoUrl',
    'brand',
    'model',
    'year',
    'cc',
    'transmission',
    'seat',
    'price',
  ];
  displayedColumns: string[] = this.desktopColumns;

  constructor(private appService: AppService = Inject(AppService),
    private breakpointObserver: BreakpointObserver,
    private route: ActivatedRoute,
    private router: Router = Inject(Router)) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.search = params;
    });

    this.appService.getCarByBrandAndByDate(this.search).subscribe((cars) => {
      this.cars = cars;

      this.dataSource = new MatTableDataSource<Car>(this.cars);
      this.dataSource.sort = this.sort;
    })

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
      searchData: JSON.stringify(this.search)
    }
    this.router.navigate(['crud/utils/read-car'], { queryParams: queryParams });
  }
}

