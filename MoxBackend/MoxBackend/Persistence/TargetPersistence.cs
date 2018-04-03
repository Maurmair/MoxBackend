using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MoxBackend.Models;
using System.Collections;

namespace MoxBackend.Persistence
{
    public class TargetPersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public TargetPersistence()
        {
            string myConnectionString = "server=108.167.172.114;uid=maurmair_moxAdm;pwd=RandomPw!;database=maurmair_moxDb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                ex.ToString();
            }
        }

        public String saveTarget(Target targetToSave)
        {
            string sqlString = "INSERT INTO Target (Date, ActiveMinutes, Steps) VALUES ('"
                + targetToSave.Date.ToString("yyyy-MM-dd") + "',"
                + targetToSave.ActiveMinutes + ","
                + targetToSave.Steps + ")";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            String id = cmd.LastInsertedId.ToString();
            return id;
        }

        public Target getTarget(DateTime Id)
        {
            Target target = new Target();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Target WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();

            if (mySqlReader.Read())
            {
                target.Date = mySqlReader.GetDateTime(0);
                target.ActiveMinutes = mySqlReader.GetInt32(1);
                target.Steps = mySqlReader.GetInt32(2);
                return target;
            }
            else
            {
                return null;
            }
        }

        public ArrayList getTargets()
        {
            ArrayList targetArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Target";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            while (mySqlReader.Read())
            {
                Target target = new Target();
                target.Date = mySqlReader.GetDateTime(0);
                target.ActiveMinutes = mySqlReader.GetInt32(1);
                target.Steps = mySqlReader.GetInt32(2);
                targetArray.Add(target);
            }
            return targetArray;
        }

        public bool deleteTarget(DateTime Id)
        {
            Target device = new Target();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;

            String sqlString = "SELECT * FROM Target WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "DELETE FROM Target WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateTarget(DateTime Id, Target targetToSave)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Target WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "UPDATE Target Set Date='"
                    + targetToSave.Date.ToString("yyyy-MM-dd") + "', ActiveMinutes= "
                    + targetToSave.ActiveMinutes + ", Steps="
                    + targetToSave.Steps + " WHERE Date = '"
                    + Id.ToString("yyyy-MM-dd") + "'";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}