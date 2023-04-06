namespace NuorisoTaloKortti.ViewModels
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public class NuoriHuoltajaData
    {
        public int NuoriId { get; set; }

        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public System.DateTime SyntymaAika { get; set; }
        public string Puhelinnumero { get; set; }
        public string Osoite { get; set; }
        public string Postinumero { get; set; }
        public int Huoltaja { get; set; }
        public string SPosti { get; set; }
        public string Allergiat { get; set; }
        public bool Kuvauslupa { get; set; }
        public bool Aktivointi { get; set; }
        public byte[] Kuva { get; set; }
        public string Kayttajanimi { get; set; }
        //hulotaja get set
        public int HuoltajaId { get; set; }
        public string HuoltajaEtunimi { get; set; }
        public string HuoltajaSukunimi { get; set; }
        public string HuoltajaPuhelinnumero { get; set; }
        public string HuoltajaOsoite { get; set; }
        public string HulotajaPostinumero { get; set; }

    }
}