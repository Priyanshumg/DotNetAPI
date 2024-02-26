using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using CommonLayer.RequestModel;
using RepositoryLayer.Interface;
using System;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserInterface
    {
        private readonly UserContext context;
        private readonly IConfiguration _config;
        private static readonly byte[] key = new byte[] { 0x45, 0x6F, 0x3F, 0x12, 0x98, 0xAB, 0xCD, 0xEF, 0x45, 0x6F, 0x3F, 0x12, 0x98, 0xAB, 0xCD, 0xEF };
        private static readonly byte[] iv = new byte[] { 0x45, 0x6F, 0x3F, 0x12, 0x98, 0xAB, 0xCD, 0xEF, 0x45, 0x6F, 0x3F, 0x12, 0x98, 0xAB, 0xCD, 0xEF };
        
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            if (context.UserTable.Any(user => user.UserEmail == model.UserEmail))
            {throw new Exception("Email Address already exist");}
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = model.FirstName;
            userEntity.LastName = model.LastName;
            userEntity.UserName = model.UserName;
            userEntity.UserEmail = model.UserEmail;
            // userEntity.UserPassword = model.UserPassword;
            userEntity.UserPassword = Encrypt(model.UserPassword);
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }


        public UserEntity UserLogin(LoginModel model)
        {
            UserEntity userEntity = new UserEntity();
            try
            {
                if (userEntity.UserEmail != null)
                {
                    if (userEntity.UserEmail == model.UserEmail)
                    {
                        // if (userEntity.UserPassword == model.UserPassword)
                        // {
                        //     return userEntity;
                        // }
                        if (Decrypt(userEntity.UserPassword) == model.User_Passwords)
                        {
                            return userEntity;
                        }
                        else
                        {
                            throw new Exception("Invalid Password");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid UserName,Create new Account or Add ");
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(string Email, string UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId", UserId)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        public static string Encrypt(string UserPassword)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(UserPassword);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
