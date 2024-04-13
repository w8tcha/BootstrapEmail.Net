# BootstrapEmail.NET
<br>
  <p align="center">
  <a href="https://bootstrapemail.com">
    <img src="https://bootstrapemail.com/img/icons/logo.png" alt="" width=72 height=72>
  </a>
  <p align="center">
    If you know Bootstrap, you know Bootstrap Email.
  </p>
<br>

[![NuGet](https://img.shields.io/nuget/v/BootstrapEmail.Net.svg)](https://nuget.org/packages/BootstrapEmail.Net)

[![build dotnet](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml/badge.svg)](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=w8tcha_BootstrapEmail.Net&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=w8tcha_BootstrapEmail.Net)

This is the .NET Version of  [Bootstrap Email](https://github.com/bootstrap-email/bootstrap-email) which was converted from Ruby to .NET (Core) 7/8

Bootstrap Email takes most of its inspiration from these two wonderful frameworks, [Bootstrap](https://getbootstrap.com) and [Tailwind](https://tailwindcss.com) but for HTML emails. Working with HTML in emails is never easy because of the nuances of email vs the web. With Bootstrap Email you don't have to understand all the nuance and it allows you to write emails like you would a website.

## Setup
There are a few different ways you can use Bootstrap Email to compile emails:

### Use the dll

#### compile all files ending in .html in the current directory

```c#
var bsEmail = new BootstrapEmail();

bsEmail.Compile(string.Empty, string.Empty, InputType.File);
```
#### compile the file email.html and save it to the file out.html

```c#
var bsEmail = new BootstrapEmail();

bsEmail.Compile("email.html", "out.html", InputType.File);
```
#### specify a path pattern and a destination directory for compiled emails to be saved to

```c#
var bsEmail = new BootstrapEmail();

bsEmail.Compile("emails/*", "mails/compiled/", InputType.Pattern);
```

#### compile for a string

```c#
var bsEmail = new BootstrapEmail();

bsEmail.Compile("<a href='#' class='btn btn-primary'>Some Button</a>", string.Empty, InputType.String);
```

### Via the command line with the BootstrapEmailNet.Cli:

#### compile all files ending in .html in the current directory
```` cmd
> BootstrapEmail.Cli
````
#### compile the file email.html and save it to the file out.html
```` cmd
> BootstrapEmail.Cli -f email.html -d out.html
````
#### specify a path pattern and a destination directory for compiled emails to be saved to
```` cmd
> BootstrapEmail.Cli -p 'emails/*' -d 'emails/compiled/*'
````
#### compile for a string
```` cmd
> BootstrapEmail.Cli -s '<a href="#" class="btn btn-primary">Some Button</a>'
````
#### specify a config json file to use custom scss files
```` cmd
> BootstrapEmail.Cli -c bootstrap-email.json
````
