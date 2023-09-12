using Servicios.api.Seguridad.Core.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.JwtLogic
{
    public interface IUsuarioSesion
    {
        string GetUsuarioSesion();
        void SetUsuarioSesion(UsuarioDto claims);
    }
}
