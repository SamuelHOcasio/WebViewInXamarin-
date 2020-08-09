using System;
using Android.Webkit;
using Java.Interop;
using Java.Lang;
using JSWebView.Droid.Renderes;

namespace JSWebView.Droid.Objects
{
    public class CustomJavaScriptInterface: Java.Lang.Object,IRunnable
    {
        readonly WeakReference<CustomWebViewRenderer> customWebViewRenderer;
        public CustomJavaScriptInterface(CustomWebViewRenderer customRenderer)
        {
            customWebViewRenderer = new WeakReference<CustomWebViewRenderer>(customRenderer);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        [Export("notify")]
        [JavascriptInterface]
        public void notify(Java.Lang.String result)
        {
            Console.WriteLine("Javascript calling c# function" + result);
        }

    }
}
