using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace HxForms.Droid.Utils
{
    public static class ImagesHelper
    {
        public static IImageSourceHandler GetHandler(this ImageSource source)
        {
            //Image source handler to return
            IImageSourceHandler returnValue = null;
            //check the specific source type and return the correct image source handler
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }
    }
}