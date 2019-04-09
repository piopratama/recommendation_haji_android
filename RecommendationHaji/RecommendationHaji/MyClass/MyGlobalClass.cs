using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace RecommendationHaji.MyClass
{
  class MyGlobalClass
  {
    public static string ErrorMessage(Context ctx, ref StackTrace st, string eMessage)
    {
      //+PIO 20190108 get class and method name
      var sf = st.GetFrame(0);

      var currentMethodName = sf.GetMethod();

      return "Class: " + ctx.GetType().Name + "\nMethod: " + currentMethodName + "\nError: " + eMessage;
    }
    public static Bitmap GetImageBitmapFromUrl(string url)
    {
      Bitmap imageBitmap = null;

      using (var webClient = new WebClient())
      {
        var imageBytes = webClient.DownloadData(url);
        if (imageBytes != null && imageBytes.Length > 0)
        {
          imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
        }
      }

      return imageBitmap;
    }
    public static void hideKeyboard(ref InputMethodManager imm, ref EditText editText)
    {
      imm.HideSoftInputFromWindow(editText.WindowToken, 0);
    }
  }
}