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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task Add(CategoryDTO categoryDTO)
        {
            var entity = _mapper.Map<Category>(categoryDTO);
            await _repository.Create(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetById(id);
            await _repository.Delete(entity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(entities);
        }

        public async Task<CategoryDTO> GetById(int Id)
        {
            var entity = await _repository.GetById(Id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var entity = _mapper.Map<Category>(categoryDTO);
            await _repository.update(entity);
        }
    }
}
