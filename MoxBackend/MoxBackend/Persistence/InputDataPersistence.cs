using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MoxBackend.Models;
using System.Collections;

namespace MoxBackend.Persistence
{
    public class InputDataPersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;
        public InputDataPersistence()
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

        public String saveInputData(InputData inputDataToSave)
        {
            string sqlString = "INSERT INTO InputData (Date, ActiveMinutesReached, StepsReached, DeviceId) VALUES ('"
                + inputDataToSave.Date.ToString("yyyy-MM-dd") + "',"
                + inputDataToSave.ActiveMinutesReached + ","
                + inputDataToSave.StepsReached + ",'" 
                + inputDataToSave.DeviceId + "')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            String id = cmd.LastInsertedId.ToString();
            return id;
        }

        public InputData getInputData(DateTime Id)
        {
            InputData inputData = new InputData();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM InputData WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();

            if (mySqlReader.Read())
            {
                inputData.Date = mySqlReader.GetDateTime(0);
                inputData.ActiveMinutesReached = mySqlReader.GetInt32(1);
                inputData.StepsReached = mySqlReader.GetInt32(2);
                inputData.DeviceId = mySqlReader.GetString(3);            
                return inputData;
            }
            else
            {
                return null;
            }
        }

        public ArrayList getAllInputData()
        {
            ArrayList inputDataArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM InputData";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            while (mySqlReader.Read())
            {
                InputData inputData = new InputData();
                inputData.Date = mySqlReader.GetDateTime(0);
                inputData.ActiveMinutesReached = mySqlReader.GetInt32(1);
                inputData.StepsReached = mySqlReader.GetInt32(2);
                inputData.DeviceId = mySqlReader.GetString(3);
                inputDataArray.Add(inputData);
            }
            return inputDataArray;
        }

        public bool deleteInputData(DateTime Id)
        {
            InputData inputData = new InputData();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;

            String sqlString = "SELECT * FROM InputData WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "DELETE FROM InputData WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateInputData(DateTime Id, InputData inputDataToSave)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM InputData WHERE Date = '" + Id.ToString("yyyy-MM-dd") + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "UPDATE InputData Set Date='"
                    + inputDataToSave.Date.ToString("yyyy-MM-dd") + "', ActiveMinutesReached= "
                    + inputDataToSave.ActiveMinutesReached + ", StepsReached="
                    + inputDataToSave.StepsReached + ", DeviceId='"
                    + inputDataToSave.DeviceId + "' WHERE Date = '"
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