import { Component, inject, OnInit } from '@angular/core';
import { AppComponent } from '../../app.component';
import { UserdetailsService } from '../../core/userdetails.service';
import { AuthService } from '@auth0/auth0-angular';
import { RecommendationService } from '../../core/recommendation.service';
import { ContentItem } from '../catalog/contentitem';
import { WatchlistService } from '../../core/watchlist.service';
import { WatchList } from '../watchlist/watchlist';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [AppComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  private userService = inject(UserdetailsService);
  private authService = inject(AuthService);
  private recommService = inject(RecommendationService);
  private watchListService = inject(WatchlistService);
  private userId: string = "";
  public userName: string = "";
  public fullName: string = "";
  public recommendations: ContentItem[] = [];
  public watchListCount: number = 0;

  ngOnInit(): void {

    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.      

    this.userId = localStorage.getItem('userId')!;
    this.userName = localStorage.getItem('username')!;
    this.fullName = localStorage.getItem('fullName')!;

    if (!this.userId || !this.userName || !this.fullName) {

      this.authService.isAuthenticated$.subscribe(isAuth => {
        if (isAuth) {
          this.authService.user$.subscribe(user => {
            this.userName = user?.nickname!;
            localStorage.setItem('username', user?.nickname!);

            this.userService.getUserIdByUserName(this.userName).subscribe(id => {

              this.userId = id;
              localStorage.setItem('userId', id); // or sessionStorage.setItem

              this.userService.getUserProfile(this.userId).subscribe(profile => {

                this.fullName = profile.Profile.FirstName + ' ' + profile.Profile.LastName;
                localStorage.setItem('fullName', this.fullName);
              });

              this.getWatchListCount();

            });


          });
        }//End of if isauth
      });

    }//If

  }

  constructor() {

    //Get recommendations
    this.recommService.GetRecommendations().subscribe({
      next: (result) => {
        this.recommendations = result;
      }
    });


    //Get no. of items in watchlist
    if (!this.userId) {
      this.userId = localStorage.getItem('userId')!;
      this.getWatchListCount();
    }

  }

  private getWatchListCount(): void {

    this.watchListService.GetWatchListOnUserId(this.userId).subscribe(watchlist => {
      if (watchlist.WatchListItems) {
        this.watchListCount = watchlist.WatchListItems.length;
      }
      else {
        this.watchListCount = 0;
      }
    });
  }


  get isLoggedIn(): boolean {
    return AppComponent.isLoggedin;
  }
}
