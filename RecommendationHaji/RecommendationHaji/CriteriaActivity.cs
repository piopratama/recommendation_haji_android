using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using RecommendationHaji.MyClass;

namespace RecommendationHaji
{
  [Activity(Label = "CriteriaActivity", MainLauncher = true, Theme = "@style/AppTheme", WindowSoftInputMode =SoftInput.AdjustResize|SoftInput.StateHidden)]
  public class CriteriaActivity : AppCompatActivity
  {
    EditText dateOfDepartureTxt, dateOfReturnTxt, packagesTxt, priceTxt;
    Button submitBtn;
    ProgressBar mProgress;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.CriteriaView);
      initControl();
      addEvent();
    }
    private void initControl()
    {
      try
      {
        dateOfDepartureTxt = FindViewById<EditText>(Resource.Id.DateOfDepartureTxt);
        dateOfReturnTxt = FindViewById<EditText>(Resource.Id.DateOfReturnTxt);
        packagesTxt = FindViewById<EditText>(Resource.Id.packagesTxt);
        priceTxt = FindViewById<EditText>(Resource.Id.priceTxt);
        submitBtn = FindViewById<Button>(Resource.Id.submitCriteriaBtn);
        mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);

        InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
        MyGlobalClass.hideKeyboard(ref imm, ref dateOfDepartureTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref dateOfReturnTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref packagesTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref priceTxt);
      }
      catch (Exception ex)
      {
        StackTrace st = new StackTrace();
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
      }
    }

    private void addEvent()
    {
      submitBtn.Click += SubmitBtn_Click;
    }

    private void SubmitBtn_Click(object sender, EventArgs e)
    {

      string dateOfDeparture, dateOfReturn, packeges, price;
      dateOfDeparture = dateOfDepartureTxt.Text;
      dateOfReturn = dateOfReturnTxt.Text;
      packeges = packagesTxt.Text;
      price = priceTxt.Text;

      InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      MyGlobalClass.hideKeyboard(ref imm, ref dateOfDepartureTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref dateOfReturnTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref packagesTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref priceTxt);

      List<List<String>> result = new List<List<string>>();
      List<String> tempResult = new List<string>();

      tempResult.Add(dateOfDeparture);
      tempResult.Add(dateOfReturn);
      tempResult.Add(packeges);
      tempResult.Add(price);
      result.Add(tempResult);

      Intent activity = new Intent(this, typeof(RecommendationResultActivity));
      activity.PutExtra("data", JsonConvert.SerializeObject(result));
      StartActivity(activity);


      
    }
  }
}