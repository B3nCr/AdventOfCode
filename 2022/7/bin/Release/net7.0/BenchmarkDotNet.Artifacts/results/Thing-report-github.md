``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.6.1 (21G217) [Darwin 21.6.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


```
|     Method |     Mean |   Error |  StdDev |
|----------- |---------:|--------:|--------:|
| RunProgram | 316.2 μs | 3.99 μs | 3.54 μs |
