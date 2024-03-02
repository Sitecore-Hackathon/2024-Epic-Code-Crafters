using ECCHackaton24.Foundation.ServiceManager.Repository;
using Google.Cloud.Vision.V1;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECCHackaton24.Foundation.ServiceManager.ComputedFields
{
    public class ImageDescriptionAI : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            ID imageTemplate = new ID("{DAF085E8-602E-43A6-8299-038FF171349F}");
            try
            {
                if (item != null &&
                    new List<ID> { imageTemplate }.Contains(item.TemplateID))
                {

                    //string imageUrl = "https://xp0.sc/-/media/Images/812311";

                    string imageUrl = MediaItemToBase64Converter.ConvertMediaItemToBase64(item);
                    
                    /*string imageUrl = "https://elements-video-cover-images-0.imgix.net/files/9aa4fccd-e239-4eb5-b814-18dfd4d7047e/inline_image_preview.jpg?auto=compress&h=630&w=1200&fit=crop&crop=edges&fm=jpeg&s=0864dace99cddf289b33a5c8a8c635d9";*/

                    var labels = BaseRepository.GetImageLabels(imageUrl).GetAwaiter().GetResult();

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

                    return concatenatedDescriptions.ToString();
                }
            }
            catch (Exception ex) { Log.Error($"Error creating computed field {this} item[{item.ID}] Message:[{ex.Message}]", this); }
            return string.Empty;
        }
    }
}