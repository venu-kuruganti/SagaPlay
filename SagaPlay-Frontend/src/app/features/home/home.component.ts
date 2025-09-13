import { Component, inject, OnInit } from '@angular/core';
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
export class HomeComponent implements OnInit {

  private userService = inject(UserdetailsService);
  private authService = inject(AuthService);
  private userId: string = "";
  private userName: string = "";

  ngOnInit(): void {

    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.  

    this.userId = localStorage.getItem('userId')!;

    if (!this.userId) {

      this.authService.isAuthenticated$.subscribe(isAuth => {
        if (isAuth) {
          this.authService.user$.subscribe(user => {
            this.userName = user?.nickname!;
            this.userService.getUserIdByUserName(this.userName).subscribe(id => {
              console.log('userId : ', id);
              localStorage.setItem('userId', id); // or sessionStorage.setItem
            });

          });
        }
      });




    }



  }


  get isLoggedIn(): boolean {
    return AppComponent.isLoggedin;
  }
}
