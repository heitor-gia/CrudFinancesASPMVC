﻿using Finances.Connection;
using Finances.Models.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Finances.Models.Repositories
{
    public class ExpensesRepository
    {
        ConnectionClass rj45;

        public ExpensesRepository()
        {
            rj45 = new ConnectionClass();
        }

        public void Create(Expense expense)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("INSERT INTO expenses(value_expense,date_expense,id_establishment_expense, id_category_expense,id_user_expense) ");
            sql.Append("values (@value,@date,@id_establishment,@id_category,@id_user)");

            cmm.Parameters.AddWithValue("@value", expense.value);
            cmm.Parameters.AddWithValue("@date", expense.date);
            cmm.Parameters.AddWithValue("@id_user", expense.id_user);
            cmm.Parameters.AddWithValue("@id_establishment", expense.id_establishment);
            cmm.Parameters.AddWithValue("@id_category", expense.id_category);
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public List<Expense> GetAllFromUser(int id_user)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            List<Expense> expenses = new List<Expense>();
            sql.Append("SELECT * FROM expenses WHERE id_user_expense = @id_user  ORDER BY date_expense DESC");

            cmm.Parameters.AddWithValue("@id_user", id_user);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);

            while (dr.Read())
            {
                expenses.Add(new Expense()
                {
                    id = (int)dr["id_expense"],
                    date = DateTime.Parse((string)dr["date_expense"]),
                    value = (float)dr["value_expense"],
                    id_establishment = (int)dr["id_establishment_expense"],
                    id_category = (int)dr["id_category_expense"],
                    id_user = (int)dr["id_user_expense"]
                });
            }

            return expenses;
        }

        public List<Expense> GetAllFromUser(int id_user,int limit)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            List<Expense> expenses = new List<Expense>();
            sql.Append("SELECT * FROM expenses WHERE id_user_expense = @id_user ORDER BY date_expense DESC LIMIT @limit");

            cmm.Parameters.AddWithValue("@id_user", id_user);
            cmm.Parameters.AddWithValue("@limit", limit);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);

            while (dr.Read())
            {
                expenses.Add(new Expense()
                {
                    id = (int)dr["id_expense"],
                    date = DateTime.Parse((string)dr["date_expense"]),
                    value = (float)dr["value_expense"],
                    id_establishment = (int)dr["id_establishment_expense"],
                    id_category = (int)dr["id_category_expense"],
                    id_user = (int)dr["id_user_expense"]
                });
            }

            return expenses;
        }


        public Expense GetById(int id)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();

            sql.Append("SELECT * FROM expenses ");
            sql.Append("WHERE id_expense=@id");

            cmm.Parameters.AddWithValue("@id", id);
            cmm.CommandText = sql.ToString();
            MySqlDataReader dr = rj45.getDataReader(cmm);
            Expense expense = null;
            while (dr.Read())
            {
                expense = new Expense()
                {
                    id = (int)dr["id_expense"],
                    date = DateTime.Parse((string)dr["date_expense"]),
                    value = (float)dr["value_expense"],
                    id_establishment = (int)dr["id_establishment_expense"],
                    id_category = (int)dr["id_category_expense"],
                    id_user = (int)dr["id_user_expense"]
                };
            }
            return expense;
        }

        public void Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("DELETE from expenses ");
            sql.Append("WHERE id=@id");

            cmm.Parameters.AddWithValue("@id", id);
            cmm.CommandText = sql.ToString();
            rj45.ExecuteCommand(cmm);
        }

        public void Edit(Expense expense)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmm = new MySqlCommand();
            sql.Append("UPDATE members ");
            sql.Append("SET value_expense = @value, date_expense= @date, id_establishment_expense = @id_establishment,");
            sql.Append("id_category_expense = @id_category WHERE id_expense=@id");

            cmm.CommandText = sql.ToString();
            cmm.Parameters.AddWithValue("@id", expense.id);
            cmm.Parameters.AddWithValue("@value", expense.value);
            cmm.Parameters.AddWithValue("@date", expense.date);
            cmm.Parameters.AddWithValue("@id_category", expense.id_category);
            cmm.Parameters.AddWithValue("@id_establishment", expense.id_establishment);

            rj45.ExecuteCommand(cmm);
        }
    }
}