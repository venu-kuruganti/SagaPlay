import { Component, inject } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { RegisterDTO } from './registration';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthenticationService } from '../../../core/authentication.service';
import { AuthService } from '@auth0/auth0-angular';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  registerForm = this.fb.group({
    userName: ['', Validators.required],
    userEmail: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private authService: AuthenticationService, private thisRouter: Router) { }

  onSubmit() {
    const emptyGuidValue = '00000000-0000-0000-0000-000000000000';
    if (this.registerForm.valid) {
      const dto: RegisterDTO = this.registerForm.value as RegisterDTO;
      this.authService.register(dto).subscribe({
        next: (result: any) => {
          if (result.message != emptyGuidValue) {
            this.thisRouter.navigate(['/home']);//Navigate home
          }
        },
        error: (err) => {
          console.error('Registration failed', err);
          alert(err.error?.message || 'Registration failed. Please try again.');
        }
      });

    }//if

  }//onSubmit

  private authenticationService = inject(AuthService);
  onBackToLogin() {
    this.authenticationService.loginWithRedirect();
  }
}
