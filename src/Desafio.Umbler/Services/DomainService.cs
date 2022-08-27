using Desafio.Umbler.Models;
using Desafio.Umbler.Models.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Desafio.Umbler.Services
{
    public class DomainService : IDomainService
    {
        private readonly HttpClient httpClient;

        public DomainService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Domain> GetDomain(string domainName)
        {
            return await httpClient.GetFromJsonAsync<Domain>("/api/domain/" + domainName);
        }
    }
}
