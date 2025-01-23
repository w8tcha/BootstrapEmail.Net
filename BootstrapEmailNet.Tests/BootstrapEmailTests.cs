

namespace BootstrapEmail.Net.Tests;

/// <summary>
/// Class BootstrapEmailTests.
/// </summary>
public class BootstrapEmailTests
{
    private readonly ITestOutputHelper testOutputHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="BootstrapEmailTests"/> class.
    /// </summary>
    /// <param name="testOutputHelper">The test output helper.</param>
    public BootstrapEmailTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// Parallel Test
    /// </summary>
    [Fact]
    public void ParallelTests()
    {
		var compiler = new BootstrapEmail();

		var tasks = Enumerable.Range(0, 50)
			.Select(_ => (Action)(() =>
			{
				var html = compiler.Compile(
					"""<a href="#" class="btn btn-primary">A button</a> <a href="#" class="btn btn-secondary">B button</a>""",
					string.Empty,
					InputType.String);

				const string expected = """
                        <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
                        <html>
                        	<head>
                        		<!-- Compiled with Bootstrap Email version: 1.5.3 -->
                        		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                        		<meta http-equiv="x-ua-compatible" content="ie=edge">
                        		<meta name="x-apple-disable-message-reformatting">
                        		<meta name="viewport" content="width=device-width, initial-scale=1">
                        		<meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                        		
                        		<style type="text/css">
                              body,table,td{font-family:Helvetica,Arial,sans-serif !important}.ExternalClass{width:100%}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:150%}a{text-decoration:none}*{color:inherit}a[x-apple-data-detectors],u+#body a,#MessageViewBody a{color:inherit;text-decoration:none;font-size:inherit;font-family:inherit;font-weight:inherit;line-height:inherit}img{-ms-interpolation-mode:bicubic}table:not([class^=s-]){font-family:Helvetica,Arial,sans-serif;mso-table-lspace:0pt;mso-table-rspace:0pt;border-spacing:0px;border-collapse:collapse}table:not([class^=s-]) td{border-spacing:0px;border-collapse:collapse}@media screen and (max-width: 600px){*[class*=s-lg-]>tbody>tr>td{font-size:0 !important;line-height:0 !important;height:0 !important}}
                        
                            
                        		</style>
                        	</head>
                        	<body style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;background-color: #ffffff;color: #000000" bgcolor="#ffffff">
                        		<table class="body" valign="top" role="presentation" bgcolor="#ffffff" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;background-color: #ffffff;color: #000000" border="0" cellpadding="0" cellspacing="0">
                        			<tbody>
                        				<tr>
                        					<td valign="top" style="line-height: 24px;font-size: 16px;margin: 0" align="left">
                        						<table class="btn btn-primary" role="presentation" style="border-radius: 6px;border-collapse: separate" border="0" cellpadding="0" cellspacing="0">
                        							<tbody>
                        								<tr>
                        									<td style="line-height: 24px;font-size: 16px;margin: 0;border-radius: 6px;" align="center" bgcolor="#0d6efd">
                        										<a href="#" style="font-size: 16px;font-family: Helvetica, Arial, sans-serif;text-decoration: none;border-radius: 6px;padding: 8px 12px;line-height: 20px;border: 1px solid transparent;display: block;font-weight: normal;white-space: nowrap;background-color: #0d6efd;color: #ffffff;border-color: #0d6efd">A button</a>
                        									</td>
                        								</tr>
                        							</tbody>
                        						</table>
                        						<table class="btn btn-secondary" role="presentation" style="border-radius: 6px;border-collapse: separate" border="0" cellpadding="0" cellspacing="0">
                        							<tbody>
                        								<tr>
                        									<td style="line-height: 24px;font-size: 16px;margin: 0;border-radius: 6px;" align="center" bgcolor="#718096">
                        										<a href="#" style="font-size: 16px;font-family: Helvetica, Arial, sans-serif;text-decoration: none;border-radius: 6px;padding: 8px 12px;line-height: 20px;border: 1px solid transparent;display: block;font-weight: normal;white-space: nowrap;background-color: #718096;color: #ffffff;border-color: #718096">B button</a>
                        									</td>
                        								</tr>
                        							</tbody>
                        						</table>
                        					</td>
                        				</tr>
                        			</tbody>
                        		</table>
                        	</body>
                        </html>
                        """;

                html.Should().BeEquivalentTo(expected, o => o.IgnoringNewlineStyle());
            }))
			.ToArray();
		Parallel.Invoke(tasks);
	}

	/// <summary>
	/// Compile html to String Test
	/// </summary>
	[Fact]
    public void ConvertToTextStringInput()
    {
        var config = new ConfigStore { plain_text = true };

        var html = new BootstrapEmail(config).Compile(
            """<a href="#" class="btn btn-primary">A button</a> <a href="#" class="btn btn-secondary">B button</a>""",
            string.Empty,
            InputType.String);

        const string expected = "A buttonB button";

        html.Should().Be(expected);
	}

	/// <summary>
	/// Compile html to String Test
	/// </summary>
	[Fact]
	public void TestStringInputWithCss()
	{
		var html = new BootstrapEmail(new ConfigStore { CssEmailPath = "SassEmail.css", CssHeadPath = "Head.css" }).Compile(
			"""<a href="#" class="btn btn-primary">A button</a><a href="#" class="btn btn-secondary">B button</a>""",
			string.Empty,
			InputType.String);

		const string expected = """
                                <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
                                <html>
                                	<head>
                                		<!-- Compiled with Bootstrap Email version: 1.5.3 -->
                                		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                                		<meta http-equiv="x-ua-compatible" content="ie=edge">
                                		<meta name="x-apple-disable-message-reformatting">
                                		<meta name="viewport" content="width=device-width, initial-scale=1">
                                		<meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                                		
                                		<style type="text/css">
                                      body,table,td{font-family:Helvetica,Arial,sans-serif !important}.ExternalClass{width:100%}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:150%}a{text-decoration:none}*{color:inherit}a[x-apple-data-detectors],u+#body a,#MessageViewBody a{color:inherit;text-decoration:none;font-size:inherit;font-family:inherit;font-weight:inherit;line-height:inherit}img{-ms-interpolation-mode:bicubic}table:not([class^=s-]){font-family:Helvetica,Arial,sans-serif;mso-table-lspace:0pt;mso-table-rspace:0pt;border-spacing:0px;border-collapse:collapse}table:not([class^=s-]) td{border-spacing:0px;border-collapse:collapse}@media screen and (max-width: 600px){*[class*=s-lg-]>tbody>tr>td{font-size:0 !important;line-height:0 !important;height:0 !important}}
                                
                                    
                                		</style>
                                	</head>
                                	<body style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;background-color: #ffffff;color: #000000" bgcolor="#ffffff">
                                		<table class="body" valign="top" role="presentation" bgcolor="#ffffff" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;background-color: #ffffff;color: #000000" border="0" cellpadding="0" cellspacing="0">
                                			<tbody>
                                				<tr>
                                					<td valign="top" style="line-height: 24px;font-size: 16px;margin: 0" align="left">
                                						<table class="btn btn-primary" role="presentation" style="border-radius: 6px;border-collapse: separate" border="0" cellpadding="0" cellspacing="0">
                                							<tbody>
                                								<tr>
                                									<td style="line-height: 24px;font-size: 16px;margin: 0;border-radius: 6px;" align="center" bgcolor="#0d6efd">
                                										<a href="#" style="font-size: 16px;font-family: Helvetica, Arial, sans-serif;text-decoration: none;border-radius: 6px;padding: 8px 12px;line-height: 20px;border: 1px solid transparent;display: block;font-weight: normal;white-space: nowrap;background-color: #0d6efd;color: #ffffff;border-color: #0d6efd">A button</a>
                                									</td>
                                								</tr>
                                							</tbody>
                                						</table>
                                						<table class="btn btn-secondary" role="presentation" style="border-radius: 6px;border-collapse: separate" border="0" cellpadding="0" cellspacing="0">
                                							<tbody>
                                								<tr>
                                									<td style="line-height: 24px;font-size: 16px;margin: 0;border-radius: 6px;" align="center" bgcolor="#718096">
                                										<a href="#" style="font-size: 16px;font-family: Helvetica, Arial, sans-serif;text-decoration: none;border-radius: 6px;padding: 8px 12px;line-height: 20px;border: 1px solid transparent;display: block;font-weight: normal;white-space: nowrap;background-color: #718096;color: #ffffff;border-color: #718096">B button</a>
                                									</td>
                                								</tr>
                                							</tbody>
                                						</table>
                                					</td>
                                				</tr>
                                			</tbody>
                                		</table>
                                	</body>
                                </html>
                                """;

        html.Should().BeEquivalentTo(expected, o => o.IgnoringNewlineStyle());
	}

	/// <summary>
	/// Compile html to String Test
	/// </summary>
	[Fact]
    public void TestStringInput()
    {
        var html = new BootstrapEmail().Compile(
            """<a href="#" class="btn btn-primary">A button</a><a href="#" class="btn btn-secondary">B button</a>""",
            string.Empty,
            InputType.String);

        const string expected = """
                                <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
                                <html>
                                	<head>
                                		<!-- Compiled with Bootstrap Email version: 1.5.3 -->
                                		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                                		<meta http-equiv="x-ua-compatible" content="ie=edge">
                                		<meta name="x-apple-disable-message-reformatting">
                                		<meta name="viewport" content="width=device-width, initial-scale=1">
                                		<meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                                		
                                		<style type="text/css">
                                      body,table,td{font-family:Helvetica,Arial,sans-serif !important}.ExternalClass{width:100%}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:150%}a{text-decoration:none}*{color:inherit}a[x-apple-data-detectors],u+#body a,#MessageViewBody a{color:inherit;text-decoration:none;font-size:inherit;font-family:inherit;font-weight:inherit;line-height:inherit}img{-ms-interpolation-mode:bicubic}table:not([class^=s-]){font-family:Helvetica,Arial,sans-serif;mso-table-lspace:0pt;mso-table-rspace:0pt;border-spacing:0px;border-collapse:collapse}table:not([class^=s-]) td{border-spacing:0px;border-collapse:collapse}@media screen and (max-width: 600px){*[class*=s-lg-]>tbody>tr>td{font-size:0 !important;line-height:0 !important;height:0 !important}}
                                
                                    
                                		</style>
                                	</head>
                                	<body style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;background-color: #ffffff;color: #000000" bgcolor="#ffffff">
                                		<table class="body" valign="top" role="presentation" bgcolor="#ffffff" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;background-color: #ffffff;color: #000000" border="0" cellpadding="0" cellspacing="0">
                                			<tbody>
                                				<tr>
                                					<td valign="top" style="line-height: 24px;font-size: 16px;margin: 0" align="left">
                                						<table class="btn btn-primary" role="presentation" style="border-radius: 6px;border-collapse: separate" border="0" cellpadding="0" cellspacing="0">
                                							<tbody>
                                								<tr>
                                									<td style="line-height: 24px;font-size: 16px;margin: 0;border-radius: 6px;" align="center" bgcolor="#0d6efd">
                                										<a href="#" style="font-size: 16px;font-family: Helvetica, Arial, sans-serif;text-decoration: none;border-radius: 6px;padding: 8px 12px;line-height: 20px;border: 1px solid transparent;display: block;font-weight: normal;white-space: nowrap;background-color: #0d6efd;color: #ffffff;border-color: #0d6efd">A button</a>
                                									</td>
                                								</tr>
                                							</tbody>
                                						</table>
                                						<table class="btn btn-secondary" role="presentation" style="border-radius: 6px;border-collapse: separate" border="0" cellpadding="0" cellspacing="0">
                                							<tbody>
                                								<tr>
                                									<td style="line-height: 24px;font-size: 16px;margin: 0;border-radius: 6px;" align="center" bgcolor="#718096">
                                										<a href="#" style="font-size: 16px;font-family: Helvetica, Arial, sans-serif;text-decoration: none;border-radius: 6px;padding: 8px 12px;line-height: 20px;border: 1px solid transparent;display: block;font-weight: normal;white-space: nowrap;background-color: #718096;color: #ffffff;border-color: #718096">B button</a>
                                									</td>
                                								</tr>
                                							</tbody>
                                						</table>
                                					</td>
                                				</tr>
                                			</tbody>
                                		</table>
                                	</body>
                                </html>
                                """;

        html.Should().BeEquivalentTo(expected, o => o.IgnoringNewlineStyle());
    }

    /// <summary>
    /// Run all tests
    /// </summary>
    [Fact]
    public void TestFileInput()
    {
        this.testOutputHelper.WriteLine("🧪 Starting tests...");
        var startTime = DateTime.Now;

        var path = Path.GetFullPath("tests/input", Directory.GetCurrentDirectory());

        var files = Directory.GetFiles(path, "*.html", SearchOption.AllDirectories);

        var bsEmail = new BootstrapEmail(new ConfigStore());

        bsEmail.ClearSassCache();

        foreach (var file in files)
        {
            this.testOutputHelper.WriteLine("🧪 Start test...");

            var startFileTime = DateTime.Now;
            var fileContents = File.ReadAllText(file);
            var destination = file.Replace(@"tests\input\", @"tests\output\");
            var convertedHtml = bsEmail.Compile(fileContents, destination, InputType.String);

            var expectedHtml = File.ReadAllText(destination);

            this.testOutputHelper.WriteLine($"🚀 Built {destination} (in {(DateTime.Now - startFileTime).TotalSeconds:0.00}s)");

            //File.WriteAllText(destination, convertedHtml);

            convertedHtml.Should().BeEquivalentTo(expectedHtml, o => o.IgnoringNewlineStyle());
        }

        this.testOutputHelper.WriteLine($"Finished compiling tests in {(DateTime.Now - startTime).TotalSeconds:0.00}s 🎉");
    }
}