// <copyright file="EditTypeAnimalsViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.TypeAnimals
{
    using System.ComponentModel.DataAnnotations;

    public class EditTypeAnimalsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(100, ErrorMessage = "La longitud del campo debe ser menor a 100 caracteres")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "La longitud del campo debe ser menor a 200 caracteres")]
        public string Description { get; set; }
    }
}
