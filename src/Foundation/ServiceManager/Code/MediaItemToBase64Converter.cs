using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.IO;

namespace ECCHackaton24.Foundation.ServiceManager
{
    public static class MediaItemToBase64Converter
    {
        public static string ConvertMediaItemToBase64(Item mediaItem)
        {
            if (mediaItem == null)
            {
                throw new ArgumentNullException(nameof(mediaItem));
            }

            // Obtiene el archivo asociado al item de la biblioteca de medios
            MediaItem media = new MediaItem(mediaItem);
            var stream = media.GetMediaStream();

            if (stream != null)
            {
                // Lee el contenido del archivo en un arreglo de bytes
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byte[] bytes = memoryStream.ToArray();

                    // Convierte los bytes a una cadena base64
                    string base64String = Convert.ToBase64String(bytes);
                    return base64String;
                }
            }

            return null;
        }
    }
}