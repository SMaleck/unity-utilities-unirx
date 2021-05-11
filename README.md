# Unity Utilities - UniRx
A collection of general purpose extensions and utility classes for UniRx.

## Quick Start
> See the releases or the [CHANGELOG](./Packages/com.smaleck.utilities-unirx/CHANGELOG.md) for the current version.

> This package uses the [OpenUPM](https://openupm.com/) scoped registry.

To use this package in your Unity project, you have to manually add it to you `manifest.json`.

1. Go to `YourProject/Packages/`
2. Open `manifest.json`
3. Add the OpenUPM scoped registry
3. Add this packages git repository to the dependencies object in the JSON:

### Example:
```json
{
  "dependencies": {
    "com.smaleck.utilities-unirx": "git://github.com/SMaleck/unity-utilities-unirx.git?path=/Packages/com.smaleck.utilities-unirx#v1.0.0"
  }
}
```

### With Scoped Registry Dependencies
```json
{
  "dependencies": {
    "com.smaleck.utilities-unirx": "git://github.com/SMaleck/unity-utilities-unirx.git?path=/Packages/com.smaleck.utilities-unirx#v1.0.0"
  },
  "scopedRegistries": [
      {
        "name": "package.openupm.com",
        "url": "https://package.openupm.com",
        "scopes": [
          "com.neuecc.unirx",
          "com.openupm"
        ]
      }
    ]
  }
```