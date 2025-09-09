using Relier.Domain.Validation;

namespace Relier.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get;  set; }
        public ICollection<Category> Products { get; set; }
        public Category(string name) => ValidateDomain(name);
        public Category()
        {
                
        }
        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "invalid id");
            Id = id;
            ValidateDomain(name);
        }

        private void Update(string name) => ValidateDomain(name);

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "O nome não pode ser vazio");
        }
    }
}
