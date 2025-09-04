import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { AuthModule } from '@auth0/auth0-angular';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './core/auth.interceptor';
import { AuthstateService } from './core/authstate.service';



export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
  importProvidersFrom(
    AuthModule.forRoot({
      domain: 'dev-sagaplay.eu.auth0.com',
      clientId: 'g7JHxlUJo0nCGlDmTCnLcH4u269y2Agy',
      authorizationParams: {
        redirect_uri: window.location.origin
      }
    })
  ),
  provideHttpClient(withInterceptors([authInterceptor])),
  AuthstateService
  ]
};

