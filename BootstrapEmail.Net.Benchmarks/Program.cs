using System.Reflection;

using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

// Run dotnet run -c Release -- --job short --filter *LibraryComparisonBenchmarks*
