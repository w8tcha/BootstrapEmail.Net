using BenchmarkDotNet.Running;

using BootstrapEmail.Net.Benchmarks;

BenchmarkRunner.Run<LibraryComparisonBenchmarks>();

// Run dotnet run -c Release
