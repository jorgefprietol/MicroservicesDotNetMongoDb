using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.Application.Dto;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class UsuarioActual
    {
        public class UsuarioActualCommand : IRequest<UsuarioDto> { }

        public class UsuarioActualHandler : IRequestHandler<UsuarioActualCommand, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _iusuarioSesion;
            private readonly IJwtGenerator _ijwtGeneratos;
            private readonly IMapper _mapper;

            public UsuarioActualHandler(UserManager<Usuario> userManager, IUsuarioSesion iusuarioSesion, IJwtGenerator ijwtGeneratos, IMapper mapper)
            {
                _userManager = userManager;
                _iusuarioSesion = iusuarioSesion;
                _ijwtGeneratos = ijwtGeneratos;
                _mapper = mapper;
            }
            public async Task<UsuarioDto> Handle(UsuarioActualCommand request, CancellationToken cancellationToken)
            {

                var usuario = await _userManager.FindByNameAsync(_iusuarioSesion.GetUsuarioSesion());

                if (usuario != null)
                {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDTO.Token = _ijwtGeneratos.CreateToken(usuario);
                    return usuarioDTO;
                }

                throw new Exception("No se encontro el usuario");
            }
        }
    }
}
