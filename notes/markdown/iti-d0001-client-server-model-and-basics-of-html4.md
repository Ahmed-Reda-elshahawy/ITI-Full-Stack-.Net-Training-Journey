# 🔖 ITI - D0001 - Client Server Model & Basics of HTML4

## Client Server Model (Request, Response)

### Main Components

- Client (Browser like an interrupter)
- Server

#### Key Points

- We are looking to server as software
  - like ftp server, mail server, web server
- Server know the requests service using **port number**
  - DNS: 53
  - HTTP: 80
  - HTTPS: 443
  - FTP: 20, 21
- We use **protocols** (like HTTP, HTTPS)to set communication standard between client and server
- HTTP web server should have a public folder called _www_ which in the root directory
- URI (Uniform Resource Identifier) (e.g. `https://iti.com/index.html`)
  - URL (Universal Resource Locator) (e.g.`https://iti.com`)
  - URN (Uniform Resource Name) (e.g. index.html)

## Frontend Technologies

- HTML (Structure)
- CSS (Styling)
- JS (Interactivity (doing actions))

## HTML

- Stands for HyperText Markup Language

### HTML Features & Characteristics

- Interrupted Language
  - Direction from top to bottom, left to right
- Not case sensitive
- W3C
- Predefined tags and attributes
- HTML not save newlines and not save more than one space

> [!Note]
> Elements have a relationships (parent, child, siblings)

### Common Structure

- `html`: root element of document
- `head`: contains page title and meta data
- `body`: contains page content

> [!Note]
>
> - Self closing tags called mono tags
> - Comments in HTML using `<!--content-->`
> - Attributes added to tags to either add extra meta data for tag or use some attributes to change some styling or behavior based on featured attributes in HTML

### Dealing With Text

- **Headings `<h1...h6>...</h1...h6>`**
  - is a block element
  - common attributes:
    - title
    - align "center|left|right"
- **Paragraph `<p></p>`**
  - is a block element
- **Horizontal Row `<hr>`**
  - common attributes:
    - color
    - width
- **Pre formatted text `<pre>content</pre>`**
  - like font-family: monospace also it's aligned center
  - follow newlines and spaces you set in text
- **Break line `<br/>`**
- **Character Entities `&<entity>;`**
  - e.g. `&copy;`
  - e.g. `&nbsp;` (non breakable spaces)
- **Emojis**
- **Lists**
  - **Ordered List** `<ol><li>item_content</li></ol>`
    - ul common attributes
      - type : numbering style(e.g. type="a")
      - start : counting start (e.g. style="4")
  - **Unordered List** `<ul><li>item_content</li></ul>`
    - ul common attributes
      - type: bullet point shape (e.g. type="disc")
- `<dt></dt>` , `<dd></dd>`

#### Physical Style

- Bold `<b>content</b>` (semantic `<strong>`)
- Italic `<a>content</a>`
- `<sub></sub>`
- `<sup></sup>`
- `<s></s>`

### Dealing With Images

- Use `<img />` tag
- Inline-block element

```html
<img src="./img.png" alt="some pic" />
```

**Common Attributes:**

- `width`
- `height`
- `src`: image location path
- `alt`: alternative text (if alt not setted, it will take title value)
- `title`
- `border`: (just set width, and it will be solid border but we can't change it's style with html)
- `hspace` (set left and right margin for image)
- `vspace` (set top and bottom margin for image)
- `align`: "right|left|top|bottom|middle"
  - by default will be left
  - "left|right|bottom" not align for image, it's an align for text around image

#### Map

- Use `<map>content</map>`

#### Area

```html
<area name="" shape="" coords="" href="" title="" target="" />
<img usemap="" src="" alt="" />
```

**Example:**

```html
<img src="./img.png" alt="an image" usemap="#mymap" />
<map
  name="mymap"
  shape="rect"
  coords="10,20,30,40"
  tite="demo-map"
  target="_blank"
/>
```

### Dealing with Links

1. External links
2. Internal links
3. named anchor, self link
4. mailto link

```html
<a
  href="mailto:_email_?cc=_cc-emails..._&bcc=_bcc-emails..._&subject=...&body=..."
/>
<a href="tel:_telphone" />
```

> [!Note]
> To refer to something in the same page, we use `#element_name` (element_name which is value of `name` attribute)
