using Desafio.Umbler.Models;
using Desafio.Umbler.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Umbler.Services
{
    public interface IDomainService
    {
        Task<Domain> GetDomain(string domainName); 
    }
}
