// <copyright file="VisitsController.cs" company="SysRC">
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
    using VET.Core.Appointments;
    using VET.Core.Customers;
    using VET.DataBase.Models;
    using VET.Site.Models.Animals;
    using VET.Site.Models.Visits;

    [Route("Visits/{Action}/{Id?}")]
    public class VisitsController : Controller
    {
        private readonly IAnimalsManager animalsManager;
        private readonly IAppointmentsManager appointmentsManager;
        private readonly ICustomersManager customersManager;

        public VisitsController(
            IAnimalsManager animalsManager,
            ICustomersManager customersManager,
            IAppointmentsManager appointmentsManager)
        {
            this.animalsManager = animalsManager;
            this.appointmentsManager = appointmentsManager;
            this.customersManager = customersManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Index()
        {
            var visit = await this.appointmentsManager.GetAllAsync();

            var query = visit.Select(d => new IndexVisitViewModel
            {
                Id = d.Id,
                CustomerName = d.Customers.Name,
                TypeAnimalName = d.Animals.TypeAnimals.Description,
                DateVisit = d.DateVisit,
                IsComplet = d.IsComplet,
                NoteFirst = d.NoteFirst,
                NoteSeconds = d.NoteSeconds,
                NoteThird = d.NoteThird,
                VisitDetail = d.VisitDetail,
            });

            return this.View(query);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create()
        {
            var model = new CreateVisitsViewModel();
            model.Animals = await this.animalsManager.GetAllAsync();
            model.Customers = await this.customersManager.GetAllAsync();
            model.DateVisit = DateTime.Now;
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create(CreateVisitsViewModel createModel)
        {
            if (createModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                createModel.Animals = await this.animalsManager.GetAllAsync();
                createModel.Customers = await this.customersManager.GetAllAsync();
                return this.View(createModel);
            }

            var customer = await this.customersManager.FindByIdAsync(createModel.CustomerId);

            if (customer == null)
            {
                return this.NotFound();
            }

            var animal = await this.animalsManager.FindByIdAsync(createModel.AnimalId);

            if (animal == null)
            {
                return this.NotFound();
            }

            var newanimal = new Appointment
            {
                NoteFirst = createModel.NoteFirst,
                VisitDetail = createModel.VisitDetail,
                IsComplet = false,
                DateVisit = createModel.DateVisit,
                AnimalId = createModel.AnimalId,
                CustomerId = createModel.CustomerId,
            };

            var result = await this.appointmentsManager.CreateAsync(newanimal);
            if (!result.Succeeded)
            {
                foreach (var kvp in result.ValidationMessages)
                {
                    foreach (var message in kvp.Value)
                    {
                        this.ModelState.AddModelError(kvp.Key, message);
                    }
                }

                createModel.Animals = await this.animalsManager.GetAllAsync();
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

            var edit = await this.appointmentsManager.FindByIdAsync(id.Value);

            if (edit == null)
            {
                return this.NotFound();
            }

            var model = new EditVisitsViewModel
            {
                IdModel = edit.Id,
                DateVisit = edit.DateVisit,
                CustomerId = edit.CustomerId,
                NoteFirst = edit.NoteFirst,
                VisitDetail = edit.VisitDetail,
                AnimalId = edit.AnimalId,
                Animals = await this.animalsManager.GetAllAsync(),
                Customers = await this.customersManager.GetAllAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(EditVisitsViewModel editModel)
        {
            if (editModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                editModel.Animals = await this.animalsManager.GetAllAsync();
                editModel.Customers = await this.customersManager.GetAllAsync();
                return this.View(editModel);
            }

            var customer = await this.customersManager.FindByIdAsync(editModel.CustomerId);

            if (customer == null)
            {
                return this.NotFound();
            }

            var animal = await this.animalsManager.FindByIdAsync(editModel.AnimalId);

            if (animal == null)
            {
                return this.NotFound();
            }

            var visitToEdit = await this.appointmentsManager.FindByIdAsync(editModel.IdModel);

            if (visitToEdit == null)
            {
                return this.NotFound();
            }

            visitToEdit.CustomerId = editModel.CustomerId;
            visitToEdit.AnimalId = editModel.AnimalId;
            visitToEdit.DateVisit = editModel.DateVisit;
            visitToEdit.VisitDetail = editModel.VisitDetail;
            visitToEdit.NoteFirst = editModel.NoteFirst;
            var editResult = await this.appointmentsManager.EditAsync(visitToEdit);

            if (!editResult.Succeeded)
            {
                foreach (var kvp in editResult.ValidationMessages)
                {
                    foreach (var message in kvp.Value)
                    {
                        this.ModelState.AddModelError(kvp.Key, message);
                    }
                }

                editModel.Animals = await this.animalsManager.GetAllAsync();
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

            var visit = await this.appointmentsManager.FindByIdAsync(id.Value);

            if (visit == null)
            {
                return this.NotFound();
            }

            await this.appointmentsManager.DeleteAsync(visit);

            return this.RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Detail(int? id)
        {
            var visit = await this.appointmentsManager.FindByIdAsync(id.Value);
            if (visit == null)
            {
                return this.NotFound();
            }

            var model = new IndexVisitViewModel
            {
                Id = visit.Id,
                CustomerName = visit.Customers.Name,
                TypeAnimalName = visit.Animals.TypeAnimals.Description,
                DateVisit = visit.DateVisit,
                IsComplet = visit.IsComplet,
                NoteFirst = visit.NoteFirst,
                NoteSeconds = visit.NoteSeconds,
                NoteThird = visit.NoteThird,
                VisitDetail = visit.VisitDetail,
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals(int? customerId)
        {
            var animals = await this.animalsManager.GetAllCustomerAsync(customerId.Value);

            return this.Json(animals.Select(c => new { Id = c.Id, Name = c.Name }));
        }

    }
}