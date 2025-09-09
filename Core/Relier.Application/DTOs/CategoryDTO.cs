using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Application.DTOs
{
    public class CategoryDTO
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Mome")]
        [MaxLength(30,ErrorMessage = "O nome deve ter no maximo 30 caracteres")]
        public string Name { get; set; }
    }
}
