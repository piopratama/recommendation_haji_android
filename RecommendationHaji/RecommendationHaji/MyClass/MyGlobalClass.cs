using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
    public static void hideKeyboard(ref InputMethodManager imm, ref EditText editText)
    {
      imm.HideSoftInputFromWindow(editText.WindowToken, 0);
    }
  }
}