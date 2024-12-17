# 🔖 ITI - D0008 - CST (JavaScript 5, ECMA5) (Part 5)

## document.cookie

- Save data permanent for period of time
- Sharing data between different pages within the same domain
- Store data in it as key-value pairs
- Data will be stored as string
- Max size $4KB$

### Session Cookie

- Will be expired at the end of user session

```js
var username = "John";
document.cookie = `username=${username}`;
```

### Persistent Cookie

- Will be expired at the end of expiry date

```js
var username = "John";
var expiresAt = new Date(2025, 0, 15);
document.cookie = `username=${username};expires=${expiresAt}`;
```

## Events

- Events are actions that respond to user's specific actions
- Events are controlled in JS using event handlers that indicate what actions the browser takes in response to an event
- Event handlers are created as attributes added to the HTML tags in which the event is triggered
- An Event handler adopts the event name and appends the word "on" in front of it.

```js
<tag onEvent="JS commands;">
```

- Thus, the "click" event becomes the `onclick` event handler

### Event object common properties

- **`target`** Refers to the element that triggered the event

```js
document.addEventListener("click", function (event) { console.log(event.target); // Logs the element that was clicked });
```

- **`type`** Indicates the type of event that occurred, such as `click`, `keyword`, `mousemove`, etc.

```js
document.addEventListener("click", function (event) { console.log(event.type); // Logs "click" });
```

- **`timeStamp`** Represents the time (in milliseconds) when the event was created.

```js
document.addEventListener("click", function (event) { console.log(event.timeStamp); // Logs the time of the event in milliseconds });
```

- **`bubbles`** Indicates whether the event is a **bubbling event** - if `true`, the event bubbles up from the target element to its ancestors in the DOM hierarchy.
  > [!Note]
  >
  > **What is Event Bubbling?**
  >
  > - When an event occurs on an element, it propagates upward to its parent elements unless stopped.
  > - Example: Clicking a child element triggers the parent element's event listener if `bubbles` is `true`.

```js
document.addEventListener("click", function (event) {
  console.log(event.bubbles); // Logs true for most common events like "click"
});
```

- **`cancelable`** Indicates whether the event's default action can be _prevented_.
  - if `ture`, calling `event.preventDefault()` will cancel the default behavior associated with the event
  - Not all events are cancelable, for example `click` is cancelable, but `mousemove` is not.

```js
document.addEventListener("click", function (event) { if (event.cancelable) { event.preventDefault(); // Cancels the default action (e.g., following a link) console.log("Default action prevented"); } });
```

### Pointer (mouse) events

#### mouse common event handlers

- **`onMouseDown`** When pressing any of the mouse buttons
- **`onMouseMove`** when the user moves the mouse pointer within an element
- **`onMouseOut`** when moving the mouse pointer out of an element.
- **`onMouseUp`** When the user releases any mouse button pressed
- **`onMouseOver`** when the user moves the mouse pointer over an element.
- **`onClick`** When clicking the left mouse button on an element.
- **`onDblClick`** When Double-clicking the left mouse button on an element.
- **`onDragStart`** When the user has begun to select an element

#### mouse common event object properties

- **`screenX`** returns the horizontal coordinate of the mouse pointer, _relative to the screen_, when the mouse event was triggered
- **`screenY`** returns the vertical coordinate of the mouse pointer, _relative to the screen_, when the mouse event was triggered
- **`clientX`** returns the horizontal coordinate of the mouse pointer, _relative to the current window_, when the mouse event was triggered
- **`clientY`** returns the vertical coordinate of the mouse pointer, _relative to the current window_, when the mouse event was triggered
- **`pageX`** returns the horizontal coordinate or the mouse pointer, _relative to the document_, when the mouse event was triggered
- **`pageY`** returns the vertical coordinate of the mouse pointer, _relative to the document_, when the mouse event was triggered
- **`offsetX`** the horizontal coordinate of the mouse pointer relatively to _the target element_
- **`offsetY`** the vertical coordinate of the mouse pointer relatively to _the target element_
- **`altKey`** True if the alt key was also pressed
- **`ctrlKey`** True if the ctrl key was also pressed
- **`shiftKey`** True if the shift key was also pressed
- **`detail`** Returns a number that indicates how many times the mouse was clicked
- **`button`** Any mouse buttons that are pressed.
  - `0`: left mouse button
  - `1`: wheel button
  - `2`: right mouse button
- **`movementX/movementY`** The X or Y coordinate of the mouse pointer relative to the position of the last `mousemove` event.

### Keyboard Events

#### keyboard common event handlers

- `onKeyDown` when user presses a key
- `onKeyPress` when user holds down a key
- `onKeyUp` when user lifts a key

> [!Note]
>
> - `keyup` and `keydown` can deal with printed and unprinted
> - `keyup` isn't preventable

#### keyboard common event object properties

- **`altKey`**: True if the alt key was also pressed
- **`ctrlKey`**: True if the alt key was also pressed
- **`shiftKey`**: True if the alt key was also pressed
- **`code`**: Returns String with _the code value_ of the key represented by the event.
- **`key`**: Returns String representing _the key value_ of the key represented by the event.

### Event Object Methods

- **`preventDefault()`**: Cancels the event if it is cancelable, meaning that the default action that belongs to the event will not occur.
- **`stopPropagation()`**: Prevents further propagation of the current event.

#### Event Direction (propagation)

- bubble (bottom to up) (child to parent)
- capture (up to bottom) (parent to child)

**use cases:**

- cares about it when child and parent are interested to the same event, so we control this behavior when we register events with `addEventListener`

```js
event.stopPropagation();
event.cancleBubble = true;
```

### Event Registration

#### HTML Event Registration (Direct Registration)

**Example: Register event and send HTML tag**

```html
<input type="button" value="ButtonOne" onClick="myFunc(this);" />
<input type="button" value="ButtonTwo" onClick="myFunc(this);" />
<input type="button" value="ButtonThree" onClick="myFunc(this);" />
```

```js
function myFunc(ele) {
  alert(`${ele.value} clicked!`);
}
```

**Example: Register event and send event**

```html
<input type="button" value="ButtonOne" onClick="myFunc(event);" />
<input type="button" value="ButtonTwo" onClick="myFunc(event);" />
<input type="button" value="ButtonThree" onClick="myFunc(event);" />
```

```js
function myFunc(event) {
  alert(`${event.target.value} clicked!`);
}
```

> [!Note]
>
> `event.target` refers to the element that triggered the event

#### JS Event Registration

**Example: Register event and send event**:

```html
<input type="button" value="ButtonOne" />
<input type="button" value="ButtonTwo" />
<input type="button" value="ButtonThree" />
```

```js
function myFunc(event) {
  alert(`${event.target.value} clicked!`);
}

var allbtns = document.getElementsByTagName("input");
for (var btn of allbtns) {
  // btn.onClick = myFunc;  // registration
  btn.onClick = function (event) {
    alert(`${event.target.value} clicked!`);
  };
}
```

> [!Note]
>
> **`this` refers to what**
>
> - In HTML event registration, `this` will refer to tag element
> - In JS event registration, `this` will refer to listener object
>   - `target` refers to object which event happen to it

##### `window.onload`

```html
<body onload="doSomething();" />
```

Or use it with JS

```js
window.onload = doSomething;
```

> [!Note]
> Also we can use `DOMContentLoaded`

##### addEventListener

```js
ele.addEventListener("event_name without on", "event handler");
```

##### Event multicasting with addEventListener

if an error happens in a handler in our handlers pipeline , and we need to stop the whole pipeline after it,use `stopImmediatePropagation`

```js
event.stopImmediatePropagation();
```

##### Remove events from element

```js
ele.removeEventListener("event_name without on");
```

> [!Note]
>
> **Advantages of addEventListener**
>
> - Ability of Multi-casting
> - Ability of removing event listeners, with `removeEventListener`
> - Ability to Control when to stop propagation using `stopImmediatePropagation`

### `this` Vs. `target`

`this` In the context of events with JS registration, `this` will refer to listener, otherwise `target` will refer to the actual element which triggers the event

for example, if we register `click` event to the table rows so when the table cell will be clicked, `this` will refer to the row and `target` will refer to the table cell

### Direct event registration Vs. Delegation event registration

- Direct event registration as examples above
- Delegation event registration, as we will delegate an element which register events automatically to future events (e.g. register event in `tBody` to apply them automatically on the future added rows `tr`)

```js
document.querySelector("tbody").addEventListener("click", function (e) {
  if (e.target && e.target.type === "checkbox") {
    console.log("checkbox clicked");
  }
});
```

## Regex

### Regex Declaration in JS

- Literal Declaration
- Constructor Declaration

#### Regex: Literal Declaration

```js
var pattern = //;
```

#### Regex: Constructor Declaration

```js
var pattern = new RegExp(//);
```

### Pattern flags

- `i` case-insensitive
- `g` global
- `m` multiple lines

### Pattern symbols

- `^` starts with
- `$` ends with
- `()` grouping
- `[]` match single character within `[]`
- `[^]` not match any character within `[]`
- `\d` match digit

> [!Note]
> When pattern doesn't contains `^` or `$` that means will match words that contains our pattern, so doesn't matter pattern is exist as prefix or postfix or at the middle or string, otherwise you can make a prefix match or postfix match or exact match

### Match pattern methods

- string.match(regex) => matched_string | null
- regex.test(string) => boolean
- regex.exec(string) => matched_string | null

### String methods which accept regex

- `match` returns an array of matches
- `search` returns the position of the first match
- `replace` allows you to substitute matched text with another string

_**[Back to the Index](../../README.md#index)**_
