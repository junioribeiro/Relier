using Relier.Domain.Validation;

namespace Relier.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public decimal Price { get;  set; }
        public int Stock { get;  set; }
        public string Image { get;  set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product()
        {
                
        }

        /// <summary>
        /// valida no insert
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="image"></param>
        public Product(string name, string description, decimal price, int stock, string image) => ValidateDomain(name, description, price, stock, image);

        /// <summary>
        /// valida ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="image"></param>
        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }


        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "O nome não pode ser vazio");
            DomainExceptionValidation.When(price < 0, "o preço não pode ser zero");
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }
    }
}
