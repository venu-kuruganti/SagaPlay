import { state } from '@angular/animations';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthstateService {
  private loggedIn = new BehaviorSubject<boolean>(false);
  private isLoggedIn$ = this.loggedIn.asObservable();

  setLoggedIn(state: boolean) {
    this.loggedIn.next(state);
  }

  getLoggedIn(): boolean {
    return this.loggedIn.value;
  }

}
