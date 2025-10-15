import { HttpClient } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { ContentItem } from '../features/catalog/contentitem';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {

  private http:HttpClient = inject(HttpClient);
  private baseUrl:string = `${environment.apiBaseUrl}/${environment.recommendationServicePrefix}`;
  private contents:ContentItem[] = [];
 
   public GetRecommendations():Observable<ContentItem[]>{

    return this.http.get<any>(`${this.baseUrl}/getnext`);

   }

  constructor() { }
}
