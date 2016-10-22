using Finances.Connection;
using Finances.Models.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Finances.Models.Repositories
{
    public class CategoriesRepository
    {
        ConnectionClass rj45;

        public CategoriesRepository()
        {
            rj45 = new ConnectionClass();
        }

        public void Create(Category category)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("INSERT INTO categories(name_category,id_user_category) ");
            sql.Append("values (@name,@id_user)");

            cmm.Parameters.AddWithValue("@name", category.name);
            cmm.Parameters.AddWithValue("@id_user", category.id_user);
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public List<Category> GetAllFromUser(int id_user)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            List<Category> categories = new List<Category>();
            sql.Append("SELECT * FROM categories WHERE id_user_category = @id_user");

            cmm.Parameters.AddWithValue("@id_user", id_user);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);

            while (dr.Read())
            {
                categories.Add(new Category()
                {
                    id = (int)dr["id_category"],
                    name = (string)dr["name_category"],
                    id_user = (int)dr["id_user_category"]
                });
            }
            dr.Close();
            return categories;
        }

        public Category GetById(int id)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();

            sql.Append("SELECT * FROM categories ");
            sql.Append("WHERE id_category=@id");

            cmm.Parameters.AddWithValue("@id", id);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);
            Category category = null;
            while (dr.Read())
            {
                category = new Category()
                {
                    id = (int)dr["id_category"],
                    name = (string)dr["name_category"],
                    id_user = (int)dr["id_user_category"]
                };
            }
            dr.Close();
            return category;
        }

        public void Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("DELETE from categories ");
            sql.Append("WHERE id_category=@id");

            cmm.Parameters.AddWithValue("@id", id);
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public void Edit(Category category)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("UPDATE categories ");
            sql.Append("SET name_category = @name  WHERE id_category=@id");

            cmm.CommandText = sql.ToString();
            cmm.Parameters.AddWithValue("@id", category.id);
            cmm.Parameters.AddWithValue("@name", category.name);

            rj45.ExecuteCommand(cmm);
        }
    }
}