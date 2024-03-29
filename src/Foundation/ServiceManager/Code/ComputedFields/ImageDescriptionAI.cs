﻿using ECCHackaton24.Foundation.ServiceManager.Repository;
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
        /// <summary>
        /// Compute field to generate and index the Image Tags in Solr
        /// </summary>
        /// <param name="indexable"></param>
        /// <returns></returns>
        public override object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            ID imageTemplate = new ID("{DAF085E8-602E-43A6-8299-038FF171349F}");
            try
            {
                if (item != null && new List<ID> { imageTemplate }.Contains(item.TemplateID))
                {
                    string imageUrl = MediaItemToBase64Converter.ConvertMediaItemToBase64(item);                    

                    var labels = BaseRepository.GetImageLabels(imageUrl).GetAwaiter().GetResult();

                    StringBuilder concatenatedDescriptions = new StringBuilder();

                    foreach (var response in labels.Responses)
                    {
                        foreach (var labelAnnotation in response.LabelAnnotations)
                        {
                            concatenatedDescriptions.Append(labelAnnotation.Description);
                            concatenatedDescriptions.Append(", ");
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