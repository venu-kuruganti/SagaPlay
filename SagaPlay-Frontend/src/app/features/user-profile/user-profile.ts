export interface User {
    Profile: UserProfile;
    Preferences: UserPreferences;
}

export interface UserProfile {
    UserId: string;    
    FirstName: string;
    LastName: string;
    EmailAddress: string;
    DateofBirth: string;
    Bio: string;
    ProfilePictureUrl: string;
    Country: string;
    PhoneNumber: string;
}

export interface UserPreferences {
    UserId: string;
    Theme: string;
    Language: string;
    NotificationSettings: string;
    PlayBackQualitySettings: string;
    ReceiveNewsLetter: boolean;
}