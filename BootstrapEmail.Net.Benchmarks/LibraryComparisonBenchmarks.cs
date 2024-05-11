namespace BootstrapEmail.Net.Benchmarks;

using System.Collections.Generic;

using BenchmarkDotNet.Attributes;

/// <summary>
/// Class LibraryComparisonBenchmarks.
/// </summary>
[ShortRunJob]
[MemoryDiagnoser]
public class LibraryComparisonBenchmarks
{
    /// <summary>
    /// Class TestData.
    /// </summary>
    public record TestData(string Label, string Html)
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => this.Label;
    }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>The data.</value>
    [ParamsSource(nameof(GetTestTemplates))]
    public TestData Data { get; set; }

    /// <summary>
    /// Gets the test templates.
    /// </summary>
    /// <returns>System.Collections.Generic.IEnumerable&lt;BootstrapEmail.Net.Benchmarks.LibraryComparisonBenchmarks.TestData&gt;.</returns>
    public static IEnumerable<TestData> GetTestTemplates()
    {
        yield return new TestData("basic", """
                                            <div id="test1">{{ test1_token }}</div>
                                            <a id="test2" href="{{ test2_token }}">Some Token Link</a>
                                            <a id="test3" href="https://google.com/some+url{{">Some Link with regular url to be encoded</a>
                                            <img id="test4" src="{{ test4_image_src }}" />

                                            """);

        yield return new TestData("alert", """
                                           <div class="alert alert-primary"><strong>Well done!</strong> You successfully read this important alert message.<hr>wow amazing</div>
                                           <div class="alert alert-secondary"><strong>Well done!</strong> You successfully read this important alert message.</div>
                                           <div class="alert alert-info"><strong>Well done!</strong> You successfully read this important alert message.</div>
                                           <div class="alert alert-success"><strong>Well done!</strong> You successfully read this important alert message.</div>
                                           <div class="alert alert-warning"><strong>Well done!</strong> You successfully read this important alert message.</div>
                                           <div class="alert alert-danger"><strong>Well done!</strong> You successfully read this important alert message.</div>
                                           <div class="alert alert-light"><strong>Well done!</strong> You successfully read this important alert message.</div>
                                           <div class="alert alert-dark"><strong>Well done!</strong> You successfully read this important alert message.</div>

                                           """);

        yield return new TestData("border-radius", """
                                           <a class="btn btn-primary rounded-none" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-sm" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-top rounded-none" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-right rounded-none" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-bottom rounded-none" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-left rounded-none" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-lg" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-xl" href="https://example.com">Click Me</a>
                                           <a class="btn btn-primary rounded-circle" href="https://example.com">Click Me</a>
                                           <a class="btn bg-black text-white rounded-pill" href="https://example.com">Click Me</a>
                                           
                                           <div class="rounded-xl bg-red-400">Cool Stuff</div>

                                           """);

        yield return new TestData("images", """
                                            <img class="img-fluid" src="https://images.unsplash.com/photo-1615621037604-e03be72c94de?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=2250&q=80" alt="Some Image" />

                                            <a href="https://bootstrapemail.com">
                                              <img class="w-48" src="https://images.unsplash.com/photo-1615621037604-e03be72c94de?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=2250&q=80">
                                            </a>

                                            <img class="w-48 rounded-3xl" src="https://images.unsplash.com/photo-1615621037604-e03be72c94de?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=2250&q=80">

                                            """);
        yield return new TestData("integration", """
                                                 <preview>This is the preview for this email!</preview>
                                                 <div class="container">
                                                     <h1 class="text-center">Acme Company</h1>
                                                     <h4 class="text-center text-muted">We are happy to have you</h4>
                                                 
                                                     <hr>
                                                 
                                                     <p>We recently started shipping our first round of our product to all of our backers and we could not be more excited.</p>
                                                     <p>When you get the first package make sure to open it and tell us what you think. I think you will be pleasantly surprised with the quality and care that went in to making each and every one of these for you.</p>
                                                     <div class="alert alert-success">Bonus giveaways are still going on through the end of the month!</div>
                                                 </div>

                                                 <div class="container bg-light p-3">
                                                     <div class="row">
                                                         <div class="col-lg-6">
                                                             <div class="card">
                                                                 <img class="img-fluid" width="280" height="200" src="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Cat03.jpg/1280px-Cat03.jpg" />
                                                                 <div class="card-body">
                                                                     <a class="btn btn-danger ax-left" href="https://example.com">Cats</a>
                                                                 </div>
                                                             </div>
                                                         </div>
                                                         <div class="col-lg-6">
                                                             <div class="card">
                                                                 <img class="img-fluid" width="280" height="220" src="https://static.wixstatic.com/media/38caab_553760a4c5474bb7a517357fbafd3e48.jpg" />
                                                                 <div class="card-body">
                                                                     <a class="btn btn-warning ax-right" href="https://example.com">Dogs</a>
                                                                 </div>
                                                             </div>
                                                         </div>
                                                     </div>
                                                 </div>

                                                 <div class="container">
                                                     <div class="ax-center m-3">
                                                         <a class="btn btn-primary btn-lg p-3 mr-2" href="https://example.com">Buy Now</a>
                                                         <a class="btn btn-secondary btn-lg p-3 ml-2" href="https://example.com">Share with a Friend</a>
                                                     </div>
                                                 </div>

                                                 <div class="container bg-dark">
                                                     <div class="text-light text-center m-4 w-full">
                                                         &copy;2017 Cool Company
                                                     </div>
                                                 </div>
                                                 """);
    }

    private static readonly BootstrapEmail BsEmail = new();

	/// <summary>
	/// Run Benchmark with BootstrapEmail.Net
	/// </summary>
	[Benchmark(Baseline = true, Description = "BootstrapEmail.Net")]
    public string BootstrapEmailNet()
    {
        return BsEmail.Compile(this.Data.Html, string.Empty, InputType.String);
    }

	/// <summary>
	/// Run Benchmark with UnDotNet BootstrapEmail
	/// </summary>
	[Benchmark(Description = "UnDotNet.BootstrapEmail")]
    public string UnDotNetBootstrapEmail()
    {
        var compiler = new UnDotNet.BootstrapEmail.BootstrapCompiler(this.Data.Html);
        return compiler.Html();
    }
}
