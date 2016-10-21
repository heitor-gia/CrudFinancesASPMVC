using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Finances.Connection;
using Finances.Models.Entities;
using System.Text;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Finances.Models.Repositories
{
    public class UsersRepository
    {
        ConnectionClass rj45;

        public UsersRepository()
        {
            rj45 = new ConnectionClass();
        }

        public void Create(User user)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("INSERT INTO users(name_user,password_user) ");
            sql.Append("values (@name,@password)");

          


            cmm.Parameters.AddWithValue("@name", user.name);
            cmm.Parameters.AddWithValue("@password",md5Encrypt(user.password));
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public User Login(User user)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();

            sql.Append("SELECT * FROM users ");
            sql.Append("WHERE name_user=@name ");
            sql.Append("AND password_user=@password ");

            cmm.Parameters.AddWithValue("@name",user.name);
            cmm.Parameters.AddWithValue("@password", md5Encrypt(user.password));
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);
           User result = null;
            while (dr.Read())
            {
                result = new User()
                {
                    id = (int)dr["id_user"],
                    name = (string)dr["name_user"]
                };
            }
            return result;
        }

        public static string md5Encrypt(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));
            StringBuilder result = new StringBuilder();
            foreach(var c in hashedBytes)
            {
                result.Append(c.ToString("X2"));
            }
            return result.ToString();
        }
    }
}