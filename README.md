# BootstrapEmail.NET

![logo](https://raw.githubusercontent.com/w8tcha/BootstrapEmail.Net/main/bootstrap-email.png)

If you know Bootstrap, you know Bootstrap Email.

[![NuGet](https://img.shields.io/nuget/v/BootstrapEmail.Net.svg)](https://nuget.org/packages/BootstrapEmail.Net)
[![build dotnet](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml/badge.svg)](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml)
[![Coverage](https://github.com/w8tcha/BootstrapEmail.Net/raw/badges/badge_linecoverage.svg)](https://github.com/w8tcha/BootstrapEmail.Net/actions/workflows/build.yml)

BootstrapEmail.NET is the .NET version of [Bootstrap Email](https://github.com/bootstrap-email/bootstrap-email), converted from Ruby. Write emails with the Bootstrap classes you already know, and it compiles them into the table-based HTML with inline styles that email clients need.

For example, this button:

```html
<a href="https://example.com" class="btn btn-primary">Confirm</a>
```

compiles to this (some styles shortened):

```html
<table class="btn btn-primary" role="presentation" border="0" cellpadding="0" cellspacing="0" style="border-radius: 6px; ...">
  <tbody>
    <tr>
      <td align="center" bgcolor="#0d6efd" style="border-radius: 6px; ...">
        <a href="https://example.com" style="background-color: #0D6EFD; color: #FFFFFF; ...">Confirm</a>
      </td>
    </tr>
  </tbody>
</table>
```

See the [Bootstrap Email documentation](https://bootstrapemail.com/docs/introduction) for the available components and utilities.

## Contents

- [Requirements](#requirements)
- [Use the library](#use-the-library)
- [Use the command line](#use-the-command-line)
- [Configuration](#configuration)
- [Custom styles](#custom-styles)
- [Files created at runtime](#files-created-at-runtime)
- [Upgrading to 2.0](#upgrading-to-20)
- [Development](#development)
- [License](#license)

## Requirements

- .NET 10 or .NET 11
- Any operating system supported by .NET. No Dart Sass binary or other native runtime is needed.

## Use the library

Install the package:

```shell
dotnet add package BootstrapEmail.Net
```

Compile a string and get the HTML back:

```csharp
using BootstrapEmail.Net;

var bsEmail = new BootstrapEmailCompiler();

var html = bsEmail.Compile(
    "<a href='#' class='btn btn-primary'>Some Button</a>",
    string.Empty,
    InputType.String);
```

The other input types write to files and return an empty string:

```csharp
// Compile email.html and save it to out.html
bsEmail.Compile("email.html", "out.html", InputType.File);

// Compile email.html and save it to compiled/email.html
bsEmail.Compile("email.html", string.Empty, InputType.File);

// Compile all .html files in the current directory and save them to compiled/
bsEmail.Compile(string.Empty, string.Empty, InputType.File);

// Compile the files matching a pattern and save them to a directory
bsEmail.Compile("emails/*", "emails/compiled", InputType.Pattern);
```

To change the defaults, pass a `ConfigStore`:

```csharp
var bsEmail = new BootstrapEmailCompiler(new ConfigStore { CompileOnlyBody = true });
```

## Use the command line

[![NuGet](https://img.shields.io/nuget/v/BootstrapEmail.Net.Cli.svg)](https://nuget.org/packages/BootstrapEmail.Net.Cli)

The CLI is a [.NET tool](https://learn.microsoft.com/dotnet/core/tools/global-tools) that installs the `bootstrap-email` command:

```shell
dotnet tool install --global BootstrapEmail.Net.Cli
```

To update it, run `dotnet tool update --global BootstrapEmail.Net.Cli`.

| Option | Long form | Description |
| --- | --- | --- |
| `-f` | `--file` | File to compile. Saved to `compiled/` next to the file, unless `--destination` is set. |
| `-s` | `--string` | HTML string to compile. The result is written to the console. |
| `-p` | `--pattern` | Pattern of files to compile, relative to the current directory. Requires `--destination`. |
| `-d` | `--destination` | Output file for `--file`, or output directory for `--pattern`. |
| `-t` | `--text` | Return the plain text version of the email. |
| `-c` | `--config` | Path to a JSON [configuration](#configuration) file. |
| `-v` | `--version` | Show the version. |
| `-h` | `--help` | Show the help. |

Without any input option, it compiles the HTML piped to it and writes the result to the console. If nothing is piped, it compiles all `.html` files in the current directory and saves them to `compiled/`:

```shell
cat input.html | bootstrap-email
bootstrap-email
```

Compile one file:

```shell
bootstrap-email -f email.html -d out.html
```

Compile the files matching a pattern. Quote the pattern so that your shell doesn't expand it:

```shell
bootstrap-email -p 'emails/*' -d emails/compiled
```

Compile a string:

```shell
bootstrap-email -s '<a href="#" class="btn btn-primary">Some Button</a>'
```

Save the plain text version of an email:

```shell
bootstrap-email -t -f email.html -d email.txt
```

Use a configuration file:

```shell
bootstrap-email -c bootstrap-email.json -f email.html
```

## Configuration

The library takes these settings as `ConfigStore` properties, and the CLI reads them from a JSON file with the same names:

```json
{
  "sass_email_location": "styles",
  "sass_email_string": "my-email.scss",
  "sass_log_enabled": true
}
```

| Setting | Default | Description |
| --- | --- | --- |
| `sass_email_string` | `bootstrap-email.scss` | File name of the Sass file for the inlined styles. |
| `sass_email_location` | | Directory of that file. Defaults to `sass_location`. |
| `sass_head_string` | `bootstrap-head.scss` | File name of the Sass file for the styles in the `<head>`. |
| `sass_head_location` | | Directory of that file. Defaults to `sass_location`. |
| `sass_location` | `core/` | Directory of the core Sass files, templates and layout, relative to the current directory. |
| `sass_cache_location` | | Directory of the Sass cache. Defaults to `.sass-cache/<name of the Sass file>` in the current directory. |
| `sass_log_enabled` | `false` | Log to the console when Sass gets compiled. |
| `layout_file` | `layout.html.erb` | Layout file in `sass_location`. It's applied unless the input already has a `<head>` with content. |
| `CssEmailPath` | | CSS file to use instead of the compiled inlined styles, relative to the current directory. |
| `CssHeadPath` | | CSS file to use instead of the compiled `<head>` styles, relative to the current directory. |
| `CompileOnlyBody` | `false` | Return only the content of the `<body>`. |
| `plain_text` | `false` | Return a plain text version of the email. Output files keep their name unless you set a destination. |

## Custom styles

The default stylesheets are included precompiled, so compiling an email normally doesn't run Sass at all. There are two ways to customize them:

- **Use your own Sass file.** Copy `core/bootstrap-email.scss` (see [Files created at runtime](#files-created-at-runtime)), add your styles, and point `sass_email_location` and `sass_email_string` at the copy. You can also edit the files in `core/` directly.
- **Use your own CSS file.** Set `CssEmailPath` or `CssHeadPath`. The file replaces the Bootstrap Email styles instead of adding to them, and no Sass is compiled.

Custom Sass is compiled in managed code with [DartSassHost](https://github.com/Taritsyn/DartSassHost) and [Jint](https://github.com/sebastienros/jint). The first compile takes several seconds, and the result is cached until the Sass files change. To clear the cache, call `ClearSassCache()`.

## Files created at runtime

Bootstrap Email creates these in the current directory, which therefore has to be writable:

| Path | Content |
| --- | --- |
| `core/` | The Sass files, templates and layout, unpacked on the first compile if the directory doesn't exist. |
| `.sass-cache/` | Compiled custom Sass. |
| `compiled/` | Output files, if no destination is set. |

> [!IMPORTANT]
> An existing `core/` directory is never updated. Delete it after upgrading so that the new version can unpack its files, and keep a copy of any changes you made in it.

## Upgrading to 2.0

- Rename `BootstrapEmail` to `BootstrapEmailCompiler`.
- Remove any `DartSass.Native.*` package references. Sass is now compiled in managed code, so the Dart Sass runtime is no longer needed.
- Remove `ConfigStore.DartSassNativeType`. There is no replacement, because no runtime has to be selected.
- `StyleType` is now `BootstrapEmail.Net.StyleType`, instead of the type from `Citizen17.DartSass`.
- The CLI option `--text` no longer takes a value.
- `plain_text` now also applies to file and pattern input, not just strings.
- Pattern input now compiles the content of the matching files. Earlier versions compiled the file path by mistake.

## Development

```shell
dotnet test BootstrapEmail.Net.slnx
```

The precompiled CSS in `BootstrapEmailNet/precompiled/` has to match the core Sass files, and a test fails if it doesn't. After changing the Sass files or updating DartSassHost, regenerate the CSS and rebuild:

```shell
BOOTSTRAP_EMAIL_UPDATE_PRECOMPILED=1 dotnet test BootstrapEmailNet.Tests/BootstrapEmail.Net.Tests.csproj -f net10.0 --filter "FullyQualifiedName~PrecompiledCssMatchesCoreSass"
```

In PowerShell, set the variable first with `$env:BOOTSTRAP_EMAIL_UPDATE_PRECOMPILED = 1`.

## License

[MIT](https://github.com/w8tcha/BootstrapEmail.Net/blob/main/LICENSE.MD)
