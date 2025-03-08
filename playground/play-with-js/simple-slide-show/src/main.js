import { ImageSliderComponent } from './ImageSliderComponent.js';

const slider = document.getElementById('slider');
const nextBtn = document.getElementById('next-btn');
const prevBtn = document.getElementById('prev-btn');
const playBtn = document.getElementById('play-btn');
const stopBtn = document.getElementById('stop-btn');

const imgsSrc = [
  '../imgs/1.jpg',
  '../imgs/2.jpg',
  '../imgs/3.jpg',
  '../imgs/4.jpg',
  '../imgs/5.jpg',
  '../imgs/6.jpg',
  '../imgs/7.jpg',
];

const imgSliderComponent = new ImageSliderComponent(imgsSrc);

nextBtn.onclick = imgSliderComponent.moveForward.bind(imgSliderComponent);
prevBtn.onclick = imgSliderComponent.moveBackward.bind(imgSliderComponent);
playBtn.onclick = imgSliderComponent.play.bind(imgSliderComponent);
stopBtn.onclick = imgSliderComponent.stop.bind(imgSliderComponent);

imgSliderComponent.addEventListener(
  'slideChange',
  (e) => (slider.src = e.detail.src)
);
