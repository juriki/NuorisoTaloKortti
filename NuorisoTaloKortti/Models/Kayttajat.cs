namespace NuorisoTaloKortti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;



    public partial class Kayttajat
    {
        public int KayttajaId { get; set; }
        public string Kayttajanimi { get; set; }
        [Required(ErrorMessage = "Käyttäjätunnus on pakollinen tieto!")]
        public string Salasana { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Anna salasana!")]
        public bool Yllapito { get; set; }
        public string LoginErrorMessage { get; internal set; }
    }
}