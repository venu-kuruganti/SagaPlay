import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

import { switchMap, take } from 'rxjs';
import { Token } from '@angular/compiler';
import { AuthService } from '@auth0/auth0-angular';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  console.log(req);
  const authService = inject(AuthService);

  const excludedUrls = [
    '/api/user/register',   // adjust to your actual register endpoint path
    '/userservice/register'
  ];


  // Check if request matches one of the excluded URLs
  if (excludedUrls.some(url => req.url.includes(url))) { 
    return next(req); // skip the token injection
  } 

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
