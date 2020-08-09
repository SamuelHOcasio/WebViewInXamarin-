using System;
using Android.OS;
using Android.Webkit;

namespace JSWebView.Droid.Objects
{
    public class CustomWebViewClient:WebViewClient
    {
        public CustomWebViewClient()
        {
        }
        //public override bool ShouldOverrrideUrlLoading(WebView view, IWebResourceRequest request)
        //{
        //    return base.ShouldOverrideUrlLoading(view, request);
        //}
        public override void OnPageFinished(WebView view,string url)
        {
            base.OnPageFinished(view, url);

            EvalJS(view, "alert('is working the js and html sam and is called from OnPageFinished()')");
        }
        public void EvalJS(WebView webview,string js)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                webview.EvaluateJavascript(js, null);
            }
            else
            {
                webview.LoadUrl("javascript:"+  js);
            }
        }
    }
}
