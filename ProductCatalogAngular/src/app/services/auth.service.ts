import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserForLoginDto } from '../Models/user-for-login-dto';
import { Observable } from 'rxjs';
import { ServiceResponse } from '../Models/service-response';
import { UserDto } from '../Models/user-dto';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://vacata3380-001-site1.qtempurl.com/api';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
  }

  login(email: string, password: string): Observable<ServiceResponse<UserDto>> {
    return this.http.post<ServiceResponse<UserDto>>(`${this.baseUrl}/Auth/Login`, { email, password });
  }
  
}