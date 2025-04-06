import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Client Meeting Handler';
}
