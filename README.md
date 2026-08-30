[![](https://img.shields.io/nuget/v/soenneker.extensions.spans.readonly.parameterinfos.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.spans.readonly.parameterinfos/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.spans.readonly.parameterinfos/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.spans.readonly.parameterinfos/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.spans.readonly.parameterinfos.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.spans.readonly.parameterinfos/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.spans.readonly.parameterinfos/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.spans.readonly.parameterinfos/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Spans.Readonly.ParameterInfos
Extract parameter types from a `ReadOnlySpan<ParameterInfo>` in declaration order.

## Installation

```bash
dotnet add package Soenneker.Extensions.Spans.Readonly.ParameterInfos
```

## Create a type array

```csharp
using Soenneker.Extensions.Spans.Readonly.ParameterInfos;
using System;
using System.Reflection;

MethodInfo method = typeof(string).GetMethod(nameof(string.StartsWith), [typeof(string)])!;
ReadOnlySpan<ParameterInfo> parameters = method.GetParameters();

Type[] parameterTypes = parameters.ToTypes();
```

This is useful when adapting reflection metadata to APIs that accept a type array. `ToTypes()` allocates the returned array and returns an empty array for an empty span.

## Reuse a destination

```csharp
Type[] reusableBuffer = new Type[parameters.Length];
parameters.FillTypes(reusableBuffer);
```

`FillTypes()` performs no allocation of its own. The destination must contain at least one slot per input parameter; extra slots remain unchanged. The resulting values are the reflection API's exact `ParameterType` values, including by-reference, pointer, array, and constructed generic types.
