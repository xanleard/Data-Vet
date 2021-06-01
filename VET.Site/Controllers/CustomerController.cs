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
    using VET.DataBase.Models;
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

        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return this.View("Create");
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create(CreateCustomersViewModel createModel)
        {
            if (createModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(createModel);
            }

            var newcustome = new Customer
            {
                Name = createModel.Name,
                IdentificationCard = createModel.IdentificationCard,
                BirthDate = createModel.BirthDate,
                Direction = createModel.Direction,
                Telephone1 = createModel.Telephone1,
                Telephone2 = createModel.Telephone2,
                Email = createModel.Email,
            };

            var result = await this.customersManager.CreateAsync(newcustome);
            if (!result.Succeeded)
            {
                foreach (var kvp in result.ValidationMessages)
                {
                    foreach (var message in kvp.Value)
                    {
                        this.ModelState.AddModelError(kvp.Key, message);
                    }
                }

                return this.View(createModel);
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var oldcustomer = await this.customersManager.FindByIdAsync(id.Value);

            if (oldcustomer == null)
            {
                return this.NotFound();
            }

            await this.customersManager.DeleteAsync(oldcustomer);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(EditCustomersViewModel editModel)
        {
            if (editModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(editModel);
            }

            var customerToEdit = await this.customersManager.FindByIdAsync(editModel.Id);

            if (customerToEdit == null)
            {
                return this.NotFound();
            }

            customerToEdit.Name = editModel.Name;
            customerToEdit.IdentificationCard = editModel.IdentificationCard;
            customerToEdit.BirthDate = editModel.BirthDate;
            customerToEdit.Direction = editModel.Direction;
            customerToEdit.Telephone1 = editModel.Telephone1;
            customerToEdit.Telephone2 = editModel.Telephone2;
            customerToEdit.Email = editModel.Email;
            var editResult = await this.customersManager.EditAsync(customerToEdit);

            if (!editResult.Succeeded)
            {
                foreach (var kvp in editResult.ValidationMessages)
                {
                    foreach (var message in kvp.Value)
                    {
                        this.ModelState.AddModelError(kvp.Key, message);
                    }
                }

                return this.View(editModel);
            }

            return this.RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var editList = await this.customersManager.FindByIdAsync(id.Value);

            if (editList == null)
            {
                return this.NotFound();
            }

            var list = new EditCustomersViewModel
            {
                Id = editList.Id,
                Name = editList.Name,
                IdentificationCard = editList.IdentificationCard,
                BirthDate = editList.BirthDate,
                Direction = editList.Direction,
                Telephone1 = editList.Telephone1,
                Telephone2 = editList.Telephone2,
                Email = editList.Email,
            };

            return this.View(list);
        }
    }
}
