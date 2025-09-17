import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from './user-profile';
import { UserdetailsService } from '../../core/userdetails.service';
import { AuthService } from '@auth0/auth0-angular';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {

  userProfileForm: FormGroup = new FormGroup({
    userProfile: new FormGroup({
      firstname: new FormControl(''),
      lastname: new FormControl(''),
      emailaddress: new FormControl(''),
      dateofbirth: new FormControl(''),
      bio: new FormControl(''),
      profilepictureurl: new FormControl(''),
      country: new FormControl(''),
      phonenumber: new FormControl('')
    }),
    userPreferences: new FormGroup({
      theme: new FormControl('Light'),
      language: new FormControl(''),
      notificationsettings: new FormControl(''),
      playbackqualitysettings: new FormControl(''),
      receivenewsletter: new FormControl('false')
    })
  });
  currentUser!: User;
  isEditing: boolean = false;
  userName: string = "";

  // Dropdown option lists
  themes: string[] = ["Light", "Dark"];
  languages = ['English', 'Spanish', 'French', 'German'];
  qualities = ['SD', 'HD', 'FullHD', '4K'];
  notifications = ['Email', 'SMS', 'Push Notification'];


  private fb: FormBuilder = inject(FormBuilder);
  private userDetailsService = inject(UserdetailsService);
  private auth0Service = inject(AuthService);


  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.        
    this.loadUser(localStorage.getItem('userId')!);
  }

  constructor() {

  }

  private loadUser(userId: string) {



    //Get data from the service and initialize currentuser with it.
    this.userDetailsService.getUserProfile(userId).subscribe({
      next: (result) => {


        // ✅ Store result in currentUser so template bindings work        
        this.currentUser = {
          Profile: {
            UserId: userId,
            FirstName: result.Profile?.FirstName,
            LastName: result.Profile?.LastName,
            EmailAddress: result.Profile?.EmailAddress,
            DateOfBirth: result.Profile?.DateOfBirth,
            Bio: result.Profile?.Bio,
            ProfilePictureUrl: result.Profile?.ProfilePictureUrl,
            Country: result.Profile?.Country,
            PhoneNumber: result.Profile?.PhoneNumber
          },
          Preferences: {
             UserId: userId,
            Theme: result.Preferences?.Theme,
            Language: result.Preferences?.Language,
            NotificationSettings: result.Preferences?.NotificationSettings,
            PlayBackQualitySettings: result.Preferences?.PlayBackQualitySettings,
            ReceiveNewsLetter: result.Preferences?.ReceiveNewsLetter
          }
        };

        console.log('currentUser', this.currentUser);

        //Initialize the form
        this.userProfileForm = this.fb.group({
          userProfile: this.fb.group({
            firstname: [this.currentUser.Profile.FirstName ?? "", [Validators.required, Validators.minLength(2)]],
            lastname: [this.currentUser.Profile.LastName ?? "", [Validators.required, Validators.minLength(2)]],
            emailaddress: [this.currentUser.Profile.EmailAddress ?? "", [Validators.required, Validators.email]],
            dateofbirth: [this.currentUser.Profile.DateOfBirth ?? "01/01/1900"],
            bio: [this.currentUser.Profile.Bio ?? ""],
            profilepictureurl: [this.currentUser.Profile.ProfilePictureUrl ?? ""],
            country: [this.currentUser.Profile.Country ?? ""],
            phonenumber: [this.currentUser.Profile.PhoneNumber ?? ""]
          }), //End of profile group
          userPreferences: this.fb.group({
            theme: [this.currentUser.Preferences.Theme ?? "Light"],
            language: [this.currentUser.Preferences.Language ?? "English"],
            notificationsettings: [this.currentUser.Preferences.NotificationSettings ?? ""],
            playbackqualitysettings: [this.currentUser.Preferences.PlayBackQualitySettings ?? "SD"],
            receivenewsletter: [this.currentUser.Preferences.ReceiveNewsLetter ?? false]
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

      updatedUser.Profile.UserId = localStorage.getItem('userId')!;
      updatedUser.Preferences.UserId = localStorage.getItem('userId')!;


      //Update the user by calling the service here.
      this.userDetailsService.updateUserProfile(updatedUser).subscribe(data => {
        this.currentUser = data;
        this.isEditing = false;
      });

    }//end of if
    else {
      this.userProfileForm.markAllAsTouched(); //Indicate validation errors.
    }//End of else
  }//End of function

  cancel() {
    this.isEditing = false;
    this.loadUser(localStorage.getItem('userId')!);//Reload user from backend
  }  

  get firstname() {
    return this.userProfileForm.get('firstname') as FormControl;    
  }

  get lastname() {
    return this.userProfileForm.get('lastname') as FormControl;
  }

  get emailaddress() {
    return this.userProfileForm.get('emailaddress') as FormControl;
  }

  get dateofbirth() {
    return this.userProfileForm.get('dateofbirth') as FormControl;
  }

  get bio() {
    return this.userProfileForm.get('bio') as FormControl;
  }

  get profilepicurl() {
    return this.userProfileForm.get('profilepicurl') as FormControl;
  }

  get country() {
    return this.userProfileForm.get('country') as FormControl;
  }

  get phonenumber() {
    return this.userProfileForm.get('phonenumber') as FormControl;
  }

  get theme() {    
    return this.userProfileForm.get('theme') as FormControl;
  }

  get language() {
    return this.userProfileForm.get('language') as FormControl;
  }

  get notificationsettings() {
    return this.userProfileForm.get('notificationsettings') as FormControl;
  }

  get playbackqualitysettings() {
    return this.userProfileForm.get('playbackqualitysettings') as FormControl;
  }

  get receivenewsletter() {
    return this.userProfileForm.get('receivenewsletter') as FormControl;
  }

}
