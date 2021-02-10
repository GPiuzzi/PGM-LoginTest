using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGMCLIP.Models
{
    public class User
    {

        public int id_usuario { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string contraseña { get; set; }
    }
}