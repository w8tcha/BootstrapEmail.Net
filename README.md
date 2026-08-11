# BootstrapEmail.NET

![logo](https://raw.githubusercontent.com/w8tcha/BootstrapEmail.Net/main/bootstrap-email.png)

If you know Bootstrap, you know Bootstrap Email.

[![NuGet](https://img.shields.io/nuget/v/BootstrapEmail.Net.svg)](https://nuget.org/packages/BootstrapEmail.Net)

[![build dotnet](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml/badge.svg)](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml)

This is the .NET Version of  [Bootstrap Email](https://github.com/bootstrap-email/bootstrap-email) which was converted from Ruby to .NET (Core) 9/10

Bootstrap Email takes most of its inspiration from these two wonderful frameworks, [Bootstrap](https://getbootstrap.com) and [Tailwind](https://tailwindcss.com) but for HTML emails. Working with HTML in emails is never easy because of the nuances of email vs the web. With Bootstrap Email you don't have to understand all the nuance and it allows you to write emails like you would a website.

## Runtime

> [!CAUTION]
> By default package contains only the windows x64 Dart Sass runtime. 
> If you have different system then it can use the installed Dart Sass in your system, or you can install one of the following nuget packages:
> * DartSass.Native.win-x64
> * DartSass.Native.win-x86
> * DartSass.Native.linux-x64
> * DartSass.Native.linux-arm64
> * DartSass.Native.linux-arm
> * DartSass.Native.linux-x86
> * DartSass.Native.linux-musl-x64
> * DartSass.Native.linux-musl-arm64
> * DartSass.Native.linux-musl-arm
> * DartSass.Native.linux-musl-x86
> * DartSass.Native.macos-x64
> * DartSass.Native.macos-arm64
> * DartSass.Native.android-x64
> * DartSass.Native.android-arm64
> * DartSass.Native.android-arm
> * DartSass.Native.android-x86

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

### Via the command line

[![NuGet](https://img.shields.io/nuget/v/BootstrapEmail.Net.Cli.svg)](https://nuget.org/packages/BootstrapEmail.Net.Cli)

The CLI is published as a [.NET tool](https://learn.microsoft.com/dotnet/core/tools/global-tools) named `BootstrapEmail.Net.Cli`, and installs the `bootstrap-email` command.

#### install globally

```` cmd
> dotnet tool install --global BootstrapEmail.Net.Cli
````

#### update to the latest version

```` cmd
> dotnet tool update --global BootstrapEmail.Net.Cli
````

#### options

| Option | Long form | Description |
| --- | --- | --- |
| `-h` | `--help` | Set output to verbose messages. |
| `-c` | `--config` | Relative path to JSON config file to customize Bootstrap Email. |
| `-t` | `--text` | Return the plain text version of the email. |
| `-s` | `--string` | HTML string to be compiled rather than a file. |
| `-f` | `--file` | File to be compiled. |
| `-p` | `--pattern` | Specify a pattern of files to compile and save multiple files at once (used with `--destination`). |
| `-d` | `--destination` | Destination for compiled files (used with the `--pattern` option). |
| `-v` | `--version` | Show version. |

#### compile all files ending in .html in the current directory
```` cmd
> bootstrap-email
````
#### compile the file email.html and save it to the file out.html
```` cmd
> bootstrap-email -f email.html -d out.html
````
#### specify a path pattern and a destination directory for compiled emails to be saved to
```` cmd
> bootstrap-email -p 'emails/*' -d 'emails/compiled/*'
````
#### compile for a string
```` cmd
> bootstrap-email -s '<a href="#" class="btn btn-primary">Some Button</a>'
````
#### specify a config json file to use custom scss files
```` cmd
> bootstrap-email -c bootstrap-email.json
````
#### pipe a file through stdin
```` cmd
> cat input.html | bootstrap-email
````
