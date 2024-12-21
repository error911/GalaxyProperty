# GalaxyProperties
Provides filling of the object with any properties. They can be serialized.

## Requirements
Sirenix Odin Inspector

### Install from Unity Package Manager.
You can add `https://github.com/error911/GalaxyMessages.git?path=Assets/Plugins/GalaxyProperty` to Package Manager

![image](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

Usage: Get property
---
```csharp
[SerializeField] private PropertyMap _propertyMap;

[Button("Test class")]
private void Test()
{
    if (_propertyMap.TryGetProperty("Settings", out var prop))
    {
        Debug.Log($"Prop1> {prop.ToString()}");
    }

    if (_propertyMap.TryGetPropertyValue<float>("Settings", out var prop2))
    {
        Debug.Log($"Prop2> {prop2.ToString()}");
    }
}

```


remember to use: `namespace Properties`
