// <copyright file="CustomerController.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VET.Core.Customers;
    using VET.Site.Models.Customers;

    public class CustomerController : Controller
    {
        private readonly ICustomersManager customersManager;

        public CustomerController(ICustomersManager customersManager)
        {
            this.customersManager = customersManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Index()
        {
            var allcustomer = await this.customersManager.GetAllAsync();
            var customerList = allcustomer.Select(l => new IndexCustomersViewModel
            {
                Id = l.Id,
                Name = l.Name,
                IdentificationCard = l.IdentificationCard,

                BirthDate = l.BirthDate,
                Direction = l.Direction,
                Telephone1 = l.Telephone1,
                Telephone2 = l.Telephone2,
                Email = l.Email,
                CreationDate = l.CreationDate,
                UpdateDate = l.UpdateDate,
            });

            return this.View(customerList);
        }
    }
}
