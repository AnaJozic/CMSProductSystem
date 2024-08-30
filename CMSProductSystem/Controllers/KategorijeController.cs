using CMSProductSystem.Models;
using CMSProductSystem.ModelsDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMSProductSystem.Controllers
{
    public class KategorijeController : Controller
    {
        CmsproductSystemContext _db = new CmsproductSystemContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kategorije(int page)
        {
            List<Kategorija> model = (from k in _db.Kategorija select k).ToList();

            double BrojRedaka = model.Count;
            int BrojStranica = (int)Math.Ceiling(BrojRedaka / 5);

            ViewBag.BrojStranica = BrojStranica;
            int aktivna;

            if (page == 0 || page == 1)
            {
                model = model.Take(5).ToList();
                aktivna = 1;
            }
            else
            {
                model = model.Skip((page - 1) * 5).Take(5).ToList();
                aktivna = page;
            }
            ViewBag.Aktivna = aktivna;

            return View(model);
        }
        [Authorize(Roles = "Administratori")]
        public IActionResult CreateKategorija()
        {
            Kategorija model = new Kategorija();
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        public IActionResult CreateKategorija(Kategorija model)
        {
            if (ModelState.IsValid)
            {
                _db.Kategorija.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Kategorije");
            }
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult EditKategorija(int id)
        {
            Kategorija model = (from k in _db.Kategorija
                              where k.ID.Equals(id)
                              select k).Single();
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        [HttpPost]
        public IActionResult EditKategorija(Kategorija model)
        {
            if (ModelState.IsValid)
            {
                Kategorija modelDb = (from k in _db.Kategorija
                                    where k.ID.Equals(model.ID)
                                    select k).Single();

                modelDb.Naziv = model.Naziv;


                _db.SaveChanges();
                return RedirectToAction("Kategorije");
            }
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult DeleteKategorija(int id)
        {
            Kategorija model = (from k in _db.Kategorija
                                  where k.ID.Equals(id)
                                  select k).Single();
            return View(model);
        }

        [Authorize(Roles = "Administratori")]
        public IActionResult DeleteKategorijaConfirm(int id)
        {
            Kategorija model = (from k in _db.Kategorija
                                where k.ID.Equals(id)
                                select k).Single();
            _db.Remove(model);
            _db.SaveChanges();
            TempData["poruka"] = "Kategorija uspješno izbrisana";
            return RedirectToAction("Kategorije");
        }

    }
}
