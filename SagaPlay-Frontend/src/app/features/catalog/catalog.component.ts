import { Component, inject } from '@angular/core';
import { CatalogService } from '../../core/catalog.service';
import { ContentItem } from './contentitem';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [],
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.css'
})
export class CatalogComponent {

  private catalogService: any = inject(CatalogService);
  public items: ContentItem[] = [];

  public GetAllContent(){
    this.catalogService.getAllContent().subscribe({
      next:(result:any)=>{
        console.log("result : ", result)
        this.items = result;        
        console.log("items : ", this.items);
      }
    })
  }

  constructor(){
    this.GetAllContent();
  }


}
