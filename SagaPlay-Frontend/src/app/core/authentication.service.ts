import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RegisterDTO } from '../features/auth/register/registration';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
private http = inject(HttpClient);
private baseUrl = 'http://localhost:4000/userservice/register';
//  private baseUrl = 'https://localhost:32773/api/User/register';

  constructor() { }

  public register(model:RegisterDTO): Observable<any>{
    return this.http.post(`${this.baseUrl}`, model);
  }
}
