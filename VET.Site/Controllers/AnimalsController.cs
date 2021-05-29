// <copyright file="AnimalsController.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
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

    [Route("Animals/{Action}/{Id?}")]
    public class AnimalsController : Controller
    {
        private readonly ISexesManager sexesManager;
        private readonly IAnimalsManager animalsManager;
        private readonly IUnitMeasurementsManager unitMeasurementsManager;
        private readonly ITypeAnimalsManager typeAnimalsManager;
        private readonly ICustomersManager customersManager;

        public AnimalsController(
            ISexesManager sexesManager,
            IAnimalsManager animalsManager,
            ITypeAnimalsManager typeAnimalsManager,
            ICustomersManager customersManager,
            IUnitMeasurementsManager unitMeasurementsManager)
        {
            this.sexesManager = sexesManager;
            this.unitMeasurementsManager = unitMeasurementsManager;
            this.animalsManager = animalsManager;
            this.typeAnimalsManager = typeAnimalsManager;
            this.customersManager = customersManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Index()
        {

            var allanimals = await this.animalsManager.GetAllAsync();

            var query = allanimals.Select(d => new IndexAnimalsViewModel
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

            return this.View(query);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create()
        {
            var model = new CreateAnimalsViewModel();
            model.UnitMeasurements = await this.unitMeasurementsManager.GetAllAsync();
            model.TypeAnimals = await this.typeAnimalsManager.GetAllAsync();
            model.Sexes = await this.sexesManager.GetAllAsync();
            model.Customers = await this.customersManager.GetAllAsync();
            model.LastVisit = DateTime.Now;
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create(CreateAnimalsViewModel createModel)
        {
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
                IdentificationCard = createModel.IdentificationCard,
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

            return this.RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var edit = await this.animalsManager.FindByIdAsync(id.Value);

            if (edit == null)
            {
                return this.NotFound();
            }

            var model = new EditAnimalsViewModel
            {
                Id = edit.Id,
                LastVisit = edit.LastVisit,
                CustomerId = edit.CustomerId,
                IdentificationCard = edit.IdentificationCard,
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

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(EditAnimalsViewModel editModel)
        {
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

            var animalToEdit = await this.animalsManager.FindByIdAsync(editModel.Id);

            if (animalToEdit == null)
            {
                return this.NotFound();
            }

            animalToEdit.DateUpdate = DateTime.Now;
            animalToEdit.CustomerId = editModel.CustomerId;
            animalToEdit.IdentificationCard = editModel.IdentificationCard;
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

            var oldanimal = await this.animalsManager.FindByIdAsync(id.Value);

            if (oldanimal == null)
            {
                return this.NotFound();
            }

            await this.animalsManager.DeleteAsync(oldanimal);

            return this.RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Detail(int? id)
        {
            var animal = await this.animalsManager.FindByIdAsync(id.Value);
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
                IdentificationCard = animal.IdentificationCard,
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