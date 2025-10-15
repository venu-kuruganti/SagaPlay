import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RegisterDTO } from '../features/auth/register/registration';
import { Observable, of, switchMap, timer } from 'rxjs';
import { tap } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.production';


@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private registerUrl = `${environment.apiBaseUrl}${environment.userServicePrefix}/register`
//private registerUrl = 'http://localhost:32768/api/user/register';
  private tokenKey = 'auth_token';
  private domain = 'dev-sagaplay.eu.auth0.com'; // e.g. dev-abc123.us.auth0.com
  private clientId = 'g7JHxlUJo0nCGlDmTCnLcH4u269y2Agy';
  private audience = 'https://sagaplay/api'; // optional, only if you’re protecting an API
  private tokenUrl = `https://${this.domain}/oauth/token`;
  private accessToken$ = new BehaviorSubject<string | null>(null);
  private refreshToken: string | null = null;
  private refreshSub: any;

  // Observable to track login state
  public isLoggedIn$ = new BehaviorSubject<boolean>(!!sessionStorage.getItem(this.tokenKey));


  constructor() { }

  public register(model: RegisterDTO): Observable<any> {
    return this.http.post(`${this.registerUrl}`, model);
  }



  // private setSession(authResult: any) {
  //   const expiresIn = authResult.expires_in; // seconds
  //   this.accessToken$.next(authResult.access_token);
  //   this.refreshToken = authResult.refresh_token;

  //   // Schedule token refresh
  //   this.scheduleRefresh(expiresIn);
  // }

  // private scheduleRefresh(expiresIn: number) {
  //   if (this.refreshSub) {
  //     this.refreshSub.unsubscribe();
  //   }

  //   // Refresh 30s before expiry
  //   const refreshTime = (expiresIn - 30) * 1000;
  //   this.refreshSub = timer(refreshTime).pipe(
  //     switchMap(() => this.refreshTokenRequest())
  //   ).subscribe((res) => this.setSession(res));
  // }

  // private refreshTokenRequest() {
  //   if (!this.refreshToken) return of(null);

  //   const body = {
  //     grant_type: 'refresh_token',
  //     client_id: this.clientId,
  //     refresh_token: this.refreshToken,
  //   };

  //   return this.http.post<any>(this.tokenUrl, body);
  // }

  // getAccessToken() {
  //   return this.accessToken$.asObservable();
  // }

  // logout() {
  //   this.accessToken$.next(null);
  //   this.refreshToken = null;
  //   if (this.refreshSub) this.refreshSub.unsubscribe();
  // }
}
