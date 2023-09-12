﻿using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.JwtLogic
{
    public class JwtGenerator : IJwtGenerator
    {

        public string CreateToken(Usuario usuario)
        {

            var claims = new List<Claim>
                {
                    //new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName),
                    new Claim("username", usuario.UserName),
                    new Claim("nombre", usuario.Nombre),
                    new Claim("apellido", usuario.Apellido)
                };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VteLKW55nbdpTb8vjYHNI0B4NA81ORUr"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(300),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
