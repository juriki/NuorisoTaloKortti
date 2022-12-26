using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace NuorisoTaloKortti
{
    internal class PasswordHash
    {
        private string Password { get; set; }
        private string _salt = "87g4bdwe6gtfywo8e";
        private string _passwordHash { get; set; }

        public string encodePassword(string password)
        {
            var suola = Encoding.UTF8.GetBytes(this._salt);
            var value = System.Text.Encoding.UTF8.GetBytes(password);
            var hmacMD5 = new HMACMD5(suola);
            var saltedHash = hmacMD5.ComputeHash(value);
            string hex = BitConverter.ToString(saltedHash);
            this.Password = hex.Replace("-", "");
            return hex.Replace("-", "");
        }
        public bool passwordChek(string passwordHas, string password)
        {
            var suola = Encoding.UTF8.GetBytes(this._salt);
            var value = System.Text.Encoding.UTF8.GetBytes(password);
            var hmacMD5 = new HMACMD5(suola);
            var saltedHash = hmacMD5.ComputeHash(value);
            string hex = BitConverter.ToString(saltedHash);
            this.Password = hex.Replace("-", "");
            if (Password == passwordHas)
            {
                return true;
            }
            return false;
        }


    }
}