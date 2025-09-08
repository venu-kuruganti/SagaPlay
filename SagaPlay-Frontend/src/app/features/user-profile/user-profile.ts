export interface User {
    Profile: UserProfile;
    Preferences: UserPreferences;
}

export interface UserProfile {
    FirstName: string;
    LastName: string;
    EmailAddress: string;
    DateOfBirth: string;
    Bio: string;
    ProfilePicURL: string;
    Country: string;
    PhoneNumber: string;
}

export interface UserPreferences {
    Theme: string;
    Language: string;
    NotificationSettings: string;
    PlayBackQuality: string;
    ReceiveNewsLetter: boolean;
}