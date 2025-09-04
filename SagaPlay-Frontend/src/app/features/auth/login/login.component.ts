import { Component, inject } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { AuthService } from '@auth0/auth0-angular';
import { AuthenticationService } from '../../../core/authentication.service';
import { FormsModule, ɵInternalFormsSharedModule } from "@angular/forms";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, ɵInternalFormsSharedModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  username = '';
  password = '';
  error = '';

  private authService = inject(AuthenticationService);
  private router = inject(Router);
  private auth0 = inject(AuthService);

  onLogin() {
    // this.error = '';
    // this.authService.login(this.username, this.password).subscribe({
    //   next: (res) => {
    //     // Save tokens to memory or storage
    //     localStorage.setItem('access_token', res.access_token);
    //     localStorage.setItem('id_token', res.id_token ?? '');
    //     this.router.navigate(['/home']);
    //   },
    //   error: () => this.error = 'Login failed. Check credentials.'
    // });
    this.auth0.loginWithRedirect();
  }

}
