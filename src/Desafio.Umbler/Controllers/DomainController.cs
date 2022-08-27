using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Umbler.Models;
using Whois.NET;
using Microsoft.EntityFrameworkCore;
using DnsClient;
using System.Collections.Generic;
using Desafio.Umbler.Models.Dtos;
using Desafio.Umbler.Services;

namespace Desafio.Umbler.Controllers
{
    [Route("api")]
    public class DomainController : Controller
    {
        private readonly DatabaseContext _db;
        public DomainLookup Lookup { get; } = new DomainLookup();

        public DomainController(DatabaseContext db)
        {
            _db = db;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> Get(string domainName)
        {
            var domain = await _db.Domains.FirstOrDefaultAsync(d => d.Name == domainName);

            if (domain == null)
            {
                domain = await Lookup.Request(domainName);
                _db.Domains.Add(domain);
            }

            if (DateTime.Now.Subtract(domain.UpdatedAt).TotalMinutes > domain.Ttl)
            {
                domain = await Lookup.Request(domainName);
            }

            await _db.SaveChangesAsync();

            return Ok(new DomainDto { 
                Name = domain.Name, 
                Ip = domain.Ip, 
                WhoIs = domain.WhoIs.Replace("\n", "<br>"), 
                HostedAt = domain.HostedAt 
            });
        }

        
    }
}
