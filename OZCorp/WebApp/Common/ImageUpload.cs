using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebApp.Common
{
    public class UploadLocation
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
    public static class ImageManipulation
    {
        public static void Remove(params string[] imageLocation)
        {
            foreach (var file in imageLocation)
            {
                if (File.Exists(file))
                    File.Delete(file);
                if (File.Exists(file.Replace(".","-Orig.")))
                    File.Delete(file.Replace(".", "-Orig."));
            }
        }
        public static IEnumerable<UploadLocation> ImageUpload(this IList<IFormFile> imageUpload, string webRootPath, bool optimize = true)
        {
            var uploadLocation = new List<UploadLocation>();
            var size = new MagickGeometry(200, 200)
            {
                IgnoreAspectRatio = true
            };
            foreach (var file in imageUpload)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                var guid = Guid.NewGuid();
                var newFilename = $"{guid}{Path.GetExtension(filename)}";
                var newFilenameOrig = $"{guid}-Orig{Path.GetExtension(filename)}";
                var filelocation = $@"\uploads\image\{newFilename}";
                var filelocationOrig = $@"\uploads\image\{newFilenameOrig}";


                using (var image = new MagickImage(file.OpenReadStream()))
                {
                    if (optimize)
                    {
                        image.VirtualPixelMethod = VirtualPixelMethod.Transparent;
                        image.Resize(size);
                    }
                    image.Write(webRootPath + filelocation);
                }
                if (optimize)
                    using (var image = new MagickImage(file.OpenReadStream()))
                    {
                        image.Write(webRootPath + filelocationOrig);
                    }

                uploadLocation.Add(new UploadLocation
                {
                    Name = filename,
                    Location = filelocation.Replace("\\", "/")
                });
            }
            return uploadLocation;
        }
    }
}
