# ef-lazy-loading-benchmark

EF Core allocation benchmark comparing Entity Framework Core 7, 8, and 9 lazy loading performance.

## To run the consolidated benchmark

```bash
cd EfLazyLoadingBenchmark

# Run specific framework
dotnet run -c Release -f net7.0   # EF Core 7
dotnet run -c Release -f net8.0   # EF Core 8
dotnet run -c Release -f net9.0   # EF Core 9
dotnet run -c Release -f net10.0  # EF Core 10
```

## Results - Lazy Loading Entity Enumeration (1M entities)

### EF Core 7.0.20 (.NET 7.0.16)

```
BenchmarkDotNet v0.15.2, Linux Arch Linux
Genuine Intel 0000 5.70GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100-preview.5.25277.114
  [Host]     : .NET 7.0.16 (7.0.1624.6629), X64 RyuJIT AVX2
  Job-TGFUNO : .NET 7.0.16 (7.0.1624.6629), X64 RyuJIT AVX2

InvocationCount=3  UnrollFactor=3  WarmupCount=1

| Method    | Mean     | Error    | StdDev   | Allocated |
|---------- |---------:|---------:|---------:|----------:|
| Enumerate | 992.9 ms | 16.70 ms | 14.81 ms |   1.23 GB |
```

### EF Core 8.0.17 (.NET 8.0.17)

```
BenchmarkDotNet v0.15.2, Linux Arch Linux
Genuine Intel 0000 5.70GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100-preview.5.25277.114
  [Host]     : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2
  Job-TGFUNO : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2

InvocationCount=3  UnrollFactor=3  WarmupCount=1

| Method    | Mean    | Error    | StdDev   | Gen0      | Gen1     | Gen2     | Allocated |
|---------- |--------:|---------:|---------:|----------:|---------:|---------:|----------:|
| Enumerate | 1.340 s | 0.0182 s | 0.0162 s | 1000.0000 | 666.6667 | 333.3333 |   2.55 GB |
```

### EF Core 9.0.6 (.NET 9.0.6)

```
BenchmarkDotNet v0.15.2, Linux Arch Linux
Genuine Intel 0000 5.70GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100-preview.5.25277.114
  [Host]     : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  Job-TGFUNO : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

InvocationCount=3  UnrollFactor=3  WarmupCount=1

| Method    | Mean    | Error    | StdDev   | Gen0      | Gen1      | Gen2      | Allocated |
|---------- |--------:|---------:|---------:|----------:|----------:|----------:|----------:|
| Enumerate | 1.823 s | 0.0352 s | 0.0330 s | 8333.3333 | 4333.3333 | 1666.6667 |   2.66 GB |
```

### EF Core 10.0.0-preview (.NET 10.0.0)

```
BenchmarkDotNet v0.15.2, Linux Arch Linux
Genuine Intel 0000 5.70GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100-preview.5.25277.114
  [Host]     : .NET 10.0.0 (10.0.25.27814), X64 RyuJIT AVX2
  Job-TGFUNO : .NET 10.0.0 (10.0.25.27814), X64 RyuJIT AVX2

InvocationCount=3  UnrollFactor=3  WarmupCount=1

| Method    | Mean    | Error    | StdDev   | Gen0      | Gen1      | Gen2      | Allocated |
|---------- |--------:|---------:|---------:|----------:|----------:|----------:|----------:|
| Enumerate | 1.294 s | 0.0193 s | 0.0161 s | 9666.6667 | 5666.6667 | 3666.6667 |   1.27 GB |
```
