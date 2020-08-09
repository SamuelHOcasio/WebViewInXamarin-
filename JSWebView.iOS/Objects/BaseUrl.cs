using Xamarin.Forms;
using JSWebView.iOS.Objects;
using JSWebView.Interfaces;
using Foundation;

[assembly: Dependency(typeof(BaseUrl))]


namespace JSWebView.iOS.Objects
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath + "/www/";
        }
    }
}