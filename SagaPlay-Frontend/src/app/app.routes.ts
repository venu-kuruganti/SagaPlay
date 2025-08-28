import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { CatalogComponent } from './features/catalog/catalog.component';
import { WatchlistComponent } from './features/watchlist/watchlist.component';
import { UserProfileComponent } from './features/user-profile/user-profile.component';
import { AboutComponent } from './features/about/about.component';

export const routes: Routes = [
    {path:'', component:HomeComponent},
    {path:'catalog', component:CatalogComponent},
    {path:'watchlist', component:WatchlistComponent},
    {path:'userprofile', component:UserProfileComponent},
    {path:'about', component:AboutComponent},
    {path:'**', pathMatch:'full', component:HomeComponent}
];
