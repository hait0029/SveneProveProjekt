import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { RouterModule } from '@angular/router';

import { FormsModule } from '@angular/forms';

import { NavbarComponent } from './navigation/navbar/navbar.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
   
    RouterModule,
    FormsModule,
  NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BubbleTeaClient';
}
