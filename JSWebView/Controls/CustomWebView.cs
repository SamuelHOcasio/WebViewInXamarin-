using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JSWebView.Controls
{
    public class CustomWebView:WebView
    {
        public static BindableProperty EvaluateJavascriptProperty = BindableProperty.Create(nameof(EvaluateJavascript),
            typeof(Func<string, Task<string>>),typeof(CustomWebView),null,BindingMode.OneWayToSource );
        public Func<string ,Task<string>> EvaluateJavascript
        {
            get { return (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty); }
            set { SetValue(EvaluateJavascriptProperty, value); }
        }
    }
}
