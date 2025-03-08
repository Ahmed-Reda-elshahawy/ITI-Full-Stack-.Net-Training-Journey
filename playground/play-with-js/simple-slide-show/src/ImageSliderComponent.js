export class ImageSliderComponent extends EventTarget {
  constructor(imgsSrc, interval = 1000) {
    super();
    this.imgsSrc = imgsSrc;
    this.interval = interval;
    this.currentIndex = 0;
    if (imgsSrc.length < 0) {
      throw new Error('Empty Slide Show!');
    }
  }

  #emitSlidChange() {
    this.currentIndex =
      (this.currentIndex + this.imgsSrc.length) % this.imgsSrc.length;
    this.dispatchEvent(
      new CustomEvent('slideChange', {
        detail: {
          src: this.imgsSrc[this.currentIndex],
        },
      })
    );
  }

  get playing() {
    return this.intervalId != undefined;
  }

  #changeSlide(indexDiff) {
    this.currentIndex += indexDiff;
    this.#emitSlidChange();
  }

  moveForward() {
    if (!this.playing) {
      this.#changeSlide(1);
    }
  }

  moveBackward() {
    if (!this.playing) {
      this.#changeSlide(-1);
    }
  }

  play() {
    if (!this.playing) {
      this.intervalId = setInterval(
        this.#changeSlide.bind(this, 1),
        this.interval
      );
    }
  }

  stop() {
    if (this.playing) {
      clearInterval(this.intervalId);
      this.intervalId = undefined;
    }
  }
}
