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
      List<List<String>> result = new List<List<string>>();
      List<String> tempResult = new List<string>();

      tempResult.Add("1");
      tempResult.Add("x");
      result.Add(tempResult);

      tempResult = new List<string>();
      tempResult.Add("2");
      tempResult.Add("y");
      result.Add(tempResult);

      Intent activity = new Intent(this, typeof(RecommendationResultActivity));
      activity.PutExtra("data", JsonConvert.SerializeObject(result));
      StartActivity(activity);


      //string dateOfDeparture, dateOfReturn, packeges, price;
      //dateOfDeparture = dateOfDepartureTxt.Text;
      //dateOfReturn = dateOfReturnTxt.Text;
      //packeges = packagesTxt.Text;
      //price = priceTxt.Text;

      //InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      //MyGlobalClass.hideKeyboard(ref imm, ref dateOfDepartureTxt);
      //MyGlobalClass.hideKeyboard(ref imm, ref dateOfReturnTxt);
      //MyGlobalClass.hideKeyboard(ref imm, ref packagesTxt);
      //MyGlobalClass.hideKeyboard(ref imm, ref priceTxt);

      //mProgress.Visibility = Android.Views.ViewStates.Visible;
      //mProgress.Progress = 50;
      //try
      //{
      //  new Thread(new ThreadStart(delegate
      //  {
      //    RunOnUiThread(() =>
      //    {
      //      Connection myConnection = new Connection();
      //      List<MyObjectInJson> CriteriaResult = myConnection.CriteriaProcess(dateOfDeparture, dateOfReturn, packeges, price);
      //      if (CriteriaResult != null)
      //      {
      //        if (CriteriaResult.Count > 0)
      //        {
      //          if (CriteriaResult[1].ObjectInJson.ToString() == "")
      //          {
      //            if (CriteriaResult[0].ObjectID == "key")
      //            {
      //              if (CriteriaResult[0].ObjectInJson.ToString() == "1")
      //              {
      //                //  Dictionary<string, string> userData = (Dictionary<string, string>)UserRegistrationResult[2].ObjectInJson;
      //                //  Session.name = userData["name"];
      //                //  Session.username = userData["username"];
      //                //  Session.userLevel = int.Parse(userData["level"]);

      //                //  Intent serviceIntent = new Intent(this, typeof(CriteriaActivity));

      //                //  To pass:
      //                //  intent.putExtra("Myitem", item);

      //                //  To retrieve object in second Activity
      //                //  getIntent().getSerializableExtra("Myitem");

      //                //  this.StartActivity(serviceIntent);
      //              }
      //              else
      //              {
      //                Toast.MakeText(this, CriteriaResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
      //              }
      //            }
      //            else
      //            {
      //              Toast.MakeText(this, "No key index found", ToastLength.Short).Show();
      //            }
      //          }
      //          else
      //          {
      //            Toast.MakeText(this, CriteriaResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
      //          }
      //        }
      //        else
      //        {
      //          Toast.MakeText(this, "No data returned", ToastLength.Short).Show();
      //        }
      //      }
      //      else
      //      {
      //        Toast.MakeText(this, "Unknown error occured", ToastLength.Short).Show();
      //      }

      //      mProgress.Progress = 100;
      //      mProgress.Visibility = Android.Views.ViewStates.Invisible;
      //    });
      //  })).Start();
      //}
      //catch (Exception ex)
      //{
      //  StackTrace st = new StackTrace();
      //  Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
      //  mProgress.Progress = 0;
      //  mProgress.Visibility = Android.Views.ViewStates.Invisible;
      //}
    }
  }
}