// <copyright file="CreateCustomersViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCustomersViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(100, ErrorMessage = "La longitud del campo debe ser menor a 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud del campo debe ser menor a 20 caracteres")]
        public string IdentificationCard { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(100, ErrorMessage = "La longitud del campo debe ser menor a 100 caracteres")]
        public string Direction { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int Telephone1 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int Telephone2 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }
    }
}
