using ECCHackaton24.Feature.ImageFinder.Repositories;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using System;
using System.Text;

namespace ECCHackaton24.Feature.ImageFinder.ProcessingEvents
{
    public class ProcessingEventHelper
    {
        public void UpdateImageInfo(object sender, EventArgs args)
        {

            var arguments = args as Sitecore.Publishing.Pipelines.PublishItem.ItemProcessingEventArgs;
            var db = Factory.GetDatabase("master");
            Item item = db.GetItem(arguments.Context.ItemId);

            ID imageTemplate = new ID("{DAF085E8-602E-43A6-8299-038FF171349F}");

            if (item != null)
            {
                var itemTemplate = TemplateManager.GetTemplate(item);
                var isImage = itemTemplate != null && (itemTemplate.ID == imageTemplate);

                if (isImage)
                {
                    var labels = ImageFinderRepository.GetImageLabels().GetAwaiter().GetResult();

                    StringBuilder concatenatedDescriptions = new StringBuilder();

                    foreach (var response in labels.Responses)
                    {
                        foreach (var labelAnnotation in response.LabelAnnotations)
                        {
                            concatenatedDescriptions.Append(labelAnnotation.Description);
                            concatenatedDescriptions.Append(", "); // Puedes ajustar este delimitador según tus necesidades
                        }
                    }
                    if (concatenatedDescriptions.Length >= 2)
                    {
                        concatenatedDescriptions.Length -= 2;
                    }

                    using (new Sitecore.Data.Events.EventDisabler())
                    {
                        using (new EditContext(item))
                        {
                            //PublishDateFieldName must be datetime field
                            item["ImageDescription"] = concatenatedDescriptions.ToString();                         
                            
                        }
                    }
                }
            }
        }
    }
}