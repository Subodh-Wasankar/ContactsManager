using ContactsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Text.RegularExpressions;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private IContactManagerService _service;

        public ContactController()
        {
            _service = new ContactManagerService(new ModelStateWrapper(this.ModelState));

        }

        public ContactController(IContactManagerService service)
        {
            _service = service;
        }

        //protected void ValidateContact(Contact contactToValidate)
        //{
        //    if (contactToValidate.FIRST_NAME.Trim().Length == 0)
        //        ModelState.AddModelError("FirstName", "First name is required.");
        //    if (contactToValidate.LAST_NAME.Trim().Length == 0)
        //        ModelState.AddModelError("LastName", "Last name is required.");
        //    if (contactToValidate.PHONE.Length > 0 && !Regex.IsMatch(contactToValidate.PHONE, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
        //        ModelState.AddModelError("Phone", "Invalid phone number.");
        //    if (contactToValidate.EMAIL.Length > 0 && !Regex.IsMatch(contactToValidate.EMAIL, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        //        ModelState.AddModelError("Email", "Invalid email address.");
        //}

        public ActionResult Index()
        {
            return View(_service.ListContacts());
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Contact contactToCreate)
        {
       
            // Database logic
            try
            {
                _service.CreateContact(contactToCreate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(_service.GetContact(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contact contactToEdit)
        {
            

            // Database logic
            try
            {
                _service.EditContact(contactToEdit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(_service.GetContact(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            try
            {
                _service.DeleteContact(contactToDelete);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}