import { Component, inject } from '@angular/core';
import { AppComponent } from '../../app.component';
import { UserdetailsService } from '../../core/userdetails.service';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [AppComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  private userService = inject(UserdetailsService);
  private authService = inject(AuthService);
  private userId: string = "";

  ngOnInit(): void {

    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    if (AppComponent.isLoggedin) {
      //Call userdetails service and get userid
      this.authService.user$.subscribe(loggedInUser => {
        console.log(loggedInUser);
        //this.userId = this.userService.getUserIdByUserName(loggedInUser?.preferred_username!);
      })
    }
  }

  get isLoggedIn(): boolean {
    return AppComponent.isLoggedin;
  }
}
