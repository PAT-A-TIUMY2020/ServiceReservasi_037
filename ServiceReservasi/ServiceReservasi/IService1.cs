using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        //method proses input data
        string pemesanan(string IdPemesanan, string NamaCustomer, string NoTelp, int JumlahPemesanan, string IdLokasi);
        [OperationContract]
        string editPemesanan(string IdPemesanan, string NamaCustomer, string NoTelp);
        [OperationContract]
        string deletePemesanan(string idPemesanan);
        [OperationContract]
        //Menampilkan data yang ada di database
        List<CekLokasi> reviewLokasi();
        [OperationContract]
        //Menampilkan detail lokasi
        List<DetailLokasi> DetailLokasi();
        [OperationContract]
        List<Pemesanan> Pemesanan();
    }

    //Daftar Lokasi
    [DataContract]
    public class CekLokasi
    {
        [DataMember]
        public string IDLokasi { get; set; }
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string Deskripsi { get; set; }
    }

    //Menampilkan detail lokasi
    [DataContract]
    public class DetailLokasi
    {
        [DataMember]
        public string IdLokasi { get; set; }
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeskripsiFull { get; set; }
        [DataMember]
        public int Kuota { get; set; }
    }

    [DataContract]
    public class Pemesanan
    {
        [DataMember]
        public string IdPemesanan { get; set; }
        [DataMember]
        public string NamaCustomer { get; set; }
        [DataMember]
        public string NoTelp { get; set; }
        [DataMember]
        public int JumlahPemesanan { get; set; }
        [DataMember]
        public string Lokasi { get; set; }
    }
}