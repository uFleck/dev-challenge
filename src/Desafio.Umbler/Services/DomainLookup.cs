using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Umbler.Models;
using DnsClient;
using Whois.NET;

namespace Desafio.Umbler.Services
{
  public partial class DomainLookup
  {
    public async Task<Domain> Request(string domainName) 
    {
      var response = await WhoisClient.QueryAsync(domainName);
      if (response.Raw.Contains("Registry Domain ID"))
      {
        var lookup = new LookupClient();
        var result = await lookup.QueryAsync(domainName, QueryType.ANY);
        var record = result.Answers.ARecords().FirstOrDefault();
        var address = record?.Address;
        var ip = address?.ToString();

        var hostResponse = await WhoisClient.QueryAsync(ip);

        return new Domain {
          Name = domainName,
          Ip = ip,
          UpdatedAt = DateTime.Now,
          WhoIs = response.Raw,
          Ttl = record?.TimeToLive ?? 0,
          HostedAt = hostResponse.OrganizationName
        };
      } else
      {
        return new Domain {
          Name = domainName,
          WhoIs = response.Raw,
          UpdatedAt = DateTime.Now,
          Ttl = 0,
          HostedAt = "N/A",
          Ip = "N/A"
        };
      }

      
    }
  }
}