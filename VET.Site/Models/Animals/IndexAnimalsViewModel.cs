// <copyright file="IndexAnimalsViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.Animals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexAnimalsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Race { get; set; }

        public string SexName { get; set; }

        public decimal Weight { get; set; }

        public string UnitMeasurementName { get; set; }

        public string Observations { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime LastVisit { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateUpdate { get; set; }

        public string TypeAnimalName { get; set; }

        public string CustomerName { get; set; }

        public int Telephone1 { get; set; }

        public int Telephone2 { get; set; }

        public string Email { get; set; }
    }
}
