import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { AuthenticationService } from './authentication.service';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {

  const auth0 = inject(AuthService);
  const router = inject(Router);

  return auth0.isAuthenticated$.pipe(
    map(isAuth => {
      if (isAuth){      
        return true;
      } 
      router.navigate(['/home']);
      return false;
    })
  );

};
