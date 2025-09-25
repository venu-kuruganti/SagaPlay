import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ContentItem } from '../features/catalog/contentitem';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private http:HttpClient = inject(HttpClient);
  private baseUrl:string = "https://localhost:32769/api/Catalog";

  constructor() { }
  
  public getAllContent() : Observable<ContentItem[]>{
    return this.http.get<ContentItem[]>(this.baseUrl);  
  }

  public getContentById(id:number):Observable<ContentItem>{
    return this.http.get<ContentItem>(`${this.baseUrl}/${id}`);
  }

}
