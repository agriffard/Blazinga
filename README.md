# BlazingaEnumSelect

BlazingaEnumSelect is a Blazor component library for easy integration of enum values in `select` elements with localization support.

## Features

- Bind enum values to a select input.
- Support for localization using Display attributes.
- Customizable UI with `AdditionalAttributes`.

## Installation

You can install the library via NuGet after creating a release.

## Usage

```razor
<EnumSelect TValue="Status" Value="@currentStatus" class="form-select" />
```
