// <copyright file="Animal.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IdentificationCard { get; set; }

        public string Race { get; set; }

        [ForeignKey(nameof(Sexes))]
        public int SexId { get; set; }

        public Sex Sexes { get; set; }

        public decimal Weight { get; set; }

        [ForeignKey(nameof(UnitMeasurements))]
        public int UnitMeasurementId { get; set; }

        public UnitMeasurement UnitMeasurements { get; set; }

        public string Observations { get; set; }

        public DateTime LastVisit { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DateUpdate { get; set; }

        [ForeignKey(nameof(TypeAnimals))]
        public int TypeAnimalId { get; set; }

        public TypeAnimal TypeAnimals { get; set; }

        [ForeignKey(nameof(Customers))]
        public int CustomerId { get; set; }

        public Customer Customers { get; set; }
    }
}
