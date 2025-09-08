import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from './user-profile';
import { UserdetailsService } from '../../core/userdetails.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent {

  userProfileForm!: FormGroup;
  currentUser!: User;
  isEditing: boolean = false;

  private fb: FormBuilder = inject(FormBuilder);
  private userDetailsService = inject(UserdetailsService);

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.loadUser();
  }

  constructor() {

  }

  private loadUser(userId: string) {

    //Get data from the service and initialize currentuser with it.
    this.userDetailsService.getUserProfile(userId).subscribe(data => {
      this.currentUser = data;
    });

    //Initialize the form
    this.userProfileForm = this.fb.group({
      userProfile: this.fb.group({
        firstname: [this.currentUser.Profile.FirstName, [Validators.required, Validators.minLength(2)]],
        lastname: [this.currentUser.Profile.LastName, [Validators.required, Validators.minLength(2)]],
        emailaddress: [this.currentUser.Profile.EmailAddress, [Validators.required, Validators.email]],
        dateofbirth: [this.currentUser.Profile.DateOfBirth],
        bio: [this.currentUser.Profile.Bio],
        profilepicurl: [this.currentUser.Profile.ProfilePicURL],
        country: [this.currentUser.Profile.Country],
        phonenumber: [this.currentUser.Profile.PhoneNumber]
      }), //End of profile group
      userPreferences: this.fb.group({
        theme: [this.currentUser.Preferences.Theme || 'light'],
        language: [this.currentUser.Preferences.Language],
        notificationsettings: [this.currentUser.Preferences.NotificationSettings],
        playbackquality: [this.currentUser.Preferences.PlayBackQuality],
        receivenewsletter: [this.currentUser.Preferences.ReceiveNewsLetter ?? true]
      })//end of preferences group      
    })//End of entire form
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
    this.loadUser();//Reload user from backend
  }

  get profileControls() {
    return (this.userProfileForm.get('profile') as FormGroup).controls;
  }

}
