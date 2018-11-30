using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class ReviewController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Review
        [HttpPost]
        public ActionResult Index(Decimal id, String txtAreaReview)
        {

            if (id != null)
            {
                review review = new review();
                var username = Session["User"].ToString();
                review.gameId = id;
                review.review1 = txtAreaReview;
                review.username = username;
                review p = db.reviews.OrderByDescending(c => c.id).FirstOrDefault();
                int newId = (null == p ? 0 : p.id) + 1;
                review.id = newId;
                db.reviews.Add(review);
                db.SaveChanges();
            }

            var reviewList = db.reviews
                .Where(c => c.gameId == id);
            return View(reviewList.ToList());
        }
    }
}