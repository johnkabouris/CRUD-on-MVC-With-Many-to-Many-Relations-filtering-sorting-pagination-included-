using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainerAssignment.DAL;
using TrainerAssignment.Models;
using TrainerAssignment.Models.ViewModels;
using PagedList;

namespace TrainerAssignment.Controllers
{
    public class TrainerController : Controller
    {
        private MyDatabase db = new MyDatabase();

        // GET: Trainer
        public ActionResult Index(string searchFirstName,string searchLastName,int?searchMinSalary,int? searchMaxSalary,DateTime? searchMaxDate,DateTime? searchMinDate ,string sortOrder,int?pSize,int?page)
        {
            List<Trainer> trainers = Filtering(searchFirstName, searchLastName, searchMinSalary, searchMaxSalary,searchMaxDate,searchMinDate ,sortOrder);

            //Filtering
            trainers = Filter(searchFirstName, searchLastName, searchMinSalary, searchMaxSalary,searchMaxDate,searchMinDate ,trainers);
            //Sorting
            trainers = Sorting(sortOrder, trainers);

            //pagination
            int pageSize, pageNumber;
            Pagination(pSize, page, out pageSize, out pageNumber);

            return View(trainers.ToPagedList(pageNumber, pageSize));
        }

        private static void Pagination(int? pSize, int? page, out int pageSize, out int pageNumber)
        {
            pageSize = pSize ?? 5;
            pageNumber = page ?? 1;
        }

        private static List<Trainer> Sorting(string sortOrder, List<Trainer> trainers)
        {
            switch (sortOrder)
            {
                case "FirstNameDesc": trainers = trainers.OrderByDescending(x => x.FirstName).ToList(); break;
                case "LastNameAsc": trainers = trainers.OrderBy(x => x.LastName).ToList(); break;
                case "LastNameDesc": trainers = trainers.OrderByDescending(x => x.LastName).ToList(); break;

                case "SalaryAsc": trainers = trainers.OrderBy(x => x.Salary).ToList(); break;
                case "SalaryDesc": trainers = trainers.OrderByDescending(x => x.Salary).ToList(); break;
                case "DateHireAsc":trainers = trainers.OrderBy(x => x.DateHire).ToList();break;
                case "DateHireDesc":trainers = trainers.OrderByDescending(x => x.DateHire).ToList();break;
                default: trainers = trainers.OrderBy(x => x.FirstName).ToList(); break;
            }

            return trainers;
        }

        private static List<Trainer> Filter(string searchFirstName, string searchLastName, int? searchMinSalary, int? searchMaxSalary, DateTime? searchMaxDate, DateTime? searchMinDate ,List<Trainer> trainers)
        {
            if (!String.IsNullOrEmpty(searchFirstName))
            {
                trainers = trainers.Where(x => x.FirstName.ToUpper().Contains(searchFirstName.ToUpper())).ToList();
            }
            if (!String.IsNullOrEmpty(searchLastName))
            {
                trainers = trainers.Where(x => x.LastName.ToUpper().Contains(searchLastName.ToUpper())).ToList();
            }
            if (!(searchMinSalary is null))
            {
                trainers = trainers.Where(x => x.Salary >= searchMinSalary).ToList();
            }
            if (!(searchMaxSalary is null))
            {
                trainers = trainers.Where(x => x.Salary <= searchMaxSalary).ToList();
            }
            if(!(searchMaxDate is null))
            {
                trainers = trainers.Where(x => x.DateHire >= searchMinDate).ToList();
            }
            if(!(searchMinDate is null))
            {
                trainers = trainers.Where(x => x.DateHire <= searchMaxDate).ToList();
            }

            return trainers;
        }

        private List<Trainer> Filtering(string searchFirstName, string searchLastName, int? searchMinSalary, int? searchMaxSalary,DateTime? searchMaxDate, DateTime? searchMinDate , string sortOrder)
        {
            var trainers = db.Trainers.ToList();
            ViewBag.FNSP = String.IsNullOrEmpty(sortOrder) ? "FirstNameDesc" : "";
            ViewBag.LNSP = sortOrder == "LastNameAsc" ? "LastNameDesc" : "LastNameAsc";
            ViewBag.SNSP = sortOrder == "SalaryAsc" ? "SalaryDesc" : "SalaryAsc";
            ViewBag.DNSP = sortOrder == "DateHireAsc" ? "DateHireDesc" : "DateHireAsc";

            ViewBag.CurrentFirstName = searchFirstName;
            ViewBag.CurrentLastName = searchLastName;
            ViewBag.CurrentMinSalary = searchMinSalary;
            ViewBag.CurrentMaxSalary = searchMaxSalary;
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentMaxDate = searchMaxDate;
            ViewBag.CurrentMinDate = searchMinDate;
            return trainers;
        }

        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            TrainerCreateViewModel vm = new TrainerCreateViewModel();
            return View(vm);
        }

        // POST: Trainer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId,FirstName,LastName,Salary,DateHire,IsAvailable")] Trainer trainer,IEnumerable<int> SelectSubjectsIds)
        {
            if (ModelState.IsValid)
            {
                db.Trainers.Attach(trainer);
                db.Entry(trainer).Collection("Categories").Load();
                trainer.Categories.Clear();
               if(!(SelectSubjectsIds is null))
                {
                    foreach(var id in SelectSubjectsIds)
                    {
                        Category category = db.Categories.Find(id);
                        if (trainer != null)
                        {
                            trainer.Categories.Add(category);
                        }
                    }
                }
                db.Entry(trainer).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            TrainerCreateViewModel vm = new TrainerCreateViewModel();
            return View(vm);
        }

        // GET: Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            TrainerEditViewModel vm = new TrainerEditViewModel(trainer);
            return View(vm);
        }

        // POST: Trainer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerId,FirstName,LastName,Salary,DateHire,IsAvailable")] Trainer trainer,IEnumerable<int> SelectSubjectsIds)
        {
            if (ModelState.IsValid)
            {
                db.Trainers.Attach(trainer);
                db.Entry(trainer).Collection("Categories").Load();
                trainer.Categories.Clear();
                db.SaveChanges();
                if (!(SelectSubjectsIds is null))
                {
                    foreach (var id in SelectSubjectsIds)
                    {
                        Category category = db.Categories.Find(id);
                        if (trainer != null)
                        {
                            trainer.Categories.Add(category);
                        }
                    }
                }
                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            db.Trainers.Attach(trainer);
            db.Entry(trainer).Collection("Categories").Load();
            TrainerEditViewModel vm = new TrainerEditViewModel(trainer);
            return View(vm);
        }

        // GET: Trainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = db.Trainers.Find(id);
            db.Trainers.Remove(trainer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
