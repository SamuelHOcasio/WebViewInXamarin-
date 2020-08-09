using System;
using JSWebView.Controls;
using JSWebView.Droid.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using System.Threading;
using Android.OS;
using System.Threading.Tasks;
using Android.Webkit;
using JSWebView.Droid.Objects;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace JSWebView.Droid.Renderes
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, global::Android.Webkit.WebView>, IWebViewDelegate
    {
        private Context context;


        public CustomWebViewRenderer(Context context) : base(context)
        {
            this.context = context;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                var webView = new global::Android.Webkit.WebView(context);
                SetNativeControl(webView);

            }
            var oldView = e.OldElement as CustomWebView;
            if (oldView != null)
            {
                oldView.EvaluateJavascript = null;

            }
            var newView = e.NewElement as CustomWebView;
            if (newView != null)
            {
                ManualResetEvent reset = new ManualResetEvent(false);
                var response = "";
                newView.EvaluateJavascript = async (js) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        System.Diagnostics.Debug.WriteLine("C# function sending javascript =>" + js);
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                        {
                            Control?.EvaluateJavascript(js, new JavascriptCallBack((r) => { response = r; reset.Set(); }));

                        }
                        else
                        {
                            Control?.LoadUrl("javascript:" + js);

                        }


                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    if (response == "null")
                    {
                        response = string.Empty;
                    }
                    return response;
                };
            }
            if(Control !=null && e.NewElement != null)
            {
                SetupControl();
            }

            Element.Source?.Load(this);

        }

        private void SetupControl()
        {
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.DomStorageEnabled = true;

            //we dont need it to make it work
            //Control.VerticalScrollBarEnabled = true;
            //Control.Settings.SetRenderPriority(WebSettings.RenderPriority.High);
            //Control.Settings.CacheMode = CacheModes.NoCache;

            //if(Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            //{
            //    Control.SetLayerType(global::Android.Views.LayerType.Hardware, null);
            //}
            //else
            //{
            //    Control.SetLayerType(global::Android.Views.LayerType.Software, null);
            //}
            Control.SetWebViewClient(new CustomWebViewClient());
            Control.SetWebChromeClient(new WebChromeClient());
            Control.AddJavascriptInterface(new CustomJavaScriptInterface(this), "CustomJavascript");
        }
        //need to use it when you use MainPageViewModel "CUSTOM_HTML" variable

        public void LoadHtml(string html, string baseUrl)
        {
            Control.LoadDataWithBaseURL(baseUrl ?? "file:///android_asset/", html, "text/html", "UTF-8", null);
        }
        //you only need this using loading url
        public void LoadUrl(string url)
        {
            Control.LoadUrl(url);
        }


        internal class JavascriptCallBack : Java.Lang.Object, IValueCallback
        {
            private Action<string> callback;

            public JavascriptCallBack(Action<string> callback)
            {
                this.callback= callback;
            }

            public void OnReceiveValue(Java.Lang.Object value)
            {
                callback?.Invoke(Convert.ToString(value));
            }
        }
    }

}
