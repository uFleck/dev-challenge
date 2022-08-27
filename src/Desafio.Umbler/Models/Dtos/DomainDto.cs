using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Umbler.Models.Dtos
{
  public class DomainDto
  {
    public string Name { get; set; }
    public string Ip { get; set; }
    public string WhoIs { get; set; }
    public string HostedAt { get; set; }
  }
}