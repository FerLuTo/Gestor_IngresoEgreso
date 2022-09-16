using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ControlIngresoGasto.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name ="Nombre Categoria")]
        public string NombreCategoria{ get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name ="Tipo")]
        public string Tipo { get; set; } //IN ingreso GA Gasto

        [Required]
        public bool Estado { get; set; }//True, False
    }
}
