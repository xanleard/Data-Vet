// <copyright file="CreateVisitsViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.Visits
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VET.DataBase.Models;

    public class CreateVisitsViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int AnimalId { get; set; }

        public IEnumerable<Animal> Animals { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int CustomerId { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public DateTime DateVisit { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string VisitDetail { get; set; }

        public string NoteFirst { get; set; }

        public string NoteSeconds { get; set; }

        public string NoteThird { get; set; }
    }
}
