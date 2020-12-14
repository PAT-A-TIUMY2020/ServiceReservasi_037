using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        string connectionString = "Data Source=LAPTOP-5QOEH7MN;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=iDhanzaghnia99";
        SqlConnection connection;
        //untuk menghubungkan database
        SqlCommand com;


        public string deletePemesanan(string idPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_Reservasi = '" + idPemesanan + "'";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return a;
        }

        public List<DetailLokasi> DetailLokasi()
        {
            //deklarasi nama List
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "select ID_Lokasi, Nama_Lokasi, Deskripsi_Full, Kuota from dbo.Lokasi";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    //array
                    data.IdLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string editPemesanan(string IdPemesanan, string NamaCustomer, string NoTelp)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_Customer = '" + NamaCustomer + "', NoTelp = '" + NoTelp + "'" +
                    " where ID_Reservasi = '" + IdPemesanan + "'";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return a;
        }

        public string pemesanan(string IdPemesanan, string NamaCustomer, string NoTelp, int JumlahPemesanan, string IdLokasi)
        {
            string a = "gagal";
            string sql = "insert into dbo.Pemesanan values ('" + IdPemesanan + "','" + NamaCustomer + "','" + NoTelp + "'," + JumlahPemesanan + ",'" + IdLokasi + "')";

            connection = new SqlConnection(connectionString);
            com = new SqlCommand(sql, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();

            string sql2 = "update dbo.Lokasi set Kuota = Kuota - " + JumlahPemesanan + " where ID_Lokasi = '" + IdLokasi + "'";
            connection = new SqlConnection(connectionString);
            com = new SqlCommand(sql2, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();

            a = "Sukses";
            return a;
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "select ID_Reservasi, Nama_Customer, No_Telp, " +
                    "Jumlah_Pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi l on p.ID_Lokasi = l.ID_Lokasi";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IdPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelp = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }

        public List<CekLokasi> reviewLokasi()
        {
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            string kategori = "";
            string sql = "select Kategori from Login where Username='" + username + "' and Password='" + password + "'";
            connection = new SqlConnection(connectionString);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }
            return kategori;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into Login values('" + username + "', '" + password + "', '" + kategori + "')";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql2 = "update into Login SET Username='" + username + "', Password= '" + password + "', Kategori='" + kategori + "' where ID_login = " + id + "";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "select ID_login from dbo.Login where Username = '" + username + "'";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                connection.Close();
                string sql2 = "delete from Login where ID_login=" + id + "";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<DataRegister> DataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_login, Username, Password, Kategori from Login";
                connection = new SqlConnection(connectionString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return list;
        }
    }



}