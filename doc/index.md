---
title: Home
---

# PowerDNS Client for .NET

This project provides a .NET client library for the [PowerDNS](https://www.powerdns.com/) API.

## Usage

Add the NuGet package `PowerDns.Client` to your project. You can then create an instance of the client like this:

```csharp
var client = new PowerDnsClient(
    uri: new Uri("http://example.com/"), // without /api/v1
    apiKey: "changeme");
var zonesEndpoint = client.Servers["localhost"].Zones;
```

Get a list of all zones:

```csharp
List<Zone> zones = await zonesEndpoint.ReadAllAsync();
```

Get a specific zone:

```csharp
Zone zone = await zonesEndpoint["example.org"].ReadAsync();
```

Create a new zone:

```csharp
await zonesEndpoint.CreateAsync(new Zone("example.org", /*nameservers:*/ "ns1.example.org", "ns2.example.org")
{
    RecordSets =
    {
        new RecordSet
        {
            Name = "www.example.org",
            Type = RecordType.A,
            Ttl = 10,
            Records = 
            {
                new Record(content: "1.2.3.4"),
                new Record(content: "5.6.7.8")
            }
        }
    }
});
```

Patch a record set in a zone:

```csharp
RecordSet recordSet = await zonesEndpoint["example.org"].GetRecordSetAsync("www.example.org");

recordSet.ChangeType = ChangeType.Replace;
recordSet.Records.Add(new ResourceRecord(content: "4.3.2.1"));

await zonesEndpoint["example.org"].PatchRecordSetAsync(recordSet);
```

Delete a zone:

```csharp
await zonesEndpoint["example.org"].DeleteAsync();
```
