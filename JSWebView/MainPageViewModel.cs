using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JSWebView.Interfaces;
using Xamarin.Forms;

namespace JSWebView
{
    public class MainPageViewModel : BindableObject
    {
        private static string Custom_HTML = @"
<!DOCTYPE html>
<html>
<head>
    <script src=""js/init.js""></script>
    <script src=""https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.0.js""></script>

    <title></title>
</head>
<body>
<div>
welcome to xamarin forms from c#
</div>
<input type=""button"" value=""Eval C#"" onclick=""evalCSharp();""/>
<p>Paragragp</p>
 <div id=""ATHMovil_Checkout_Button"">

</div>
   
    
<script type=""text/javascript"">

    ATHM_Checkout = {

        env: 'sandbox',
        publicToken: 'sandboxtoken01875617264',

        timeout: 600,

        theme: 'btn',
        lang: 'en',

        total: 1.00,
        tax: 1.00,
        subtotal: 1.00,

        metadata1: 'metadata1 test',
        metadata2: 'metadata2 test',

        items: [
            {
                ""name"":""First Item"",
                ""description"":""This is a description."",
                ""quantity"":""1"",
                ""price"":"".00"",
                ""tax"":""1.00"",
                ""metadata"":""metadata test""
            }]}</script>
  <script src=""https://www.athmovil.com/api/js/v2/athmovilV2.js""></script>
  
</body>
</html>";




        public Func<string, Task<string>> EvaluateJavascript { get; set; }

        //just use the interface to call the url from the different plataforms
        public WebViewSource CustomSource
        {
            //this is for return the custom html "Custom_HTML"
            get
            {
                //this will call "file://android_asset/www/"/index.html in the Android project and
                //NSBundle.MainBundle.BundlePath + "/www/"; in the IOs project
                var root = DependencyService.Get<IBaseUrl>().Get();
                string url = $"{root}index.html";
                //string url = $"file:///android_asset/www/index.html";

                //first way to do it
                //return new HtmlWebViewSource()
                //{
                //    Html = Custom_HTML,
                //    BaseUrl = root

                //    };


                //second way to do is loading local HTML files
                return new UrlWebViewSource()
                {
                    Url = url
                };

            }
        }

        //trigger the buttons in the MainPage.xaml
        public   ICommand EvalJS
        {
           get
            {
                 return  new Command(() => { });
            }
        }
        public ICommand EvalJS2
        {
            get
            {
                return new Command(() => { });
            }
        }
    }
}
