import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { RouterModule } from '@angular/router';
import { AppService } from '../app.service';
import { MatButtonModule } from '@angular/material/button';
import { HomeComponent } from '../pages/home/home.component';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-application-layout',
  standalone: true,
  imports: [CommonModule,
    MatSidenavModule,
    MatExpansionModule, MatToolbarModule, MatButtonModule, MatIconModule, RouterModule, HomeComponent, MatListModule, MatMenuModule],
  templateUrl: './application-layout.component.html',
  styleUrls: ['./application-layout.component.css']
})
export class ApplicationLayoutComponent {

  isLoggedIn$ = this.appService.isLoggedIn;
  fullname$ = this.appService.fullname;
  constructor(
    private breakpointObserver: BreakpointObserver,
    private appService: AppService = Inject(AppService)) { }

  logout() {
    this.appService.logout();
  }
}
