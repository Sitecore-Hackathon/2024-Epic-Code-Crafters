using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECCHackaton24.Feature.ImageFinder.Models
{
    public class ImageDescription: SearchResultItem
    {
        [IndexField("_templatename")]
        public string _TemplateName { get; set; }

        [IndexField("_template")]
        public ID _TemplateID { get; set; }

        [IndexField("_fullpath")]
        public string _FullPath { get; set; }

        [IndexField("_path")]
        public IEnumerable<string> _Path { get; set; }

        [IndexField("ImageDescriptionAI")]
        public string _ImageDescriptionAI { get; set; }

    }
}