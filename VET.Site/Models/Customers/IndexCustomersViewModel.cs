// <copyright file="IndexCustomersViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexCustomersViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IdentificationCard { get; set; }

        public string BirthDate { get; set; }

        public string Direction { get; set; }

        public int Telephone1 { get; set; }

        public int Telephone2 { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
