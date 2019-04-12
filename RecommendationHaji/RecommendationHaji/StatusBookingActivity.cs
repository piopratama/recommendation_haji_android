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
using Android.Widget;
using RecommendationHaji.MyClass;

namespace RecommendationHaji
{
  [Activity(Label = "StatusBookingActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
  public class StatusBookingActivity : AppCompatActivity
  {
    ProgressBar mProgress;
    LinearLayout statusBookingParent;
    TableLayout tableLayout;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.StatusBooking);
      initControl();
      getResultStatus();
    }

    private void initControl()
    {
      try
      {
        mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);
        statusBookingParent = FindViewById<LinearLayout>(Resource.Id.statusBookingParent);
        tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
      }
      catch (Exception ex)
      {
        StackTrace st = new StackTrace();
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
      }
    }

    private void getResultStatus()
    {
      mProgress.Visibility = Android.Views.ViewStates.Visible;
      mProgress.Progress = 50;
      try
      {
        new Thread(new ThreadStart(delegate
        {
          RunOnUiThread(() =>
          {
            Connection myConnection = new Connection();
            List<MyObjectInJson> StatusResult = myConnection.StatusBookingProcess();
            if (StatusResult != null)
            {
              if (StatusResult.Count > 0)
              {
                if (StatusResult[1].ObjectInJson.ToString() == "")
                {
                  if (StatusResult[0].ObjectID == "key")
                  {
                    if (StatusResult[0].ObjectInJson.ToString() == "1")
                    {
                      List<Dictionary<string, string>> dataStatus = (List<Dictionary<string, string>>)StatusResult[2].ObjectInJson;
                      for (int i = 0; i < dataStatus.Count; i++)
                      {
                        designStatusBooking(dataStatus[i]);
                      }
                      designRow();
                    }
                    else
                    {
                      Toast.MakeText(this, StatusResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
                    }
                  }
                  else
                  {
                    Toast.MakeText(this, "No key index found", ToastLength.Short).Show();
                  }
                }
                else
                {
                  Toast.MakeText(this, StatusResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
                }
              }
              else
              {
                Toast.MakeText(this, "No data returned", ToastLength.Short).Show();
              }
            }
            else
            {
              Toast.MakeText(this, "Unknown error occured", ToastLength.Short).Show();
            }

            mProgress.Progress = 100;
            mProgress.Visibility = Android.Views.ViewStates.Invisible;
          });
        })).Start();
      }
      catch (Exception ex)
      {
        StackTrace st = new StackTrace();
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
        mProgress.Progress = 0;
        mProgress.Visibility = Android.Views.ViewStates.Invisible;
      }

    }
    private void designRow()
    {
      TableRow tableRow = new TableRow(this);
      tableRow.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);

      TextView columm = new TextView(this);
      columm.Text = "Name";
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      columm.SetPadding(0, 0, 20, 0);
      tableRow.AddView(columm, 0);

      columm = new TextView(this);
      columm.Text = "Address";
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      columm.SetPadding(0, 0, 20, 0);
      tableRow.AddView(columm, 1);

      columm = new TextView(this);
      columm.Text = "Phone";
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      columm.SetPadding(0, 0, 20, 0);
      tableRow.AddView(columm, 2);

      columm = new TextView(this);
      columm.Text = "Email";
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      columm.SetPadding(0, 0, 20, 0);
      tableRow.AddView(columm, 3);

      columm = new TextView(this);
      columm.Text = "Description";
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 4);

      columm = new TextView(this);
      columm.Text = "Status";
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      columm.SetPadding(0, 0, 20, 0);
      tableRow.AddView(columm, 5);

      tableLayout.AddView(tableRow, 0);
    }
    private void designStatusBooking(Dictionary<string, string> data)
    {
      TableRow tableRow = new TableRow(this);
      tableRow.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      
      TextView columm = new TextView(this);
      columm.Text = data["name"].ToString();
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 0);

      columm = new TextView(this);
      columm.Text = data["address"].ToString();
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 1);

      columm = new TextView(this);
      columm.Text = data["telp"].ToString();
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 2);

      columm = new TextView(this);
      columm.Text = data["email"].ToString();
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 3);

      columm = new TextView(this);
      columm.Text = data["description"].ToString();
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 4);

      columm = new TextView(this);
      columm.Text = data["status"].ToString();
      columm.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
      columm.SetTextColor(Android.Graphics.Color.Black);
      tableRow.AddView(columm, 5);



      tableLayout.AddView(tableRow, 0);
    }
  }
}