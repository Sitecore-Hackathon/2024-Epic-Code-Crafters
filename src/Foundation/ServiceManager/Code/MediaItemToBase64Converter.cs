using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.IO;

namespace ECCHackaton24.Foundation.ServiceManager
{
    public static class MediaItemToBase64Converter
    {
        /// <summary>
        /// Convert Media Item to base 64
        /// </summary>
        /// <param name="mediaItem"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ConvertMediaItemToBase64(Item mediaItem)
        {
            if (mediaItem == null)
            {
                throw new ArgumentNullException(nameof(mediaItem));
            }

            MediaItem media = new MediaItem(mediaItem);
            var stream = media.GetMediaStream();

            if (stream != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byte[] bytes = memoryStream.ToArray();

                    string base64String = Convert.ToBase64String(bytes);
                    return base64String;
                }
            }

            return null;
        }
    }
}