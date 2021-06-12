// <copyright file="IndexVisitViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.Visits
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexVisitViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateVisit { get; set; }

        public bool IsComplet { get; set; }

        public string VisitDetail { get; set; }

        public string NoteFirst { get; set; }

        public string NoteSeconds { get; set; }

        public string NoteThird { get; set; }

        public string TypeAnimalName { get; set; }

        public string CustomerName { get; set; }
    }
}
