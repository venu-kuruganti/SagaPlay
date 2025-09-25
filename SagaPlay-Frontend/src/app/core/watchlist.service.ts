import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WatchlistService {

  private httpClient = inject(HttpClient);
  private baseUrl = "http://localhost:32770/api/Watchlist";

  public AddToWatchList(userId: string, contentItemId: number): Observable<boolean> {

    return this.httpClient.post<boolean>(`${this.baseUrl}/AddToWatchList`, { "userId": userId, "contentItemId": contentItemId });

  }

  public RemoveFromWatchList(userId: string, WatchListItemId: number): Observable<boolean> {

    return this.httpClient.patch<boolean>(`${this.baseUrl}/${userId}/items/${WatchListItemId}/RemoveWatchListItem`, {});

  }

  constructor() { }
}
