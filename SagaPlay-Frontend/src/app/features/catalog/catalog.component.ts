import { Component, inject, OnInit } from '@angular/core';
import { CatalogService } from '../../core/catalog.service';
import { ContentItem } from './contentitem';
import { WatchlistService } from '../../core/watchlist.service';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [],
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.css'
})
export class CatalogComponent implements OnInit {

  private catalogService: any = inject(CatalogService);
  private watchlistService: any = inject(WatchlistService);
  public items: ContentItem[] = [];
  public selectedItem: ContentItem | undefined = undefined;

  ngOnInit(): void {
    this.GetAllContent();
  }

  public GetAllContent() {
    this.catalogService.getAllContent().subscribe({
      next: (result: any) => {
        this.items = result;
      }
    })
  }

  public OpenModal(id: number) {
    this.selectedItem = this.items.find(c => c.Id === id);
  }

  public AddToWatchlist(id: number) {
    this.watchlistService.AddToWatchList(localStorage.getItem('userId')!, id).subscribe({
      next: (successful: boolean) => {
        if (successful)
          alert("Successfully added to watchlist!");
        else
          alert("Item already in Watchlist");
      }
    })
  }

  constructor() {

  }


}
