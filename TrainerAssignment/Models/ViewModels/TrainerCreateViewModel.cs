using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainerAssignment.DAL;

namespace TrainerAssignment.Models.ViewModels
{
    public class TrainerCreateViewModel
    {
        MyDatabase db = new MyDatabase();
        public IEnumerable<SelectListItem> SelectSubjectsIds
        {
            get
            {               
                if (Trainer is null)
                {
                    return db.Categories.ToList().Select(x => new SelectListItem()
                    {
                        Value = x.CategoryId.ToString(),
                        Text = x.Subject
                    });
                }
                else
                {
                    var categoriesId = Trainer.Categories.Select(x => x.CategoryId);
                    return db.Categories.ToList().Select(x => new SelectListItem()
                    {
                        Value = x.CategoryId.ToString(),
                        Text = x.Subject,
                        Selected = categoriesId.Any(y => y == x.CategoryId)

                    });
                }
            }

        }
        public Trainer Trainer { get; set; }
    }
}