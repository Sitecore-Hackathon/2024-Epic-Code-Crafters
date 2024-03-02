using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECCHackaton24.Feature.ImageFinder.Models
{
    public class LabelAnnotation
    {
        public string Mid { get; set; }
        public string Description { get; set; }
        public double Score { get; set; }
        public double Topicality { get; set; }
    }

    public class Response
    {
        public List<LabelAnnotation> LabelAnnotations { get; set; }
    }

    public class ApiResponse
    {
        public List<Response> Responses { get; set; }
    }
}