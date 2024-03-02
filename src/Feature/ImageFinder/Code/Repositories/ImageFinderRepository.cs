using ECCHackaton24.Feature.ImageFinder.Models;
using ECCHackaton24.Foundation.ServiceManager.Repository;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using System.Collections.Generic;
using System.Linq;

namespace ECCHackaton24.Feature.ImageFinder.Repositories
{
    public class ImageFinderRepository : BaseRepository
    {
        public ImageFinderRepository(string indexName) : base(indexName)
        {

        }

        /// <summary>
        /// Search Media Library Image in Solr
        /// </summary>
        /// <param name="searchKeywords"></param>
        /// <returns></returns>
        public IEnumerable<ImageDescription> SearchMediaLibraryImage(string searchKeywords)
        {
            using (var context = Index.CreateSearchContext())
            {
                ID imageTemplate = new ID("{DAF085E8-602E-43A6-8299-038FF171349F}");

                var query = PredicateBuilder.True<ImageDescription>();
                query = query.And(x => x.TemplateId == imageTemplate);

                searchKeywords = searchKeywords.ToLower().Trim();
                var queryOr = PredicateBuilder.True<ImageDescription>();

                queryOr = queryOr.Or(x => x._ImageDescriptionAI.MatchWildcard(searchKeywords));

                query = query.And(queryOr);

                var queryable = context.GetQueryable<ImageDescription>().Where(query);

                var result = queryable.GetResults();

                if (result.Hits.Any())
                {
                    var finalResult = result.Hits.Select(x => x.Document).ToList();

                    return new List<ImageDescription>(finalResult);
                }
            }
            return new List<ImageDescription>();
        }
    }
}