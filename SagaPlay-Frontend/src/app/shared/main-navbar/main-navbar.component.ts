import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'main-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './main-navbar.component.html',
  styleUrl: './main-navbar.component.css'
})
export class MainNavbarComponent {

}
