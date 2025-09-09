using AutoMapper;
using Relier.Application.DTOs;
using Relier.Application.Interfaces;
using Relier.Domain.Entities;
using Relier.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Add(ProductDTO productDTO)
        {
            Product entity = _mapper.Map<Product>(productDTO);
            var response = await _repository.Create(entity);
            if (response != null)
                productDTO.Id = response.Id;
        }

        public async Task Delete(int Id)
        {
            Product entity = await _repository.GetById(Id);
            await _repository.Delete(entity);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(entities);
        }

        public async Task<ProductDTO?> GetById(int Id)
        {
            var entity = await _repository.GetById(Id);
            return _mapper.Map<ProductDTO>(entity);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var entity = _mapper.Map<Product>(productDTO);
            await _repository.update(entity);
        }
       
    }
}
