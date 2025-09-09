using Relier.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Application.Interfaces
{
    public interface IProductService
    {
        Task Add(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Delete(int Id);
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO?> GetById(int Id);

    }
}
