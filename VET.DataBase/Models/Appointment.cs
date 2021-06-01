// <copyright file="Appointment.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public bool IsComplet { get; set; }

        public string VisitDetail { get; set; }

        public string NoteFirst { get; set; }

        public string NoteSeconds { get; set; }
    }
}
