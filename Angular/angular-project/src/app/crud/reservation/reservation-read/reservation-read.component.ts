import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Reservation } from 'src/app/interfaces/reservation';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { AppService } from 'src/app/app.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Router, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-reservation-read',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatSortModule, MatCardModule, RouterModule, MatInputModule],
  templateUrl: './reservation-read.component.html',
  styleUrls: ['./reservation-read.component.css']
})
export class ReservationReadComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  errorMessage: boolean | undefined;
  reservations: Reservation[] = [];
  dataSource!: MatTableDataSource<Reservation>;

  mobileColumns: string[] = ['id', 'startDate', 'endDate'];
  tabletColumns: string[] = ['id', 'startDate', 'endDate'];
  desktopColumns: string[] = ['id', 'startDate', 'endDate'];
  displayedColumns: string[] = this.desktopColumns;

  constructor(private appService: AppService = Inject(AppService),
    private breakpointObserver: BreakpointObserver,
    private router: Router = Inject(Router)) { }

  ngOnInit(): void {
    const userId = localStorage.getItem('id');
    if (userId == null) {
      return;
    } else {
      this.appService.getReservationByUserId(parseInt(userId)).subscribe({
        next: (reservations) => {
          this.reservations = reservations;
          this.dataSource = new MatTableDataSource<Reservation>(this.reservations);
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
      reservationData: JSON.stringify(row),
    }
    this.router.navigate(['crud/reservation/delete'], { queryParams: queryParams });
  }
}
