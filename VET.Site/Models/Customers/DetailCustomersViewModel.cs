// <copyright file="DetailCustomersViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models.Customers
{
    using System.Collections.Generic;
    using VET.Site.Models.Animals;

    public class DetailCustomersViewModel : IndexCustomersViewModel
    {
        public IEnumerable<IndexAnimalsViewModel> Animals { get; set; }
    }
}
