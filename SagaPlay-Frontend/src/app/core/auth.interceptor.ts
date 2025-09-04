import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

import { switchMap, take } from 'rxjs';
import { Token } from '@angular/compiler';
import { AuthService } from '@auth0/auth0-angular';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  return authService.getAccessTokenSilently().pipe(
    take(1),
    switchMap(token => {
      if (token) {
        const cloned = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });

        return next(cloned);

      }//end of if 
      
      return next(req);
    })
  );


};
