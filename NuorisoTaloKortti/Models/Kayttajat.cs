//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NuorisoTaloKortti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Kayttajat
    {
        public int KayttajaId { get; set; }
        [Required(ErrorMessage = "Käyttäjätunnus on pakollinen tieto!")]
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
