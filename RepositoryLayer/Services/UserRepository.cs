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
        
        public UserRepository(UserContext context, IConfiguration _config)
        {
            this.context = context;
            this._config = _config;
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
            // userEntity.UserPassword = model.UserPassword;+
            userEntity.UserPassword = Encrypt(model.UserPassword);
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }


        public string UserLogin(LoginModel model)
        {
            try
            {
                UserEntity user = context.UserTable.ToList().Find(x => x.UserEmail == model.User_Email);
                if (user != null)
                {
                    if (Decrypt(user.UserPassword) == model.User_Passwords)
                    {
                        string token = GenerateToken(user.UserEmail, user.UserId);
                        // Attach token to user entity or return it along with userEntity
                        return token;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserEmail",Email),
                new Claim("UserId", Convert.ToString(UserId))
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

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

        public string ForgetPassword(string UserEmail)
        {
            UserEntity user = context.UserTable.Find(UserEmail);
            if (user != null)
            {
                string token = GenerateToken(user.UserEmail, user.UserId);
                return "Token Sent Successfully";
            }
            else
            {
                return null;
            }
        }

        public bool ResetPassword(string Email, ResetPasswordModel model)
        {
            UserEntity user = context.UserTable.ToList().Find(user => user.UserEmail == Email);
            if (user != null)
            {
                user.UserPassword = Encrypt(model.newPassword);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
