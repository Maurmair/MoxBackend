using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MoxBackend.Models;
using System.Collections;

namespace MoxBackend.Persistence
{
    public class DevicePersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public DevicePersistence()
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

        public String saveDevice(Device deviceToSave)
        {
            string sqlString = "INSERT INTO Device (DeviceId, CoupledDevice) VALUES ('"
                + deviceToSave.DeviceId + "','"
                + deviceToSave.CoupledDevice + "')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            String id = cmd.LastInsertedId.ToString();
            return id;
        }

        public Device getDevice(String DeviceId)
        {
            Device device = new Device();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Device WHERE DeviceId = " + DeviceId;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                device.RecordId = mySqlReader.GetInt32(0);
                device.DeviceId = mySqlReader.GetString(1);
                device.CoupledDevice = mySqlReader.GetString(2);
                return device;
            }
            else
            {
                return null;
            }
        }

        public ArrayList getDevices()
        {
            ArrayList deviceArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM Device";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            while (mySqlReader.Read())
            {
                Device device = new Device();
                device.RecordId = mySqlReader.GetInt32(0);
                device.DeviceId = mySqlReader.GetString(1);
                device.CoupledDevice = mySqlReader.GetString(2);
                deviceArray.Add(device);
            }
            return deviceArray;
        }

        public bool deleteDevice(String DeviceId)
        {
            Device device = new Device();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;

            String sqlString = "SELECT * FROM Device WHERE DeviceId = " + DeviceId;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "DELETE FROM Device WHERE DeviceId = " + DeviceId;
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateDevice(String DeviceId, Device deviceToSave)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;

            String sqlString = "SELECT * FROM Device WHERE ID = " + DeviceId;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {

                mySqlReader.Close();
                sqlString = "UPDATE Device SET DeviceId='" + deviceToSave.DeviceId + "', CoupledDevice='" + deviceToSave.CoupledDevice + "' WHERE DeviceId = " + DeviceId;
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
