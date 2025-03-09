import { Component } from '@angular/core';
import { SlideShowComponent } from '../slide-show/slide-show.component';

@Component({
  selector: 'app-root',
  imports: [SlideShowComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'simple-slide-show';
}
