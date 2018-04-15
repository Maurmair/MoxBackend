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
            Target target = new Target();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Target WHERE Date = '" + targetToSave.Date.ToString("yyyy-MM-dd") + "' AND DeviceId='" + targetToSave.DeviceId + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                return "Combinatie van Datum en Apparaatnaam bestaan reed.";
            }
            else {
                mySqlReader.Close();
                DateTime targetDateDayBefore = targetToSave.Date.AddDays(-1);
                Target targetYesterday = getTarget(targetDateDayBefore, targetToSave.DeviceId);
                mySqlReader.Close();
                sqlString = "INSERT INTO Target (Date, ActiveMinutes, Steps, DeviceId) VALUES ('"
              + targetToSave.Date.ToString("yyyy-MM-dd") + "',"
              + targetYesterday.ActiveMinutes + ","
              + targetYesterday.Steps + ",'"
              + targetYesterday.DeviceId + "')";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                String id = cmd.LastInsertedId.ToString();
                return id;
            }            
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
                target.DeviceId = mySqlReader.GetString(3);
                return target;
            }
            else
            {
                return null;
            }
        }

        public Target getTarget(DateTime Id, String DeviceId)
        {
            Target target = new Target();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Target WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "' AND DeviceId='" + DeviceId + "'";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();

            if (mySqlReader.Read())
            {
                target.Date = mySqlReader.GetDateTime(0);
                target.ActiveMinutes = mySqlReader.GetInt32(1);
                target.Steps = mySqlReader.GetInt32(2);
                target.DeviceId = mySqlReader.GetString(3);
                mySqlReader.Close();
                return target;
                
            }
            else
            {
                mySqlReader.Close();
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
                target.DeviceId = mySqlReader.GetString(3);
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

        public bool updateTarget(DateTime Id, Target targetToSave, String DeviceId)
        {
            DateTime today = Id;
            DateTime yesterday = Id.AddDays(-1);
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Target WHERE Date = '" + Id.ToString("yyyy-MM-dd") +"' AND DeviceId='" + DeviceId + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "UPDATE Target Set Date='"
                    + targetToSave.Date.ToString("yyyy-MM-dd") + "', ActiveMinutes= "
                    + targetToSave.ActiveMinutes + ", Steps="
                    + targetToSave.Steps + ", DeviceId='" 
                    + targetToSave.DeviceId + "' WHERE Date = '"
                    + Id.ToString("yyyy-MM-dd") + "' AND DeviceId='" + DeviceId + "'";
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