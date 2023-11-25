BootstrapEmail.NET

[![logo](https://github.com/w8tcha/BootstrapEmail.Net/blob/main/bootstrap-email.png)

If you know Bootstrap, you know Bootstrap Email.

[![NuGet](https://img.shields.io/nuget/v/BootstrapEmail.Net.svg)](https://nuget.org/packages/BootstrapEmail.Net)

[![build dotnet](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml/badge.svg)](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml)

This is the .NET Version of  [Bootstrap Email](https://github.com/bootstrap-email/bootstrap-email) which was converted from Ruby to .NET (Core) 7/8

Bootstrap Email takes most of its inspiration from these two wonderful frameworks, [Bootstrap](https://getbootstrap.com) and [Tailwind](https://tailwindcss.com) but for HTML emails. Working with HTML in emails is never easy because of the nuances of email vs the web. With Bootstrap Email you don't have to understand all the nuance and it allows you to write emails like you would a website.

## Setup
There are a few different ways you can use Bootstrap Email to compile emails:

### Use the dll

#### compile all files ending in .html in the current directory

```c#
var config = new ConfigStore();

var bsEmail = new BootstrapEmail(config);

bsEmail.Compile(string.Empty, string.Empty, InputType.File);
```
#### compile the file email.html and save it to the file out.html

```c#
var config = new ConfigStore();

var bsEmail = new BootstrapEmail(config);

bsEmail.Compile("email.html", "out.html", InputType.File);
```
#### specify a path pattern and a destination directory for compiled emails to be saved to

```c#
var config = new ConfigStore();

var bsEmail = new BootstrapEmail(config);

bsEmail.Compile("emails/*", "mails/compiled/", InputType.Pattern);
```

#### compile for a string

```c#
var config = new ConfigStore();

var bsEmail = new BootstrapEmail(config);

bsEmail.Compile("<a href='#' class='btn btn-primary'>Some Button</a>", string.Empty, InputType.String);
```

### Via the command line with the BootstrapEmailNet.Cli:

#### compile all files ending in .html in the current directory
````
BootstrapEmail.Cli
````
#### compile the file email.html and save it to the file out.html
````
BootstrapEmail.Cli -f email.html -d out.html
````
#### specify a path pattern and a destination directory for compiled emails to be saved to
````
BootstrapEmail.Cli -p 'emails/*' -d 'emails/compiled/*'
````
#### compile for a string
````
BootstrapEmail.Cli -s '<a href="#" class="btn btn-primary">Some Button</a>'
````
#### specify a config json file to use custom scss files
````
BootstrapEmail.Cli -c bootstrap-email.json
````
