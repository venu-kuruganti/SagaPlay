import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { CatalogComponent } from './features/catalog/catalog.component';
import { WatchlistComponent } from './features/watchlist/watchlist.component';
import { UserProfileComponent } from './features/user-profile/user-profile.component';
import { AboutComponent } from './features/about/about.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { authGuard } from './core/auth.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'catalog', component: CatalogComponent, canActivate: [authGuard] },
    { path: 'watchlist', component: WatchlistComponent, canActivate: [authGuard] },
    { path: 'userprofile', component: UserProfileComponent, canActivate: [authGuard] },
    { path: 'about', component: AboutComponent }, //Since this page just talks a bit about myself, no need for an auth guard.
    { path: 'register', component: RegisterComponent }, //Register and login don't need auth guards    
    { path: '**', pathMatch: 'full', component: HomeComponent }
];
