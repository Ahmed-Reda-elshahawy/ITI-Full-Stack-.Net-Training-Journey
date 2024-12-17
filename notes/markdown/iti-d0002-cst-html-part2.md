# 🔖 ITI - D0002 - CST (HTML Part2)

## Tables in HTML

### Common tags

- **table**: defines table
- **th**: defines a header cell in a table
- **tr**: defines a row
- **td**: defines a cell in a table
- **caption**: defines a table caption
- **thead**: groups the header content in a table
- **tbody**: groups the body content in a table
- **tfoot**: groups the footer content in a table

**Example:**

```html
<table>
  <tr>
     
    <th>Company</th>
      
    <th>Contact</th>
    <th>Country</th>
  </tr>
  <tr>
     
    <td>Alfreds Futterkiste</td>
      
    <td>Maria Anders</td>
    <td>Germany</td>
  </tr>
  <tr>
    <td>Centro comercial Moctezuma</td>
       
    <td>Francisco Chang</td>
    <td>Mexico</td>
  </tr>
</table>
```

> [!Note]
> By default, table is border-less, and content is aligned left, and also width is automatically fit content

> [!Note]
> When use precentage value for any attribute, os it's a percentage from it's parent value

### Common Attributes

```html
<table
  width
  height
  boder
  bordercolor
  align=“left|center|right”
  bgcolor=“color”
  background=“” cellspacing cellpadding=>
<tr, td, th
  align="“left|"
  center|right”
  valing="“top|middle|"
  bottom”
  bgcolor="“color”"
  background="“”"
></tr>
```

## Form

```html
<form method="“get|post”" action="“URL" : Send Data To This URL”>
  <!– Write Controls to Collect Data -->
  <input
    type="“text”"
    ="“password”"
    ="“radio”"
    |checkbox|file|hidden|submit|reset
  />
  <Select => selection menu | selection list textarea : use to insert data in
  multi line format
</form>
```

### Selection List Vs. Selection Menu

- Selection menu, (choose one from many)
- Selection List, (choose many from many)

both of them are added by select tag

_**[Back to the Index](../../README.md#index)**_
