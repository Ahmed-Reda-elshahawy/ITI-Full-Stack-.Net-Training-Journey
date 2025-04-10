# 🔖 ITI - D0014 - CST - Bootstrap (Draft)

## BootStrap Overview

- Bootstrap is a UI framework which contains built-in CSS and JS components ready to build anything fast.
- Bootstrap contains 2 folders `css`, `js`
  - `css` contains predefined css classes you can use.
  - `js` contains predefined js components you can use.

_[Bootstrap v5.3 - Official Docs](https://getbootstrap.com/docs/5.3/getting-started/introduction/)_.

## How to Include Bootstrap in your Project

- Download precompiled files offline and include them in your project.
- Can be downloaded using package managers like (`npm`, etc...).
- Use CDN link

**CDN Links**:

- **CSS**: https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/css/bootstrap.min.css
- **JS**: https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/js/bootstrap.bundle.min.js

## Layouts

### Breakpoints

Breakpoints are customizable widths which determines how your responsive design will behave on across different devices (e.g. mobile, desktop, etc...).

| Breakpoint        | Class infix | Dimensions |
| ----------------- | ----------- | ---------- |
| Extra small       | `None`      | <576px     |
| Small             | `sm`        | ≥576px     |
| Medium            | `md`        | ≥768px     |
| Large             | `lg`        | ≥992px     |
| Extra large       | `xl`        | ≥1200p     |
| Extra extra large | `xxl`       | ≥1400px    |

### Containers

Containers are the fundamental building block of Bootstrap which pad and align our content responsively with the viewport.

Containers uses **Bootstrap Grid System**.

Bootstrap has 3 different containers:

- **`.container`** - takes `max-width` on all screens.
- **`.container-{breakpoint}`** - `width:100%` until reaches specified breakpoint.
- **`.container-{fluid}`** - `width:100%` at all break points.

_Have a quick look at [containers at Bootstrap Docs](https://getbootstrap.com/docs/5.3/layout/containers/#:~:text=%E2%89%A51400px-,.container,-100%25)_.

### Bootstrap Grid System

Bootstrap has a 12 column grid system which can be used to build responsive layouts.

Bootstrap uses a series of containers, rows and columns to layout and align content which already built-in using css `flexbox`.

**Example**:

```html
<div class="container text-center">
  <div class="row">
    <div class="col">Column</div>
    <div class="col">Column</div>
    <div class="col">Column</div>
  </div>
</div>
```

## Content

### Typography

- **`.h1` through `.h6` classes**: Style normal text like HTML heading styling.
- **`.display-1` through `.display-6` classes**: Style some text to standout from the regular text.
- **`.lead`**: also used to standout text from the regular text.
- **`.text-decoration-underline` , `.text-decoration-line-through ` classes**: to manipulate text decorations.
- **`.text-start`, `.text-center`, `.text-end` classes**: for text alignment.
- **`.text-danger`, `.text-primary`, ... classes**: for text colors.
- **`fw-bold`, ... classes**: for text weight.
- **`fs-1` through `fs-6` classes**: for text size.

### Lists

- **`.list-unstyle` class**: completely unstyled list.
- **`list-inline` class**: inline unstyled list.
- **`list-inline-item` class**: used with list items to them inline.

### Icons

We can use icon library like:

- [Bootstrap icons](https://icons.getbootstrap.com/)
- [fontawesome](https://fontawesome.com/)

### Tables

Use this base class `.table` to style table in Bootstrap then we can use set of contextual classes to color tables, table row or individual cells.

`table-{color}` class is used with `<table>`, `<tr>`, `<td>` and `<th>`.

```html
<table class="table table-primary"></table>
<td class="table-dark"></td>
```

#### Accented tables

**Stripped rows**:

```html
<table class="table table-striped">
  ...
</table>
```

**Stripped columns**:

```html
<table class="table table-striped-columns">
  ...
</table>
```

#### Active tables

`.table-active` class is used to highlight table row or a cell.

_Take a quick look at Tables on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/content/tables/)_

## Utilities

### Spacing

Bootstrap provides a shorthand responsive margin, padding, and gap utility classes to modify element's appearance easily.

#### Spacing Notation

- `m` - margin
- `p` - padding
- `t` - top
- `b` - bottom
- `s` - start (equivalent of left or right in css based on direction rtl or ltr)
- `e` - end (same as start)
- `x` - set values for (left and right)
- `y` - set values for (top and bottom)

#### Spacing Values

Bootstrap uses `$spacers` variables in Sass to easily set ranges for spacing, so it set `$spacer` value by default to `3rem` and compute following values as percentage from this `$spacer`

- `0` - eliminate the `margin` and `padding`.
- `1` - `$spacer *.25`
- `2` - `$spacer * .5`
- `3` - `$spacer`
- `4` - `$spacer * 1.5`
- `5` - `$spacer * 3`

## Bootstrap with JS

- Bootstrap v3 JS components is built using JQuery, also depends on `Popper.js`.
  - JQuery with philosophy of `write less do more`.
  - `Popper.js` is used to handle positioning.
- Bootstrap v4 JS components is built using native JS without using JQuery, but you still need to include `Popper.js` in your project.
- Bootstrap v5 JS have a bundle file which contains `Popper.js` with `bs.js`.

## Bootstrap Components based on JS

Bootstrap have a wide-range of components which uses JS to do some behaviors like:

- Dropdown menus
- Togglers
- Alerts
- Accordions
- Collapse
- etc...

Those components most of the time depends on HTML `data-*` attributes like:

- `data-bs-dismiss="alert"`
- `data-bs-toggle="dropdown|collapse`
- `data-bs-target`
- `data-bs-interval`
- `data-bs-side-to`
- `data-bs-slide`
- `data-bs-ride`
- `data-bs-slide`

## Components

### Breadcrumb

Breadcrumb uses to indicate current page's location with in a navigational hierarchy.

We use an ordered list or ordered list with list items to create minimal styled breadcrumbs.

**Example**:

```html
<nav aria-label="breadcrumb">
  <ol class="breadcrumb">
    <li class="breadcrumb-item active" aria-current="page">Home</li>
  </ol>
</nav>

<nav aria-label="breadcrumb">
  <ol class="breadcrumb">
    <li class="breadcrumb-item"><a href="#">Home</a></li>
    <li class="breadcrumb-item active" aria-current="page">Library</li>
  </ol>
</nav>

<nav aria-label="breadcrumb">
  <ol class="breadcrumb">
    <li class="breadcrumb-item"><a href="#">Home</a></li>
    <li class="breadcrumb-item"><a href="#">Library</a></li>
    <li class="breadcrumb-item active" aria-current="page">Data</li>
  </ol>
</nav>
```

_Take a quick look at Breadcrumb component on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/breadcrumb/)_.

### Badges

```html
<button type="button" class="btn btn-primary">
  Notifications <span class="badge text-bg-secondary">4</span>
</button>
```

_Take a quick look at Badge component on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/badge/)_.

### Progress bars

- Use `.progress` container which will wrap the progress bar.
- Add inner container `.progress-bar`.

**Example**:

```html
<div
  class="progress"
  role="progressbar"
  aria-label="Basic example"
  aria-valuenow="0"
  aria-valuemin="0"
  aria-valuemax="100"
>
  <div class="progress-bar" style="width: 0%"></div>
</div>
<div
  class="progress"
  role="progressbar"
  aria-label="Basic example"
  aria-valuenow="25"
  aria-valuemin="0"
  aria-valuemax="100"
>
  <div class="progress-bar" style="width: 25%"></div>
</div>
```

_Take a quick look at Progress component on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/progress/)_.

### Buttons

Buttons should take a base class `.btn` and then we can add contextual customizable classes like `btn-{colo}`, `btn-outline-{color}`, etc...

_Take a quick look at Buttons on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/buttons/)_.

### Button groups

Group a series of buttons together on a single line or stack them in a vertical column.

**Example**:

```html
<div class="btn-group" role="group" aria-label="Basic example">
  <button type="button" class="btn btn-primary">Left</button>
  <button type="button" class="btn btn-primary">Middle</button>
  <button type="button" class="btn btn-primary">Right</button>
</div>
```

_Take a quick look at Button Group on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/button-group/)_.

### Alerts

Alerts are used to provide contextual messages based on user action.

Each alert should take base `.alert` class and then can have some contextual classes like `.alert-{color}`.

#### Alert Dismissing

Alert component have some JS functionality shipped with it, like dismissing (closing) alert, which can be achieved by add `.alert-dismissible` contextual class to the alert and add button which data attribute `data-bs-dismiss="alert"`.

**Example**:

```html
<div class="alert alert-warning alert-dismissible fade show" role="alert">
  <strong>Holy guacamole!</strong> You should check in on some of those fields
  below.
  <button
    type="button"
    class="btn-close"
    data-bs-dismiss="alert"
    aria-label="Close"
  ></button>
</div>
```

_Take a quick look at Alerts on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/alerts/)_.

### Dropdowns

Dropdowns are toggleable, contextual overlays for displaying lists of links and more.

**Example**:

```html
<div class="dropdown">
  <button
    class="btn btn-secondary dropdown-toggle"
    type="button"
    data-bs-toggle="dropdown"
    aria-expanded="false"
  >
    Dropdown button
  </button>
  <ul class="dropdown-menu">
    <li><a class="dropdown-item" href="#">Action</a></li>
    <li><a class="dropdown-item" href="#">Another action</a></li>
    <li><a class="dropdown-item" href="#">Something else here</a></li>
  </ul>
</div>
```

_Take a quick look at Dropdowns on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/dropdowns/)_.

### Cards

A card is a flexible and extensible content container. It includes options for headers and footers, a wide variety of content, contextual background colors, and powerful display options.

**Example**:

```html
<div class="card" style="width: 18rem;">
  <img src="..." class="card-img-top" alt="..." />
  <div class="card-body">
    <h5 class="card-title">Card title</h5>
    <p class="card-text">
      Some quick example text to build on the card title and make up the bulk of
      the card's content.
    </p>
    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
</div>
```

_Take a quick look at Cards on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/card/)_.

### Carousels

The carousel is a slideshow for cycling through a series of content, built with CSS 3D transforms and a bit of JavaScript.

**Example**:

```html
<div id="carouselExample" class="carousel slide">
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img src="..." class="d-block w-100" alt="..." />
    </div>
    <div class="carousel-item">
      <img src="..." class="d-block w-100" alt="..." />
    </div>
    <div class="carousel-item">
      <img src="..." class="d-block w-100" alt="..." />
    </div>
  </div>
  <button
    class="carousel-control-prev"
    type="button"
    data-bs-target="#carouselExample"
    data-bs-slide="prev"
  >
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button
    class="carousel-control-next"
    type="button"
    data-bs-target="#carouselExample"
    data-bs-slide="next"
  >
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>
```

_Take a quick look at Carousels on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/carousel/)_.

### Modal

Use Bootstrap’s JavaScript modal plugin to add dialogs to your site for lightboxes, user notifications, or completely custom content.

**Example**:

```html
<!-- Button trigger modal -->
<button
  type="button"
  class="btn btn-primary"
  data-bs-toggle="modal"
  data-bs-target="#exampleModal"
>
  Launch demo modal
</button>

<!-- Modal -->
<div
  class="modal fade"
  id="exampleModal"
  tabindex="-1"
  aria-labelledby="exampleModalLabel"
  aria-hidden="true"
>
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
        <button
          type="button"
          class="btn-close"
          data-bs-dismiss="modal"
          aria-label="Close"
        ></button>
      </div>
      <div class="modal-body">...</div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
          Close
        </button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
```

_Take a quick look at Modals on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/components/modal/)_.

## Forms

- `.form-control` - gives inputs custom styles and gluing effects on focusing, etc...
- `.input-group` - extending form controls by adding text, buttons, or button groups on either side of textual inputs, custom selects, and custom file inputs.
- `floating-labels` - creates a beautifully simple form labels that float over your input fields.

**Example**:

```html
<div class="form-floating mb-3">
  <input
    type="email"
    class="form-control"
    id="floatingInput"
    placeholder="name@example.com"
  />
  <label for="floatingInput">Email address</label>
</div>
<div class="form-floating">
  <input
    type="password"
    class="form-control"
    id="floatingPassword"
    placeholder="Password"
  />
  <label for="floatingPassword">Password</label>
</div>
```

_Take a quick look at Forms on [Bootstrap Docs](https://getbootstrap.com/docs/5.3/forms/overview/)_
