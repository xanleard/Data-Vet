// <copyright file="Appointment.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Appointment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Animals))]
        public int AnimalId { get; set; }

        public Animal Animals { get; set; }

        [ForeignKey(nameof(Customers))]
        public int CustomerId { get; set; }

        public Customer Customers { get; set; }

        public DateTime DateVisit { get; set; }

        public bool IsComplet { get; set; }

        public string VisitDetail { get; set; }

        public string NoteFirst { get; set; }

        public string NoteSeconds { get; set; }

        public string NoteThird { get; set; }
    }
}
