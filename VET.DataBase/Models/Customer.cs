// <copyright file="Customer.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.DataBase.Models
{
    using System;

    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IdentificationCard { get; set; }

        public DateTime BirthDate { get; set; }

        public string Direction { get; set; }

        public int Telephone1 { get; set; }

        public int Telephone2 { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
