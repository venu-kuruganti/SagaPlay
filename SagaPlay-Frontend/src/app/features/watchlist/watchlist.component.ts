import { Component, inject, OnInit } from '@angular/core';
import { WatchlistService } from '../../core/watchlist.service';
import { ContentItem } from '../catalog/contentitem';
import { ɵInternalFormsSharedModule } from "@angular/forms";
import { WatchList } from './watchlist';
import { CatalogService } from '../../core/catalog.service';

@Component({
  selector: 'app-watchlist',
  standalone: true,
  imports: [ɵInternalFormsSharedModule],
  templateUrl: './watchlist.component.html',
  styleUrl: './watchlist.component.css'
})
export class WatchlistComponent implements OnInit {

  private watchlistService: WatchlistService = inject(WatchlistService);
  private catalogService: CatalogService = inject(CatalogService);
  private watchList?: WatchList;
  public items: { WatchListItemId: number, Item: ContentItem }[] = [];
  public figureFlag: boolean = false;

  ngOnInit(): void {
    this.loadWatchlist();
  }

  public onchange(radioValue: string): void {

    switch (radioValue) {
      case "carousel":
        this.figureFlag = false;
        break;

      case "grid":
        this.figureFlag = true;
        break;

      default:
        this.figureFlag = false;
        break;
    }
  }

  public removeFromWatchlist(itemId: number): void {
    console.log("ItemId : ", itemId);
    this.watchlistService.RemoveFromWatchList(localStorage.getItem('userId')!, itemId).subscribe({
      next: (successful) => {

        if (successful) {
          alert("Removed from Watchlist");
          this.watchList = undefined;
          this.items = [];
          this.loadWatchlist();
        }
        else {
          alert("Unable to remove! Some error occured!");
        }
      }
    })
  }

  private loadWatchlist() {
    //Get the watchlist first
    this.watchlistService.GetWatchListOnUserId(localStorage.getItem('userId')!).subscribe({
      next: (wlist) => {
        this.watchList = wlist;

        if (wlist && wlist.WatchListItems && wlist.WatchListItems.length > 0) {
          //Now populate items to show on front end.
          wlist.WatchListItems.forEach(element => {
            let item: ContentItem | undefined;
            let wlitemid: number = element.WatchListItemId;

            this.catalogService.getContentById(element.ContentItemId).subscribe({
              next: (result) => {
                item = result;
                this.items.push(
                  {
                    Item: item!,
                    WatchListItemId: wlitemid
                  });
              }
            });
          });//End of foreach
        }//End of if checker
      }//next
    });
  }


}
