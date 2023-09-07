using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Persistence
{
    public class SeguridadData
    {
        public static async Task InsertarUsuario(SeguridadContexto context, UserManager<Usuario> usuariomanager)
        {
            if (!usuariomanager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nombre = "Jorge",
                    Apellido = "Prieto",
                    Direccion = "Quito",
                    UserName = "jfpl",
                    Email = "jorgefprietol@gmail.com"
                };
                await usuariomanager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}
