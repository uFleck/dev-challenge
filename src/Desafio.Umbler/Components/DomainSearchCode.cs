using System.Threading.Tasks;
using Desafio.Umbler.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Desafio.Umbler.Models.Dtos;
using Desafio.Umbler.Models;
using System;
using System.Threading;
using System.Linq;

namespace Desafio.Umbler.Components
{
  public class DomainSearchCode : ComponentBase
  {
    public string DomainAddress { get; set; }
    public Domain DomainData { get; set; }
    [Inject]
    public IDomainService DomainService { get; set; }
    public bool Error { get; set; } = false;
    public bool IsLoading { get; set; } = false;
    public bool ShowWhois { get; set; } = false;
    public MarkupString WhoIs { get; set; }

    public async Task SearchDomain()
    {
      if(DomainAddress.Contains(".") && DomainAddress[DomainAddress.Length - 1] != '.' && DomainAddress[0] != '.')
      {
        DomainData = null;
        IsLoading = true;
        DomainAddress = DomainAddress.Replace(" ", "");
        string firstFourChar = new string(DomainAddress.Take(4).ToArray());
        if (firstFourChar == "www.")
        {
          DomainAddress = DomainAddress.Replace("www.", "");
        }
        DomainData = await DomainService.GetDomain(DomainAddress);
        WhoIs = new MarkupString(DomainData.WhoIs);
        IsLoading = false;
      }
      else
      {
        Error = true;
        await Task.Delay(3500);
        Error = false;
      }
    }
    public void Toggle() => ShowWhois = !ShowWhois;
  }
}