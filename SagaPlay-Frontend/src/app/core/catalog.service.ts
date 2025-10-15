import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ContentItem } from '../features/catalog/contentitem';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private http:HttpClient = inject(HttpClient);
 // private baseUrl:string = "https://localhost:32769/api/Catalog";
 private baseUrl:string = `${environment.apiBaseUrl}/${environment.catalogServicePrefix}`

  constructor() { }
  
  public getAllContent() : Observable<ContentItem[]>{
    return this.http.get<ContentItem[]>(this.baseUrl);  
  }

  public getContentById(id:number):Observable<ContentItem>{
    return this.http.get<ContentItem>(`${this.baseUrl}/${id}`);
  }

}
