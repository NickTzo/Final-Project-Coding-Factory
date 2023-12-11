import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, inject } from '@angular/core';
import { User } from './interfaces/user';
import { Reservation } from './interfaces/reservation';
import { Car } from './interfaces/car';
import { JWTToken } from './interfaces/jwttoken';
import { BehaviorSubject } from 'rxjs';
import { Credentials } from './interfaces/credentials';
import { Router } from '@angular/router';
import { GetHeaders } from './_services/get-headers';

const API = 'https://localhost:7094';



@Injectable({
  providedIn: 'root'
})
export class AppService {
  isLoggedIn = new BehaviorSubject<boolean>(false);
  fullname = new BehaviorSubject<string>('');


  constructor(private http: HttpClient = inject(HttpClient),
    private router: Router = Inject(Router),
    private getHeaders: GetHeaders) { }

  getAllUsers() {
    return this.http.get<User[]>(`${API}/api/users`, this.getHeaders.getHttpOptions());
  }

  getUserById(id: number) {
    return this.http.get<User>(`${API}/api/users/${id}`, this.getHeaders.getHttpOptions());
  }

  getUserByUsername(username: string) {
    return this.http.get<User>(`${API}/api/users/user/${username}`, this.getHeaders.getHttpOptions());
  }

  addUser(user: User) {
    return this.http.post<User>(`${API}/api/users`, user, this.getHeaders.getHttpOptions());
  }

  deleteUser(id: number) {
    return this.http.delete<User>(`${API}/api/users/${id}`, this.getHeaders.getHttpOptions());
  }

  updateUser(user: User) {
    return this.http.put<User>(`${API}/api/users`, user, this.getHeaders.getHttpOptions());
  }


  getAllCars() {
    return this.http.get<Car[]>(`${API}/api/cars`);
  }


  getCarById(id: number) {
    return this.http.get<Car>(`${API}/api/cars/${id}`);
  }


  getCarByUserId(id: number) {
    return this.http.get<Car[]>(`${API}/api/cars/userid/${id}`);
  }


  getCarByBrandAndByDate(search: any) {
    return this.http.post<Car[]>(`${API}/api/Cars/GetCarByBrandAndByDate`, search);
  }

  addCar(car: any, file: any) {
    const formData = new FormData();

    formData.append('photo', file);
    formData.append('brand', car.brand);
    formData.append('model', car.model);
    formData.append('year', car.year);
    formData.append('seat', car.seat);
    formData.append('cc', car.cc);
    formData.append('transmission', car.transmission);
    formData.append('price', car.price);
    formData.append('userId', car.userId);
    formData.append('isVisible', car.isVisible);

    return this.http.post(`${API}/api/cars/PostCar`, formData, this.getHeaders.getHttpOptions());
  }

  deleteCar(id: number) {
    return this.http.delete<Car>(`${API}/api/cars/${id}`, this.getHeaders.getHttpOptions());
  }

  updateCar(car: Car) {
    return this.http.put<Car>(`${API}/api/cars`, car, this.getHeaders.getHttpOptions());
  }


  getReservationById(id: number) {
    return this.http.get<Reservation>(`${API}/api/reservation/${id}`, this.getHeaders.getHttpOptions());
  }

  getReservationByUserId(userId: number) {
    return this.http.get<Reservation[]>(`${API}/api/reservation/userId/${userId}`, this.getHeaders.getHttpOptions());
  }

  addReservation(reservation: Reservation) {
    return this.http.post<Reservation>(`${API}/api/Reservation`, reservation, this.getHeaders.getHttpOptions());
  }

  deleteReservation(id: number) {
    return this.http.delete<Reservation>(`${API}/api/Reservation/${id}`, this.getHeaders.getHttpOptions());
  }

  updateReservations(reservation: Reservation) {
    return this.http.put<Reservation>(`${API}/api/reservation/${reservation.carId}`, reservation, this.getHeaders.getHttpOptions());
  }

  login(credentials: Credentials) {
    return this.http.post<JWTToken>(`${API}/api/users/login`, credentials);
  }

  logout() {
    this.isLoggedIn.next(false);
    this.fullname.next('');
    localStorage.removeItem('access_token');
    localStorage.removeItem('id');
    this.router.navigate(['']);
  }
}

