# C\# & The REST API

Working a REST api in C# can be as easy as working with any other
collection or local data source however the setting up of an API connector can be a bit of pain originally.

When using a REST API, you can send a GET, POST, PUT or DELETE request, allowing you manipulate the API in many ways. 
The first two will be the most prominent and once you learn how to
get and post the rest is self explanatory.

The data also comes in JSON form usually, so we will be using
Json.NET for serializing and deserializing the JSON data as an object.

## How to GET 

1. You will need to import 3 libraries to work with the API. One
library being used to download the data, one will allow
for the API to be queried without lagging up the app
the last being used to turn the JSON into a C# Object. Add the following up the top of your C# file:

```csharp
using System.Net.Http; // HTTP Client for querying API
using System.Threading.Tasks; // Used for async programming
using Newtonsoft.Json; // JSON Serialization
```

2. All requests using HTTPClient need to be *asynchronous*. This 
means that the requests are made on a concurrent thread from 
that which the main code runs on. C# has great support for this
using the Async-Await to make concurrent programming second nature.
It would be worth learning this in depth for the app in general
but for now we're going to just use the basics of it. To allow for asynchronous programming we can use an *async void or Task*.
A Task will return a value but must be awaited, unlike a void which can be implemented normally:

```csharp
public async void DownloadJsonVoidAsync()
{
    // Code will go here
}

public async Task<string> GetJsonAsync()
{
    string result = await ...; // Run async code here
    /// Code will go here
    return result;
}
```
- Note that async methods end in the async keyword

3. Once you have set up your procedure/function, you can implement the retrieval code. The method is super simple, `using HttpClient` and a method such as `HttpClient.GetStringAsync`. GetStringAsync takes in one parameter, which is the URL being queried Note that when using an async method you must use the `await` keyword to tell C# that this method must run on a seperate thread:

```csharp
public async void DownloadJsonVoidAsync()
{
    // This is the same everytime
    using (HttpClient client = new HttpClient())
    {
        string downloadedJson = await client.GetStringAsync("http://ajsonapi.net/api/thing");
    }
}

//  This can be ran anywhere, and is called as normal:
DownloadJsonVoidAsync();

public async Task<string> GetJsonAsync()
{
    // This is the same everytime
    using (HttpClient client = new HttpClient())
    {
        return await client.GetStringAsync("http://ajsonapi.net/api/thing");
    }
}
//  This will need to be used in an async method and is used in a similar way:

string result = await GetJsonAsync();
```

- Some queries may require you to add headers for the authentication and such, this is easy: just add this before client.GetWhatever:
```csharp
client.DefaultRequestHeaders.Add("header-name", "header-value");
```

4. To treat this data like a C# object is a bit harder and so requires a little bit of extra effort. Say we had a payload like this:

```json
[
    {
        "name": "Alex",
        "age": 19
    },
    {
        "name" : "Ethan",
        "age": 420
    }
]
```

This data is a simple JSON array of 2 identical objects.
In order to use this in C# we need to model the data and
and then serialize it. Modelling the data is as easy as making a class, assuming we'll call the object
a "Person" object it would look like this:

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```
This model will automatically map JSON data to a C# object,
it's not case sensitive and to get an array of this object
we just need to use a `List<Person>` type (make sure you import `System.Collections.Generic`). Using NewtonSoft.Json, we can convert this JSON string into an object like so (assuming the above data is called `peopleString`):

```csharp
List<People> PeopleList = JsonConvert.DeserializeObject<List<People>>(peopleString);
```

The method `DeserializeObject` requires the method to be included in a set of <> brackets as it is generic but JSON.Net will convert any json string (or at least attempt to).

Note that if you want your life to be easier you can do this:

```csharp
var PeopleList = JsonConvert.DeserializeObject<List<People>>(peopleString);
```

Once the list has been deserialized, the list can treated like any other C# list:

```csharp
Console.WriteLine(PeopleList[0].Name); // Alex
Console.WriteLine(PeopleList[0].Age); // 18
Console.WriteLine(PeopleList[1].Age); // 420
```

And if you want to turn the list back into a string you need to `SerializeObject`

```csharp
string peopleJSON = JsonConvert.SerializeObject(PeopleList);
```

So for example, if we we wanted to rewrite that API Task before to return a list of objects it would look like so:

```csharp
public async Task<List<APIObject>> GetJsonAsync()
{
    // This is the same everytime
    using (HttpClient client = new HttpClient())
    {
        string resultString = await client.GetStringAsync("http://ajsonapi.net/api/thing");
        return JsonConvert.DeserializeObject<List<APIObject>>(resultString);
    }
}
```

And that's it, that's all the foundations of sending GET requests to an API using C#.


## How to POST 

**Coming later** 
