# USBException

Namespace: Nefarius.Drivers.WinUSB

Exception used by WinUSBNet to indicate errors. This is the
 main exception to catch when using the library.

```csharp
public sealed class USBException : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) → [USBException](./nefarius.drivers.winusb.usbexception.md)<br>
Implements [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)

## Properties

### <a id="properties-data"/>**Data**

```csharp
public IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### <a id="properties-helplink"/>**HelpLink**

```csharp
public string HelpLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-hresult"/>**HResult**

```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-innerexception"/>**InnerException**

```csharp
public Exception InnerException { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### <a id="properties-message"/>**Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-source"/>**Source**

```csharp
public string Source { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-stacktrace"/>**StackTrace**

```csharp
public string StackTrace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-targetsite"/>**TargetSite**

```csharp
public MethodBase TargetSite { get; }
```

#### Property Value

[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

## Constructors

### <a id="constructors-.ctor"/>**USBException(String)**

Constructs a new USBException with the given message

```csharp
public USBException(string message)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message describing the exception

### <a id="constructors-.ctor"/>**USBException(String, Exception)**

Constructs a new USBException with the given message and underlying exception
 that caused the USBException.

```csharp
public USBException(string message, Exception innerException)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message describing the exception

`innerException` [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
The underlying exception causing the USBException
