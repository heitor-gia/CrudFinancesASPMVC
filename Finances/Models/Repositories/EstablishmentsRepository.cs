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
    public class EstablishmentsRepository
    {
        ConnectionClass rj45;

        public EstablishmentsRepository()
        {
            rj45 = new ConnectionClass();
        }

        public void Create(Establishment establishment)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("INSERT INTO establishments(name_establishment,id_user_establishment) ");
            sql.Append("values (@name,@id_user)");

            cmm.Parameters.AddWithValue("@name", establishment.name);
            cmm.Parameters.AddWithValue("@id_user", establishment.id_user);
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public List<Establishment> GetAllFromUser(int id_user)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            List<Establishment> establishments = new List<Establishment>();
            sql.Append("SELECT * FROM establishments WHERE id_user_establishment = @id_user");

            cmm.Parameters.AddWithValue("@id_user", id_user);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);

            while (dr.Read())
            {
                establishments.Add(new Establishment()
                {
                    id = (int)dr["id_establishment"],
                    name = (string)dr["name_establishment"],
                    id_user = (int)dr["id_user_establishment"]
                });
            }
            dr.Close();
            return establishments;
        }

        public Establishment GetById(int id)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();

            sql.Append("SELECT * FROM establishments ");
            sql.Append("WHERE id_establishment=@id");

            cmm.Parameters.AddWithValue("@id", id);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);
            Establishment establishment = null;
            while (dr.Read())
            {
                establishment = new Establishment()
                {
                    id = (int)dr["id_establishment"],
                    name = (string)dr["name_establishment"],
                    id_user = (int)dr["id_user_establishment"]
                };
            }
            dr.Close();
            return establishment;
        }

        public void Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("DELETE from establishmemts");
            sql.Append("WHERE id_establishment=@id");

            cmm.Parameters.AddWithValue("@id", id);
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public void Edit(Establishment establishment)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("UPDATE establishments ");
            sql.Append("SET name_establishment = @name  WHERE id_establishment=@id");

            cmm.CommandText = sql.ToString();
            cmm.Parameters.AddWithValue("@id", establishment.id);
            cmm.Parameters.AddWithValue("@name", establishment.name);

            rj45.ExecuteCommand(cmm);
        }
    }
}