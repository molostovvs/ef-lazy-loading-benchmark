# ef-lazy-loading-benchmark
Contains a .NET benchmark for Entity Framework Core 8's lazy loading to evaluate memory footprint regression.
                                                        
## To run the benchmarks, use the following commands:

For Entity Framework 7:

`dotnet run --project ef7/ef7.csproj -c Release`

For Entity Framework 8:

`dotnet run --project ef8/ef8.csproj -c Release`

## Results with lazy-loading

BenchmarkDotNet v0.13.12, macOS Sonoma 14.3 (23D56) [Darwin 23.3.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.203
  [Host]     : .NET 8.0.3 (8.0.324.11423), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.3 (8.0.324.11423), Arm64 RyuJIT AdvSIMD

### Microsoft.EntityFrameworkCore 8.0.3

| Method    | Mean    | Error    | StdDev   | Gen0        | Gen1        | Gen2      | Allocated |
|---------- |--------:|---------:|---------:|------------:|------------:|----------:|----------:|
| Enumerate | 3.104 s | 0.0120 s | 0.0100 s | 281000.0000 | 104000.0000 | 2000.0000 |   1.81 GB |

### Microsoft.EntityFrameworkCore 7.0.17

| Method    | Mean    | Error    | StdDev   | Gen0        | Gen1       | Gen2      | Allocated |
|---------- |--------:|---------:|---------:|------------:|-----------:|----------:|----------:|
| Enumerate | 1.968 s | 0.0381 s | 0.0356 s | 168000.0000 | 46000.0000 | 1000.0000 |   1.16 GB |

## Results without lazy-loading

### Microsoft.EntityFrameworkCore 8.0.3

| Method    | Mean    | Error    | StdDev   | Gen0        | Gen1       | Gen2      | Allocated |
|---------- |--------:|---------:|---------:|------------:|-----------:|----------:|----------:|
| Enumerate | 1.182 s | 0.0093 s | 0.0087 s | 117000.0000 | 34000.0000 | 1000.0000 | 861.84 MB |

### Microsoft.EntityFrameworkCore 7.0.17

| Method    | Mean    | Error    | StdDev   | Gen0        | Gen1       | Gen2      | Allocated |
|---------- |--------:|---------:|---------:|------------:|-----------:|----------:|----------:|
| Enumerate | 1.266 s | 0.0102 s | 0.0096 s | 132000.0000 | 40000.0000 | 1000.0000 | 953.39 MB |
