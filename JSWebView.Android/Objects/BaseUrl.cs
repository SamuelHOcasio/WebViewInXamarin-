using Xamarin.Forms;
using JSWebView.Android.Objects;
using JSWebView.Interfaces;


[assembly:Dependency(typeof(BaseUrl))]


namespace JSWebView.Android.Objects{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/www/";
           

        }
    }
}

