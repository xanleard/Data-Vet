namespace VET.Site.Models.Animals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VET.DataBase.Models;

    public class CreateAnimalsViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string IdentificationCard { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Race { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Observations { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public DateTime LastVisit { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int SexId { get; set; }

        public IEnumerable<Sex> Sexes { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int TypeAnimalId { get; set; }

        public IEnumerable<TypeAnimal> TypeAnimals { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int UnitMeasurementId { get; set; }

        public IEnumerable<UnitMeasurement> UnitMeasurements { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int CustomerId { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
