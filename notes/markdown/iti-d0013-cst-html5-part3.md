# 🔖 ITI - D0012 - CST (HTML5) (Part3)

## New Added APIs

- Media
- Drag and Drop
- Form
- Geolocation
- Canvas

## Media APIs

### Video

```html
<video src="./video/universe.mp4" controls />
```

> [!Note]
> Each browser has its own video controls icons, i.e. `controls` is a browser dependent

#### Video Controls

- **Play/Pause**: Start or stop the video.
- **Volume**: Adjust sound levels.
- **Mute/Unmute**: Toggle audio.
- **Seek Bar**: Navigate to different parts of the video.
- **Full Screen**: Expand the video to full-screen mode.
- **Loop**: Replay the video continuously.

#### Video element properties

- `currentTime`: Get or set the current playback position.
- `duration`: Total length of the video (in seconds).
- `paused`: Returns `true` if the video is paused.
- `muted`: Returns `true` if the video is muted.
- `loop`: Enable or disable looping.
- `volume`: Adjust playback volume (value between `0.0` and `1.0`).
- `playbackRate`: Control playback speed.

#### Methods

- `play()`: Start playback.
- `pause()`: Pause playback.
- `requestFullscreen()`: Switch to full-screen mode.
- `load()`: Reload the video resource.
- `addEventListener('event', callback)`: Handle events like `play`, `pause`, or `ended`.

## Drag And Drop

### Events

- `dragstart`
- `dragend`
- `dragover`
- `dragleave`
- `dragenter`
- `drop`

## Forms

### New Attributes

- `required`
- `min`, `max`, `step` used with range input type
- `placeholder`

### New Input types

- `datalist`
- `color`
- `date`
- `time`
- `month`
- `week`
- `search`
- `tel` refers to telephone numbers
- `number`
- `range`
- `email`
- `url`

## Storage API

- [Cookies](./iti-d0008-cst-javascript-ecma5-part5.md#documentcookie)
- [Local Storage](./iti-d0006-cst-javascript-ecma5-part3.md)

## Geolocation API

- The Geolocation API allows web applications to access the geographical location of the user's device, with the user's consent.

### Core Methods

**`getCurrentPosition(success, error?, options?)`**

```js
navigator.geolocation.getCurrentPosition(
  (position) => {
    console.log("Latitude:", position.coords.latitude);
    console.log("Longitude:", position.coords.longitude);
  },
  (error) => {
    console.error("Error:", error.message);
  }
);
```

### Key Properties of `position` Object

- **`coords`**:
  - `latitude`: The latitude of the position.
  - `longitude`: The longitude of the position.
  - `altitude`: Altitude in meters above sea level (may be `null`).
  - `accuracy`: Accuracy level of latitude and longitude (in meters).
  - `altitudeAccuracy`: Accuracy of the altitude (may be `null`).
  - `heading`: Direction of travel in degrees (may be `null`).
  - `speed`: Speed in meters per second (may be `null`).
- **`timestamp`**:
  - The time at which the position was determined.
