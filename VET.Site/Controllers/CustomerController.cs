// <copyright file="CustomerController.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VET.Core.Animals;
    using VET.Core.Customers;
    using VET.Core.Sexes;
    using VET.Core.TypeAnimals;
    using VET.Core.UnitMeasurements;
    using VET.DataBase.Models;
    using VET.Site.Models.Animals;
    using VET.Site.Models.Customers;

    [Route("Customer/{Action}/{Id?}")]
    public class CustomerController : Controller
    {
        private readonly ICustomersManager customersManager;
        private readonly IAnimalsManager animalsManager;
        private readonly ISexesManager sexesManager;
        private readonly IUnitMeasurementsManager unitMeasurementsManager;
        private readonly ITypeAnimalsManager typeAnimalsManager;

        public CustomerController(
            ICustomersManager customersManager,
            IAnimalsManager animalsManager,
            ISexesManager sexesManager,
            IUnitMeasurementsManager unitMeasurementsManager,
            ITypeAnimalsManager typeAnimalsManager)
        {
            this.customersManager = customersManager;
            this.animalsManager = animalsManager;
            this.sexesManager = sexesManager;
            this.unitMeasurementsManager = unitMeasurementsManager;
            this.typeAnimalsManager = typeAnimalsManager;
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
                CreationDate = createModel.CreationDate,
                UpdateDate = createModel.UpdateDate,
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
            customerToEdit.CreationDate = editModel.CreationDate;
            customerToEdit.UpdateDate = editModel.UpdateDate;
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
                CreationDate = editList.CreationDate,
                UpdateDate = editList.UpdateDate,
            };

            return this.View(list);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Detail(int? id)
        {
            var customer = await this.customersManager.FindByIdAsync(id.Value);
            if (customer == null)
            {
                return this.NotFound();
            }

            var allanimals = await this.animalsManager.GetAllCustomerAsync(customer.Id);
            var animals = allanimals.Select(d => new IndexAnimalsViewModel
            {
                Id = d.Id,
                CustomerName = d.Customers.Name,
                Name = d.Name,
                Race = d.Race,
                SexName = d.Sexes.Description,
                TypeAnimalName = d.Sexes.Description,
                UnitMeasurementName = d.UnitMeasurements.Description + d.Weight.ToString(),
                LastVisit = d.LastVisit,
            });

            var model = new DetailCustomersViewModel();
            model.CreationDate = customer.CreationDate;
            model.UpdateDate = customer.UpdateDate;
            model.Telephone1 = customer.Telephone1;
            model.Email = customer.Email;
            model.Id = customer.Id;
            model.BirthDate = customer.BirthDate;
            model.Name = customer.Name;
            model.IdentificationCard = customer.IdentificationCard;
            model.Animals = animals;
            model.Direction = customer.Direction;
            return this.View(model);
        }

        [HttpGet("/Customer/Details/{id}/Animal/{action}/{animalId?}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateAnimal(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var customer = await this.customersManager.FindByIdAsync(id.Value);

            if (customer == null)
            {
                return this.NotFound();
            }

            var model = new CreateAnimalsViewModel();
            model.CustomerId = customer.Id;
            model.UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync();
            model.TypeAnimals = await this.typeAnimalsManager.GetAllAsync();
            model.Sexes = await this.sexesManager.GetAllAsync();
            model.Customers = await this.customersManager.GetAllAsync();
            model.LastVisit = DateTime.Now;
            return this.View(model);
        }

        [HttpPost("/Customer/Details/{id?}/Animal/{action}/{animalId?}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateAnimal(int? id, CreateAnimalsViewModel createModel)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            if (createModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                createModel.UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync();
                createModel.TypeAnimals = await this.typeAnimalsManager.GetAllAsync();
                createModel.Sexes = await this.sexesManager.GetAllAsync();
                createModel.Customers = await this.customersManager.GetAllAsync();
                return this.View(createModel);
            }

            var unit = await this.unitMeasurementsManager.FindByIdAsync(createModel.UnitMeasurementId);

            if (unit == null)
            {
                return this.NotFound();
            }

            var sex = await this.sexesManager.FindByIdAsync(createModel.SexId);

            if (sex == null)
            {
                return this.NotFound();
            }

            var customer = await this.customersManager.FindByIdAsync(createModel.CustomerId);

            if (customer == null)
            {
                return this.NotFound();
            }

            var typeanimal = await this.typeAnimalsManager.FindByIdAsync(createModel.TypeAnimalId);

            if (typeanimal == null)
            {
                return this.NotFound();
            }

            var newanimal = new Animal
            {
                CustomerId = createModel.CustomerId,
                DateUpdate = DateTime.Now,
                CreationDate = DateTime.Now,
                LastVisit = DateTime.Now,
                Age = createModel.Age,
                Name = createModel.Name,
                Race = createModel.Race,
                Observations = createModel.Observations,
                SexId = createModel.SexId,
                TypeAnimalId = createModel.TypeAnimalId,
                UnitMeasurementId = createModel.UnitMeasurementId,
                Weight = createModel.Weight,
            };

            var result = await this.animalsManager.CreateAsync(newanimal);
            if (!result.Succeeded)
            {
                foreach (var kvp in result.ValidationMessages)
                {
                    foreach (var message in kvp.Value)
                    {
                        this.ModelState.AddModelError(kvp.Key, message);
                    }
                }

                createModel.UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync();
                createModel.TypeAnimals = await this.typeAnimalsManager.GetAllAsync();
                createModel.Sexes = await this.sexesManager.GetAllAsync();
                createModel.Customers = await this.customersManager.GetAllAsync();

                return this.View(createModel);
            }

            return this.RedirectToAction("Detail", new { id = id.Value });
        }

        [HttpGet("/Customer/Details/{id}/Animal/{action}/{animalId?}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EditAnimal(int? id, int? animalId)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            if (animalId == null)
            {
                return this.BadRequest();
            }

            var edit = await this.animalsManager.FindByIdAsync(animalId.Value);

            if (edit == null)
            {
                return this.NotFound();
            }

            var model = new EditAnimalsViewModel
            {
                IdModel = edit.Id,
                LastVisit = edit.LastVisit,
                CustomerId = edit.CustomerId,
                Age = edit.Age,
                Name = edit.Name,
                Observations = edit.Observations,
                Race = edit.Race,
                SexId = edit.SexId,
                TypeAnimalId = edit.TypeAnimalId,
                UnitMeasurementId = edit.UnitMeasurementId,
                Weight = edit.Weight,
                UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync(),
                TypeAnimals = await this.typeAnimalsManager.GetAllAsync(),
                Sexes = await this.sexesManager.GetAllAsync(),
                Customers = await this.customersManager.GetAllAsync(),
            };

            return this.View(model);
        }

        [HttpPost("/Customer/Details/{id?}/Animal/{action}/{animalId?}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EditAnimal(int? id, int? animalId, EditAnimalsViewModel editModel)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            if (animalId == null)
            {
                return this.BadRequest();
            }

            if (editModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                editModel.UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync();
                editModel.TypeAnimals = await this.typeAnimalsManager.GetAllAsync();
                editModel.Sexes = await this.sexesManager.GetAllAsync();
                editModel.Customers = await this.customersManager.GetAllAsync();
                return this.View(editModel);
            }

            var unit = await this.unitMeasurementsManager.FindByIdAsync(editModel.UnitMeasurementId);

            if (unit == null)
            {
                return this.NotFound();
            }

            var sex = await this.sexesManager.FindByIdAsync(editModel.SexId);

            if (sex == null)
            {
                return this.NotFound();
            }

            var customer = await this.customersManager.FindByIdAsync(editModel.CustomerId);

            if (customer == null)
            {
                return this.NotFound();
            }

            var typeanimal = await this.typeAnimalsManager.FindByIdAsync(editModel.TypeAnimalId);

            if (typeanimal == null)
            {
                return this.NotFound();
            }

            var animalToEdit = await this.animalsManager.FindByIdAsync(editModel.IdModel);

            if (animalToEdit == null)
            {
                return this.NotFound();
            }

            animalToEdit.DateUpdate = DateTime.Now;
            animalToEdit.CustomerId = editModel.CustomerId;
            animalToEdit.Age = editModel.Age;
            animalToEdit.LastVisit = editModel.LastVisit;
            animalToEdit.Name = editModel.Name;
            animalToEdit.Observations = editModel.Observations;
            animalToEdit.Race = editModel.Race;
            animalToEdit.SexId = editModel.SexId;
            animalToEdit.TypeAnimalId = animalToEdit.TypeAnimalId;
            animalToEdit.UnitMeasurementId = editModel.UnitMeasurementId;
            animalToEdit.Weight = editModel.Weight;
            var editResult = await this.animalsManager.EditAsync(animalToEdit);

            if (!editResult.Succeeded)
            {
                foreach (var kvp in editResult.ValidationMessages)
                {
                    foreach (var message in kvp.Value)
                    {
                        this.ModelState.AddModelError(kvp.Key, message);
                    }
                }

                editModel.UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync();
                editModel.TypeAnimals = await this.typeAnimalsManager.GetAllAsync();
                editModel.Sexes = await this.sexesManager.GetAllAsync();
                editModel.Customers = await this.customersManager.GetAllAsync();
                return this.View(editModel);
            }

            return this.RedirectToAction("Detail", new { id = id.Value });
        }

        [HttpPost("/Customer/Details/{id?}/Animal/{action}/{animalId?}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteAnimal(int? id, int? animalId)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            if (animalId == null)
            {
                return this.BadRequest();
            }

            var oldanimal = await this.animalsManager.FindByIdAsync(animalId.Value);

            if (oldanimal == null)
            {
                return this.NotFound();
            }

            await this.animalsManager.DeleteAsync(oldanimal);

            return this.RedirectToAction("Detail", new { id = id.Value });
        }

        [HttpGet("/Customer/Details/{id}/Animal/{action}/{animalId?}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DetailAnimal(int? id, int? animalId)
        {
            var animal = await this.animalsManager.FindByIdAsync(animalId.Value);
            if (animal == null)
            {
                return this.NotFound();
            }

            var model = new IndexAnimalsViewModel
            {
                Id = animal.Id,
                CustomerName = animal.Customers.Name,
                Email = animal.Customers.Email,
                Telephone1 = animal.Customers.Telephone1,
                Telephone2 = animal.Customers.Telephone2,
                Age = animal.Age,
                Weight = animal.Weight,
                Observations = animal.Observations,
                CreationDate = animal.CreationDate,
                DateUpdate = animal.DateUpdate,
                Name = animal.Name,
                Race = animal.Race,
                SexName = animal.Sexes.Description,
                TypeAnimalName = animal.Sexes.Description,
                UnitMeasurementName = animal.UnitMeasurements.Description + animal.Weight.ToString(),
                LastVisit = animal.LastVisit,
            };

            return this.View(model);
        }
    }
}
