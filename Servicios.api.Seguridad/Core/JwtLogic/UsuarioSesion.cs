using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Servicios.api.Seguridad.Core.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.JwtLogic
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUsuarioSesion()
        {
            //string userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
            string userName = JsonConvert.DeserializeObject<UsuarioDto>(_httpContextAccessor.HttpContext.Session.GetString("UserSession")).Username.ToString();
            return userName;
        }

        public void SetUsuarioSesion(UsuarioDto claims)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserSession", value: JsonConvert.SerializeObject(claims));
        }
    }
}
