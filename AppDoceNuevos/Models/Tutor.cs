﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDoceNuevos.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string? Email { get; set; }

        public int Idusuario { get; set; }

        public virtual ICollection<AlumnoTutor> AlumnoTutor { get; } = new List<AlumnoTutor>();

        public virtual Usuario IdusuarioNavigation { get; set; } = null!;
    }
}
