import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class GetHeaders {

  public getHttpOptions() {
    const token = localStorage.getItem('access_token');
    if (!token) return;
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    }
  }
}