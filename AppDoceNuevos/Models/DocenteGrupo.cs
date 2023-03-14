using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDoceNuevos.Models
{
    public class DocenteGrupo
    {
        public int Id { get; set; }

        public int IdDocente { get; set; }

        public int IdGrupo { get; set; }

        public int IdPeriodo { get; set; }

        public virtual Docente IdDocenteNavigation { get; set; } = null!;

        public virtual Grupo IdGrupoNavigation { get; set; } = null!;

        public virtual Periodo IdPeriodoNavigation { get; set; } = null!;
    }
}
