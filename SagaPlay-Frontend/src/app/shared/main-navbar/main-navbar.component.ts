import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { AppComponent } from '../../app.component';


@Component({
  selector: 'main-navbar',
  standalone: true,
  imports: [RouterLink, AppComponent],
  templateUrl: './main-navbar.component.html',
  styleUrl: './main-navbar.component.css'
})
export class MainNavbarComponent {
  private auth0 = inject(AuthService);
  public isAuthenticated$ = this.auth0.isAuthenticated$;
  public user$ = this.auth0.user$;

  get isLoggedIn(): boolean {
    return AppComponent.isLoggedin;
  }

  onLogin() {
    this.auth0.loginWithRedirect();
  }

  onLogout() {
    this.auth0.logout({ logoutParams: { returnTo: window.location.origin } });
  }
}
