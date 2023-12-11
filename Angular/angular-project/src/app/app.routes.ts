import { Routes } from '@angular/router'
import { HomeComponent } from './pages/home/home.component'
import { AboutUsComponent } from './pages/about-us/about-us.component'
import { LoginComponent } from './pages/login/login.component'
import { UserUpdateComponent } from './crud/user/user-update/user-update.component'
import { CarCreateComponent } from './crud/car/car-create/car-create.component'
import { CarUpdateComponent } from './crud/car/car-update/car-update.component'
import { ReservationReadComponent } from './crud/reservation/reservation-read/reservation-read.component'
import { ReservationCreateComponent } from './crud/reservation/reservation-create/reservation-create.component'
import { CarReadComponent } from './crud/car/car-read/car-read.component'
import { UserReadComponent } from './crud/user/user-read/user-read.component'
import { UserDeleteComponent } from './crud/user/user-delete/user-delete.component'
import { ListCarsComponent } from './crud/utils/list-cars/list-cars.component'
import { UserCreateComponent } from './crud/user/user-create/user-create.component'
import { ReadCarComponent } from './crud/utils/read-car/read-car.component'
import { CarDeleteComponent } from './crud/car/car-delete/car-delete.component'
import { ReservationDeleteComponent } from './crud/reservation/reservation-delete/reservation-delete.component'
import { AuthGuard } from './auth.guard'

export const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "aboutUs", component: AboutUsComponent },
  { path: "login/register", component: UserCreateComponent },
  { path: "crud/user/read", component: UserReadComponent, canActivate: [AuthGuard] },
  { path: "crud/user/update", component: UserUpdateComponent, canActivate: [AuthGuard] },
  { path: "crud/user/delete", component: UserDeleteComponent, canActivate: [AuthGuard] },
  { path: "crud/car/create", component: CarCreateComponent, canActivate: [AuthGuard] },
  { path: "crud/reservation/delete", component: ReservationDeleteComponent, canActivate: [AuthGuard] },
  { path: "crud/car/update", component: CarUpdateComponent, canActivate: [AuthGuard] },
  { path: "crud/car/read", component: CarReadComponent, canActivate: [AuthGuard] },
  { path: "crud/car/delete", component: CarDeleteComponent, canActivate: [AuthGuard] },
  { path: "crud/utils/list-cars", component: ListCarsComponent },
  { path: "crud/utils/read-car", component: ReadCarComponent },
  { path: "crud/reservation/create", component: ReservationCreateComponent, canActivate: [AuthGuard] },
  { path: "crud/reservation/read", component: ReservationReadComponent, canActivate: [AuthGuard] }
]