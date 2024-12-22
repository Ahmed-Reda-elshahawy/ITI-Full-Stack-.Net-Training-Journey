# 🔖 ITI - D0011 - CST (HTML5)

## HTML5

Added some new features

### Semantic Tags

- `section`
- `nav`
- `aside`
- `header`
- `footer`

> [!Note]
>
> - Semantic Tags provides logical meaning of elements
> - Improves accessibility of web pages

### CSS 3.0

- Transformation: `scale`, `rotate`, `skew`, `translate`
- Transition
- Animation: from to . %
- Display: flex, grid
- Media query
- Shadows, box, text

### JS APIs

- Media API: Video, audio
- Canvas
- Browser Storage: `localStorage`, `sessionStorage`, `DragAndDrop`

## HTML 5 Document

- HTML5 Document must start with `<!DOCTYPE html>`
- `<html lang="en">`
- inside head tag
  - `meta charset="UTF-8"`
    - [unicode.org](https://home.unicode.org/)
  - Must have a `<tilte></title>` tag

### New CSS Selectors

- `nth-child(number)`
  - go to every parent and check if we have nth-child of the same type
  - `nth-child(odd)`
  - `nth-child(even)`
  - `nth-child(formula)`

```css
/*go for every element and make first child background to be red if and only if this child is paragraph*/
p:nth-child(1) {
  background-color: red;
}

p:nth-child(odd) {
  /*using odd*/
  background-color: red;
}

p:nth-child(even) {
  /*using even*/
  background-color: red;
}

p:nth-child(2n + 1) {
  /*using equation*/
  background-color: red;
}

element:nth-child(n + 3):nth-child(-n + 7) {
} /* select range starts from 3rd element to 7th element*/
```

- `first-of-type`

```css
/* means select first paragraph inside each div element and make it's background color to be red*/
div p:first-of-type {
  background-color: red;
}
```

- `last-of-type`
- `only-of-type`
- `only-of-child`
- `first-child`

### MediaQuery

- **Media queries** in CSS are a feature that allows you to apply styles based on specific conditions, like screen size, resolution, orientation, or other device characteristics.
- Adaptive layouts

#### Basic Syntax

```css
@media (condition) {
  /* CSS rules */
}
```

#### Key Components

- **Media Types**: Specify the type of device
  - `all`: default, applies to all devices
  - `screen`: (desktop, tablet, mobile) screen
  - `print`: for printed materials
- **Media Features**: Define the conditions to match
  - **`width/height`**
    - `max-width`: Maximum width.
    - `min-width`: Minimum width.
  - **`resolution`**
    - `min-resolution`: Minimum DPI.
  - **`orientation`**
    - `landscape`: Wider than tall.
    - `portrait`: Taller than wide.
- **Logical Operators**:
  - `and`: Combine multiple conditions.
  - `not`: Exclude a condition.
  - `only`: Target only specified devices.

```css
@media (min-width: 600px) and (max-width: 1200px) and (orientation: landscape) {
  body {
    background-color: lightgreen;
  }
}
```

## XML

- XML is a markup language that provides rules to define any data
- An XML document with correct syntax is called "Well Formed".
- An XML document validated against a **DTD** (Document Type Definition) is both "Well Formed" and "Valid".

## XHTML

- XHTML stands for **Extensible HyperText Mark-up Language**
- New and more well-structured way of writing HTML
- Contains of all the elements in HTML 4.01, combined with the strict syntax of XML.
