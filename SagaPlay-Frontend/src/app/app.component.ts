import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainNavbarComponent } from './shared/main-navbar/main-navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MainNavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'SagaPlay-Frontend';
}
