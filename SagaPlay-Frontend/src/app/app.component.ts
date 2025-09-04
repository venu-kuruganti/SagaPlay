import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainNavbarComponent } from './shared/main-navbar/main-navbar.component';
import { AuthstateService } from './core/authstate.service';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MainNavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  private authStateService = inject(AuthstateService);
  private authService = inject(AuthService);
  title = 'SagaPlay-Frontend';
  public static isLoggedin:boolean = false; //global flag

  constructor(){
    this.authService.isAuthenticated$.subscribe(state=>{
      this.authStateService.setLoggedIn(state);
      AppComponent.isLoggedin = state;
    })
  }
}
