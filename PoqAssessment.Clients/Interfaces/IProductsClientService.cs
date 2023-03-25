using PoqAssessment.Clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoqAssessment.Clients.Interfaces;
public interface IProductsClientService
{
    Task<ClientProducts> FetchProductsAsync(string url, CancellationToken cancellationToken);
}
