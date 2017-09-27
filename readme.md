# Axoom.Provisioning.PowerDns

This library provides a client for the PowerDNS API.

## Usage

```c#
serviceCollection
    .AddOptions()
    .Configure<PowerDnsOptions>(opt =>
                      {
                        opt.Hostname = "localhost";
                        opt.Port = 443;
                        opt.UseTls = true;
                        opt.ApiKey = "changeme"
                      })
    .AddPowerDns();
var powerDns = serviceCollection.GetRequiredService<IPowerDns>(); 
```

### Get a list of all zones
```c#
List<Zone> zones = await powerDns.GetZonesAsync();
```

### Get a specific zone
```c#
Zone zone = await powerDns.GetZoneAsync("example.org");
```

### Create a new zone
```c#
var zone = new Zone(name: "example.org", nameServers: "ns1.example.org", "ns2.example.org")
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
};
await powerDns.CreateZoneAsync(zone);
```

### Patch a Record Set
```c#
RecordSet recordSet = await powerDns.GetRecordSetAsync(zoneName: "example.org", "www.example.org");

recordSet.ChangeType = ChangeType.Replace;
recordSet.Records.Add(new ResourceRecord(content: "4.3.2.1"));

await powerDns.PatchRecordSetAsync(zoneName, "example.org", recordSet);
```

### Delete a zone
```c#
await powerDns.DeleteZoneAsync("example.org");
```