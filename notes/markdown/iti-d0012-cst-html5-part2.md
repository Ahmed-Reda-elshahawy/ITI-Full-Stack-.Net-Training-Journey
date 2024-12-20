# 🔖 ITI - D0012 - CST (HTML5) (Part2)

## Shadows

### Text Shadow

```css
/* box-shadow: none|_h-offset v-offset blur spread color_ |inset|initial|inherit; */
text-shadow: 1px 1px 2px black;
text-shadow: -1px -2px red inset; /*show inside shape*/
```

### Box Shadow

```css
/*text-shadow: _h-shadow v-shadow blur-radius color_|none|initial|inherit;*/
box-shadow: 2px 2px 2px 1px rgb(0 0 0 / 20%);
```

## Manipulating `section` text

### Overflows

- `overflow`
- `overflow-x`
- `overflow-y`

```css
/*scroll: show scroller, even of no overflow */*
/*auto: show scroller incase of overflow*/
overflow: visible|hidden|clip|scroll|auto|initial|inherit;
```

> [!Note]
> If you handle just one direction of overflow, so by default other direction will be scroll-able

### Resize not resized tags

- Can't `resize` without handling `overflow`

```css
resize: none|both|horizontal|vertical|initial|inherit;
```

### Split section text into columns

```css
column-count: num_of_cols_value;
column-gap: col_gap_value;
column-rule: _column-rule-width column-rule-style
  column-rule-color_|initial|inherit;
```

> [!Note]
> You should to set `height` property and handle `overflow` property with column.

### Mark text with `<mark>`

```html
<p><mark>some marked text<mark></p>
```

## Transformation

- Apply a 2D or 3D transformation to an element using `transform` property
- Allows you to `rotate`, `scale`, `move`, `skew`.

### `rotate(angle)`

```css
transform: rotate(45deg)
transform: rotate(-50deg);
```

### `scale(sx, sy)`

Used to scale an element (both width and height)

```css
transform: scale(0.7); /* scale down*/
transform: scale(110%); /* scale up*/
transform: scale(1.1, 0.5);
transform: scale(-1, 1); /*mirroring (flipping)*/
```

### `translate(x, y)`

Used to change the position of an element

```css
transform: translate(50px); /* moves the element 50px along the x-axis, and 0px along the y-axis */
transform: translate(50px, 20px); /* moves the element 50px along the x-axis, and 20px along the y-axis */
transform: translate(100px, 30px); /* moves the element 100px along the x-axis, and 30px along the y-axis */
```

> [!Note] Apply Multiple function to `transform`
>
> ```css
> transform: rotate(0deg) scale(1.2);
> ```

```css
transform: rotate()
transform: scale(width, height) /* negative values will flip shape */
transform: translate(move-x, move-y)
transform: skew()
transform: scale()
```

## Transitions

Allows you to change property values smoothly, over a given duration

```css
transition: property duration;
```

**Example**:

```css
img {
  width: 200px;
  height: 200px;
  border-radius: 50%;
  opacity: 0.5;
  transform: rotate(-10deg);
  transition: all 2s;
  /*transition: opacity 2s, transform 2s;*/
}
```

> [!Note]
>
> `transition` will only work with properties, which takes numeric values (e.g. changing `color=red` to `color=blue` is not valid in this case)

## Animation

- **change** in **duration**
- declare animation using `keyframes`
- There are 2 types of animation changes
  - from - to
  - percent animation

```css
/* declare animation : using keyframes */
@keyframes sectionanimation {
  /* changes: from to, percent animation*/
  from {
    width: 400px;
    height: 400px;
  }
  to {
    width: 400px;
    height: 400px;
  }
}

section {
  animation: sectionanimation;
  animation-duration: 2s;
  animation-iteration-count: 3;
  /*animation-iteration-count: infinite;*/
  animation-direction: alternate;
  animation-delay: 2s;
}

section:hover {
  animation-play-state: pause;
  /* animation-direction: reverse */
  /* animation-direction: alternate-reverse */
}
```

if `from` will contain the same state of the default state, so we can ignore writing `from`

**Percent Animation Example**:

```css
/* declare animation : using keyframes */
@keyframes sectionanimation {
	/* changes: from to, percent animation*/
	10% {
		background-color: green;
		background-image: url('./m)
	}
	30% {
		background-color: blue;
	}
	50% {
	background-color: yellow;
	}
	70% {
	background-color: red;
	}
	90% {
	background-color: cyan;
	}
}

section {
	animation: sectionanimation;
	animation-duration: 2s;
	animation-iteration-count: 3;
	/*animation-iteration-count: infinite;*/
	animation-direction: alternate;
	animation-delay: 2s;
}

section:hover {
	animation-play-state: pause;
	/* animation-direction: reverse */
	/* animation-direction: alternate-reverse */
}
```

## CSS Variables

```css
section {
  --main-color: red;
  color: var(--main-color);
}
```

**Global variables (inside `:root`)**:

```css
:root {
  --main-color: red;
}

section {
  color: var(--main-color);
}

p {
  color: var(--main-color);
}
```

## Box Sizing

- `border-box`

```css
* {
  padding: 0px;
  margin: 0px;
  box-sizing: border-box;
}
```

## Measure Units

### Absolute Measure Units

- `px` pixel
- `cm` centimeters

### Relative Measure Units

- `%` (relative to the same property name in the parent element)
- `em` (relative to the font size of the same element)
- `rem` (relative to the font size for root:html)
- `vh` (relative to height of viewport)
- `vw` (relative to width of viewport)
- `vmin` (relative to min dimension (width or height) of viewport)
- `vmax` (relative to max dimension (width or height) of viewport)

> [!NOTE]
> You must not have a horizontal scroll in responsive design

## Flex

### Parent (Flex container) Properties

```css
display: flex;
flex-direction: row|column|row-reversed|column-reversed;
flex-wrap: no-wrap|wrap;
justify-content: flex-start|flex-end|center|space-between|space-around|space-evenly;
align-items: center|flex-start|flex-end|stretch|baseline|normal;
```

### Child (Flex items) Properties

```css
flex-grow: 0|1;
flex-shrink: 1|0;
flex-basis: 250px; /* refers to min-width or min-height of item according to flex direction */
flex: grow shrink basis;
align-self: flex-start|center|flex-end;
order: ;
```

## Grid

Can create grid using 2 methods:

- `template columns`, `template rows`
- `template area`

### Parent (Grid Container)

```css
display: grid
grid-template-columns: repeat(3, 1fr) 200px;
grid-template-rows: repeat(3, 100px);
gap: 10px 10px;
```

### Child (grid items) Properties

```css
grid-row: val1/val2; /* e.g. 1/2 */
grid-column: val1/val2; /* e.g. 1/4 */
```

### Grid Template Area

Is used to solve maintainability issue with Grid

```css
.item1 {  grid-area: myArea;}
.grid-container {  display: grid;
  grid-template-areas:
    'myArea myArea . . .'
    'myArea myArea . . .';
}
```
