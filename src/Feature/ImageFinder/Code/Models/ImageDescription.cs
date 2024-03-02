using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ECCHackaton24.Feature.ImageFinder.Models
{
    public class ImageDescription
    {
        [IndexField("_fullpath")]
        public virtual string Path { get; set; }

        [IndexField("_group")]       
        public virtual ID ItemId { get; set; }

        [IndexField("_template")]        
        public virtual ID TemplateId { get; set; }

        [IndexField("imageDescriptionAI")]
        public string _ImageDescriptionAI { get; set; }
    }
}