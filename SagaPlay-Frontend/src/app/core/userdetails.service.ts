import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { User, UserProfile, UserPreferences } from '../features/user-profile/user-profile';


@Injectable({
  providedIn: 'root'
})
export class UserdetailsService {

  private httpClient: HttpClient = inject(HttpClient);
  private baseUrl: string = "https://localhost:4000/userservice/";

  getUserProfile(userId: string): Observable<User> {

    const profile$ = this.httpClient.get<UserProfile>(`${this.baseUrl}/Profile?id=${userId}`);
    const preferences$ = this.httpClient.get<UserPreferences>((`${this.baseUrl}/Preferences?id=${userId}`));

    return forkJoin({ Profile: profile$, Preferences: preferences$ });

  }

  updateUserProfile(updatedUser: User): Observable<User> {
    const profile$ = this.httpClient.patch<UserProfile>(`${this.baseUrl}/UpdateProfile`, updatedUser.Profile);
    const preferences$ = this.httpClient.patch<UserPreferences>(`${this.baseUrl}/UpdatePreferences`, updatedUser.Preferences);

    return forkJoin({ Profile: profile$, Preferences: preferences$ });
  }

  constructor() { }
}
