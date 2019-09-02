using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace InsightDerm.Core.Service
{
    public class UserService : BaseService<User, UserDto>
    {
        public UserService(IUnitOfWork unitOfWork, 
                            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public User Authenticate(string username, string password)
        {
            var hashPassword = Hash(password);
            var user = Repository.GetFirstOrDefault(x => x.Username == username && x.Password == hashPassword, null, null, true);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("y9#tM!D~h?b`*#Kygq4R)J-GJupe:qA8");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        static string Hash(string input)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
