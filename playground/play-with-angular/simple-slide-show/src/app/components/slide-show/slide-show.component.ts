import { Component } from '@angular/core';
import { ImgSrc } from '../../models/img-src';

@Component({
  selector: 'app-slide-show',
  imports: [],
  templateUrl: './slide-show.component.html',
  styleUrl: './slide-show.component.css',
})
export class SlideShowComponent {
  private currentIndex: number;
  private imgs: ImgSrc[];
  private playerIntervalId?: number;
  private playerIntervalSpanInMs: number;

  constructor() {
    this.playerIntervalSpanInMs = 1000;
    this.currentIndex = 0;
    this.imgs = Array.from(
      { length: 5 },
      (item, idx) => (item = new ImgSrc(idx + 1, `${idx + 1}.jpg`, 'slide')),
    );
  }

  get currentSlide(): ImgSrc {
    return this.imgs[this.currentIndex];
  }

  get playing(): boolean {
    return this.playerIntervalId != undefined;
  }

  protected changeIndex(diff: number): void {
    this.currentIndex =
      (this.currentIndex + diff + this.imgs.length) % this.imgs.length;
  }

  moveForward(): void {
    if (!this.playing) {
      this.changeIndex(1);
    }
  }

  moveBackward(): void {
    if (!this.playing) {
      this.changeIndex(-1);
    }
  }

  play() {
    if (this.playing) return;

    this.playerIntervalId = setInterval(
      this.changeIndex.bind(this, 1),
      this.playerIntervalSpanInMs,
    ) as unknown as number;
  }

  pause() {
    if (!this.playing) return;
    clearInterval(this.playerIntervalId);
    this.playerIntervalId = undefined;
  }
}
