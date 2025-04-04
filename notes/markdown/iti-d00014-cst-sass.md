# 🔖 ITI - D0014 - CST - SASS

## SASS Overview

- SASS (Syntactically Awesome Style Sheets) is a CSS preprocessor that extends CSS with features like variables, nested rules, mixins, and functions.
- SCSS (Sassy CSS) is a superset of CSS that adds these features, making it easier to write and maintain complex stylesheets.
- SASS files have a `.scss` or `.sass` extension, with SCSS being the more common format due to its CSS-like syntax.
- Use Tools like `compass` to compile SASS files into standard CSS.

## SCSS vs. SASS

- SCSS uses curly braces `{}` and semicolons `;` to separate rules and properties, similar to CSS.
- SASS uses indentation to separate rules and properties, making it more concise but less familiar to those used to CSS.

## Sass Variables

- Define variables using `$` followed by the variable name and value.
- Naming convention: use lowercase letters and hyphens for multi-word variable names.

```scss
$primary-color: #3498db;
```

## Sass Nesting

- Nesting allows you to write CSS rules inside other rules, making it easier to read and maintain styles.
- Use `&` to reference the parent selector.

**Example:**

SCSS:

```scss .parent {
  color: red;

  .child {
    color: blue;
    &:hover {
      color: green;
    }
  }
}
```

Compiled CSS:

```css
.parent {
  color: red;
}

.parent .child {
  color: blue;
}

.parent .child:hover {
  color: green;
}
```

## Sass Loops

- SASS supports loops using `@for`, `@each`, and `@while` directives.
- Loops can be used to generate repetitive styles or classes.

**Example:**

SCSS:

```scss
@for $i from 1 through 3 {
  .item-#{$i} {
    width: 100px * $i;
  }
}
```

CSS:

```css
.item-1 {
  width: 100px;
}
.item-2 {
  width: 200px;
}
.item-3 {
  width: 300px;
}
```

> [!Tip]
>
> with `@for`, you can use `through` or `to` to specify the range of values.
> `through` includes the last value, while `to` excludes it.

## Sass Mixins

- Mixins allow you to create reusable styles that can accept arguments.
- Use `@mixin` to define a mixin and `@include` to use it.
- Mixins are like functions in programming languages, allowing you to pass parameters and generate dynamic styles.
- Mixins can also include other mixins, making them powerful for creating complex styles.

**Example:**

SCSS:

```scss
@mixin border-radius($radius) {
  -webkit-border-radius: $radius;
  -moz-border-radius: $radius;
  border-radius: $radius;
}

.box {
  @include border-radius(10px);
}
```

CSS:

```css
.box {
  -webkit-border-radius: 10px;
  -moz-border-radius: 10px;
  border-radius: 10px;
}
```

### Mixin to Build Grid System Like in Bootstrap

```scss
@mixin build-grid-system($column-numbers, $screen-name, $screen-break-point) {
  @media all and (min-width: $screen-break-point) {
    @for $i from 1 through 12 {
      .col#{if($screen-name=="xs",null, "-#{$screen-name}")}-#{$i} {
        width: 100/ ($column-numbers/$i) * 1%;
      }
    }
  }
}

@include build-grid-system(12, "xs", 0px);
```

## SASS 7-1 Pattern

This is an architecture pattern to organize projects using SASS with the following file structure:

- `base/`
- `components/`
- `layout/`
- `pages/`
- `themes/`
- `abstracts/`
- `venders`

and Finally `main.scss` file

After compilation we will have single `.css` file.
