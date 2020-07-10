using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBBCFOODS.Ncapas
{
    interface IGENERIC<clase>
    {
        bool Insertar(clase obj);
        bool Modificar(clase obj);
        bool Eliminar(clase obj);
        //clase Listar(clase obj);
        List<clase> listarTodo();
    }
}
