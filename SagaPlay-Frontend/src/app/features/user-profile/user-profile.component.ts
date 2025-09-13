import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from './user-profile';
import { UserdetailsService } from '../../core/userdetails.service';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {

  userProfileForm!: FormGroup;
  currentUser!: User;
  isEditing: boolean = false;
  userName: string = "";


  private fb: FormBuilder = inject(FormBuilder);
  private userDetailsService = inject(UserdetailsService);
  private auth0Service = inject(AuthService);


  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.    
    console.log("initializer");
    this.loadUser(localStorage.getItem('userId')!);
  }

  constructor() {

  }

  private loadUser(userId: string) {

   

    //Get data from the service and initialize currentuser with it.
    this.userDetailsService.getUserProfile(userId).subscribe({
      next: (result) => {
        console.log("Successfully retrieved user profile");
        console.log('result', result);

        // ✅ Store result in currentUser so template bindings work
   this.currentUser = {
      Profile: {
        FirstName: result.Profile.FirstName,
        LastName: result.Profile.LastName,
        EmailAddress: result.Profile.EmailAddress,
        DateOfBirth: result.Profile.DateOfBirth,
        Bio: result.Profile.Bio,
        ProfilePicURL: result.Profile.ProfilePicURL,
        Country: result.Profile.Country,
        PhoneNumber: result.Profile.PhoneNumber
      },
      Preferences: result.Preferences
    };

    console.log('currentUser', this.currentUser);

        //Initialize the form
        this.userProfileForm = this.fb.group({
          userProfile: this.fb.group({
            firstname: [result.Profile.FirstName ?? "", [Validators.required, Validators.minLength(2)]],
            lastname: [result.Profile.LastName ?? "", [Validators.required, Validators.minLength(2)]],
            emailaddress: [result.Profile.EmailAddress ?? "", [Validators.required, Validators.email]],
            dateofbirth: [result.Profile.DateOfBirth ?? ""],
            bio: [result.Profile.Bio ?? ""],
            profilepicurl: [result.Profile.ProfilePicURL ?? ""],
            country: [result.Profile.Country ?? ""],
            phonenumber: [result.Profile.PhoneNumber ?? ""]
          }), //End of profile group
          userPreferences: this.fb.group({
            theme: [result.Preferences?.Theme || 'light'],
            language: [result.Preferences?.Language ?? ""],
            notificationsettings: [result.Preferences?.NotificationSettings ?? ""],
            playbackquality: [result.Preferences?.PlayBackQuality ?? ""],
            receivenewsletter: [result.Preferences?.ReceiveNewsLetter ?? true]
          })//end of preferences group      
        })//End of entire form
      },
      error: (err) => {
        console.error("Some error happened : ", err);
      }
    });


  }//End of function loadUser

  edit() {
    this.isEditing = true;
  }

  save() {
    if (this.userProfileForm.valid) {

      const updatedUser: User = {
        Profile: this.userProfileForm.value.userProfile,
        Preferences: this.userProfileForm.value.userPreferences
      };

      //Update the user by calling the service here.
      this.userDetailsService.updateUserProfile(updatedUser).subscribe(data => {
        this.currentUser = data;
      });

    }//end of if
    else {
      this.userProfileForm.markAllAsTouched(); //Indicate validation errors.
    }//End of else
  }//End of function

  cancel() {
    this.isEditing = false;
    //this.loadUser();//Reload user from backend
  }

  get profileControls() {
    return this.userProfileForm.controls as { [key: string]: FormControl };
  }

}
