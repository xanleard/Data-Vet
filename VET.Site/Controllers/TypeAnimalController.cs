// <copyright file="TypeAnimalController.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.WebSite.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VET.Core.TypeAnimals;
    using VET.DataBase.Models;
    using VET.Site.Models.TypeAnimals;

    public class TypeAnimalController : Controller
    {
        private readonly ITypeAnimalsManager typeAnimalsManager;

        public TypeAnimalController(ITypeAnimalsManager typeAnimalsManager)
        {
            this.typeAnimalsManager = typeAnimalsManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Index()
        {
            var alltypeanimal = await this.typeAnimalsManager.GetAllAsync();
            var typeanimalList = alltypeanimal.Select(l => new IndexTypeAnimalsViewModel
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
            });

            return this.View(typeanimalList);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return this.View("Create");
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create(CreateTypeAnimalsViewModel createModel)
        {
            if (createModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(createModel);
            }

            var newtypeanimal = new TypeAnimal
            {
                Name = createModel.Name,
                Description = createModel.Description,
            };

            var result = await this.typeAnimalsManager.CreateAsync(newtypeanimal);
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

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var editList = await this.typeAnimalsManager.FindByIdAsync(id.Value);

            if (editList == null)
            {
                return this.NotFound();
            }

            var list = new EditTypeAnimalsViewModel
            {
                Id = editList.Id,
                Name = editList.Name,
                Description = editList.Description,
            };

            return this.View(list);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(EditTypeAnimalsViewModel editModel)
        {
            if (editModel == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(editModel);
            }

            var typeanimalToEdit = await this.typeAnimalsManager.FindByIdAsync(editModel.Id);

            if (typeanimalToEdit == null)
            {
                return this.NotFound();
            }

            typeanimalToEdit.Description = editModel.Description;
            typeanimalToEdit.Name = editModel.Name;
            var editResult = await this.typeAnimalsManager.EditAsync(typeanimalToEdit);

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

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var oldtypeanimal = await this.typeAnimalsManager.FindByIdAsync(id.Value);

            if (oldtypeanimal == null)
            {
                return this.NotFound();
            }

            await this.typeAnimalsManager.DeleteAsync(oldtypeanimal);

            return this.RedirectToAction("Index");
        }
    }
}