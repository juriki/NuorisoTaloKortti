namespace NuorisoTaloKortti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;



    public partial class Kayttajat
    {
        public int KayttajaId { get; set; }
        [Required(ErrorMessage = "K�ytt�j�tunnus on pakollinen tieto!")]
        public string Kayttajanimi { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Anna salasana!")]
        public string Salasana { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Anna salasana!")]
        public string uusiSalasana { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Anna salasana!")]
        public string ToistaSalasana { get; set; }
        public bool Yllapito { get; set; }
        public string LoginErrorMessage { get; internal set; }
    }
}