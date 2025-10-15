import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ContentItem } from '../features/catalog/contentitem';
import { WatchList } from '../features/watchlist/watchlist';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class WatchlistService {

  private httpClient = inject(HttpClient);
  //private baseUrl = "https://localhost:32771/api/Watchlist";
  private baseUrl:string = `${environment.apiBaseUrl}/${environment.watchlistServicePrefix}`;

  public AddToWatchList(userId: string, contentItemId: number): Observable<boolean> {

    return this.httpClient.post<boolean>(`${this.baseUrl}/AddToWatchList`, { "userId": userId, "contentItemId": contentItemId });

  }

  public RemoveFromWatchList(userId: string, WatchListItemId: number): Observable<boolean> {

    return this.httpClient.patch<boolean>(`${this.baseUrl}/${userId}/items/${WatchListItemId}/RemoveWatchListItem`, {});

  }

  public GetWatchListOnUserId(userId: string): Observable<WatchList> {
    return this.httpClient.get<WatchList>(`${this.baseUrl}/GetWatchListOnUserId?userId=${userId}`);
  }



  constructor() { }
}
