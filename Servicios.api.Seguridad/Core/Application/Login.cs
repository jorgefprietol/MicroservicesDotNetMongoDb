using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Servicios.api.Seguridad.Core.Application.Dto;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using Servicios.api.Seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    [Authorize]
    public class Login
    {
        public class UsuarioLoginCommand : IRequest<UsuarioDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioLoginValidation : AbstractValidator<UsuarioLoginCommand>
        {
            public UsuarioLoginValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginCommand, UsuarioDto>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManeger;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _ijwtGenerator;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IUsuarioSesion _usuarioSesion;

            public UsuarioLoginHandler(SeguridadContexto context, UserManager<Usuario> userManeger, IMapper mapper, IJwtGenerator ijwtGenerator, SignInManager<Usuario> signInManager, IUsuarioSesion usuarioSesion)
            {
                _context = context;
                _userManeger = userManeger;
                _mapper = mapper;
                _ijwtGenerator = ijwtGenerator;
                _signInManager = signInManager;
                _usuarioSesion = usuarioSesion;
            }
            [Authorize]
            public async Task<UsuarioDto> Handle(UsuarioLoginCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _userManeger.FindByEmailAsync(request.Email);
                if(usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }
                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                if (resultado.Succeeded)
                {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDTO.Token = _ijwtGenerator.CreateToken(usuario);

                    _usuarioSesion.SetUsuarioSesion(usuarioDTO);
                    return usuarioDTO;
                }

                throw new Exception("Login Incorrecto");

            }
        }
    }
}
