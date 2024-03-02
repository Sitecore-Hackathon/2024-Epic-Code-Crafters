using ECCHackaton24.Feature.ImageFinder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Google.Cloud.Vision.V1.ProductSearchResults.Types;

namespace ECCHackaton24.Feature.ImageFinder.Controllers
{
    public class ImageFinderController: Controller
    {
        [HttpGet]
        public JsonResult SearchMediaLibraryImage(string searchKeywords)
        {
            ImageFinderRepository repo = new ImageFinderRepository("sitecore_web_index");
            var result = repo.SearchMediaLibraryImage(searchKeywords);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}