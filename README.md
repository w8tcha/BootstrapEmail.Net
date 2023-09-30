<p align="center">
  <a href="https://bootstrapemail.com">
    <img src="https://bootstrapemail.com/img/icons/logo.png" alt="" width=72 height=72>
  </a>

  <h3 align="center">BootstrapEmail.NET</h3>

  <p align="center">
    If you know Bootstrap, you know Bootstrap Email.
    <br>
    <a href="https://v1.bootstrapemail.com/docs/introduction"><strong>Explore Bootstrap Email docs »</strong></a>
  </p>
</p>

<br>

This is the .NET Version of  [Bootstrap Email]([https://bootstrapemail.com/docs/usage#command-line](https://github.com/bootstrap-email/bootstrap-email)) which was converted from Ruby to .NET (Core) 7

Bootstrap Email takes most of its inspiration from these two wonderful frameworks, [Bootstrap](https://getbootstrap.com) and [Tailwind](https://tailwindcss.com) but for HTML emails. Working with HTML in emails is never easy because of the nuances of email vs the web. With Bootstrap Email you don't have to understand all the nuance and it allows you to write emails like you would a website.

## Setup
There are a few different ways you can use Bootstrap Email to compile emails:
- Use the dll
- Via the command line with the BootstrapEmailNet.Cli:

### compile all files ending in .html in the current directory
````
BootstrapEmail.Cli
````
### compile the file email.html and save it to the file out.html
````
BootstrapEmail.Cli -f email.html -d out.html
````
### specify a path pattern and a destination directory for compiled emails to be saved to
````
BootstrapEmail.Cli -p 'emails/*' -d 'emails/compiled/*'
````
### compile for a string
````
BootstrapEmail.Cli -s '<a href="#" class="btn btn-primary">Some Button</a>'
````
### specify a config json file to use custom scss files
````
cat input.html | bootstrap-email
### specify config path to use to customize things like colors
````
bootstrap-email -c bootstrap-email.json
````
