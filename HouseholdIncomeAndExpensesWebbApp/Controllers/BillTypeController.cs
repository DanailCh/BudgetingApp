﻿using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Core.Models.BillType;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class BillTypeController:BaseController
    {
        private readonly IBillTypeService billTypeService;
      
        public BillTypeController(
            IBillTypeService _billTypeService)
        {
            billTypeService = _billTypeService;
           
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.Id();            
            var model = await billTypeService.AllCustomBillTypesAsync(userId);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new BillTypeFormViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BillTypeFormViewModel model)
        {
            var userId = User.Id();
            

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await billTypeService.CreateCustomBillTypeAsync(model, userId);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await billTypeService.DeleteCustomBillTypeByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}
