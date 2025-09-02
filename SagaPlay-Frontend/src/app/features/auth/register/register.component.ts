import { Component } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { RegisterDTO } from './registration';
import { FormBuilder, Validators, ReactiveFormsModule} from '@angular/forms';
import { AuthenticationService } from '../../../core/authentication.service';
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
    if (this.registerForm.valid) {
      const dto: RegisterDTO = this.registerForm.value as RegisterDTO;
      this.authService.register(dto).subscribe({
        next: (result: any) => {
         console.log(result);          
          if(result.message==="Created"){
            this.thisRouter.navigate(['/login']);//Navigate home
          }
        },
        error: (err) => {
          console.error('Registration failed', err);
          alert(err.error?.message || 'Registration failed. Please try again.');
        }
      });

    }//if

  }//onSubmit
}
