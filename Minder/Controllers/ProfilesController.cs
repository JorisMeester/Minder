﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Minder.Models;

namespace Minder.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles/Index
        public ActionResult Index()
        {
            var profiles = db.Profiles.Include(p => p.ProfilePicture);
            return View(profiles.ToList());
        }

        // GET: Profiles/Search
        public ActionResult Search()
        {
            return View();
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // GET: Profiles/Edit
        public ActionResult Edit()
        {
            return View();
        }

        // GET: Profiles
        [HttpPost]
        public ActionResult Search(ProfileSearchViewModel svm)
        {
            // TODO: ProfileResultViewModel aanpassen
            // TODO: code om te filteren op wat er in de svm staat

            var profiles = db.Profiles.Include(p => p.ProfilePicture);

            // TODO: View scaffolden
            return View(profiles.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }


        [Authorize]
        public ActionResult _EditProfilePartial()
        {
            // ingelogde user ophalen via usermanager
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            // bijbehorend profiel ophalen uit de DB
            Profile profile = db.Profiles.SingleOrDefault(p => p.User.Id == currentUser.Id);
            if (profile == null)
            {
                // is er geen profiel, dan maken we een lege
                profile = new Models.Profile();
            }

            return View(profile);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _EditProfilePartial(Profile profile, HttpPostedFileBase ImageUpload)
        {
            // server side validatie van het model object
            if (ModelState.IsValid)
            {
                // ingelogde user ophalen via usermanager
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var currentUser = manager.FindById(User.Identity.GetUserId());

                // profiel dat bij de user hoort uit de DB halen
                Profile storedProfile = db.Profiles.SingleOrDefault(p => p.User.Id == currentUser.Id);

                // geen profiel = nieuwe aanmaken en bewaren in de DB
                if (storedProfile == null)
                {
                    storedProfile = profile;
                    profile.User = currentUser;
                    db.Profiles.Add(storedProfile);
                }

                // data overzetten van geposte object naar database object
                storedProfile.Birthdate = profile.Birthdate;
                storedProfile.City = profile.City;
                storedProfile.Education = profile.Education;
                storedProfile.Ethnicity = profile.Ethnicity;
                storedProfile.Gender = profile.Gender;
                storedProfile.GenderInterests = profile.GenderInterests;
                storedProfile.Height = profile.Height;
                storedProfile.Nickname = profile.Nickname;

                // afbeelding verwerken
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    // directory aanmaken
                    var uploadPath = Path.Combine(Server.MapPath("~/Content/Uploads"), storedProfile.Id.ToString());
                    Directory.CreateDirectory(uploadPath);

                    // TODO: oude afbeelding verwijderen

                    // bestandsnaam maken
                    string fileGuid = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    string newFilename = fileGuid + extension;

                    // bestand opslaan
                    ImageUpload.SaveAs(Path.Combine(uploadPath, newFilename));

                    // opslaan in database
                    Picture pic = new Picture { Filename = newFilename };
                    storedProfile.ProfilePicture = pic;
                }

                // alle wijzigingen opslaan in de DB
                db.SaveChanges();

                return View(storedProfile);
            }
            return RedirectToAction("Details", "Profiles");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
