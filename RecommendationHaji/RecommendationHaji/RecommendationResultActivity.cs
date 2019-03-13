using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using Newtonsoft.Json;
using RecommendationHaji.MyClass;

namespace RecommendationHaji
{
  [Activity(Label = "RecommendationResultActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
  public class RecommendationResultActivity : AppCompatActivity
  {
    ProgressBar mProgress;
    Button submitBtn;
    LinearLayout criteriaParent;
    ImageView logoImg;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.RecommendationResult);
      initControl();
      List<List<String>> data = JsonConvert.DeserializeObject<List<List<String>>>(Intent.GetStringExtra("data"));
      getResultRecommendation(data);
      //addEvent();
    }

    private void initControl()
    {
      try
      {
        criteriaParent = FindViewById<LinearLayout>(Resource.Id.CriteriaParent);
        logoImg = FindViewById<ImageView>(Resource.Id.LogoImg);
        mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);
      }
      catch (Exception ex)
      {
        StackTrace st = new StackTrace();
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
      }
    }

    private void designCriteriaResult(Dictionary<string,string> data)
    {
      TextView travelPlaceholder = new TextView(this);
      travelPlaceholder.Text = "Location: Bali";
      travelPlaceholder.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
      travelPlaceholder.SetTextColor(Android.Graphics.Color.Black);

      ImageView iconPlaceholder = new ImageView(this);
      iconPlaceholder.SetImageResource(Resource.Drawable.placeholder);
      iconPlaceholder.LayoutParameters = new LinearLayout.LayoutParams(120, 80);

      TextView travelHaji = new TextView(this);
      travelHaji.Text = data["id"].ToString();
      travelHaji.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
      travelHaji.SetTextColor(Android.Graphics.Color.Black);

      ImageView iconHaji = new ImageView(this);
      iconHaji.SetImageResource(Resource.Drawable.star);
      iconHaji.LayoutParameters = new LinearLayout.LayoutParams(120, 80);

      TextView travelUmroh = new TextView(this);
      travelUmroh.Text = "350 on 2018";
      travelUmroh.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
      travelUmroh.SetTextColor(Android.Graphics.Color.Black);

      ImageView iconUmroh = new ImageView(this);
      iconUmroh.SetImageResource(Resource.Drawable.star);
      iconUmroh.LayoutParameters = new LinearLayout.LayoutParams(120, 80);

      LinearLayout linearParentPlaceholder = new LinearLayout(this);
      linearParentPlaceholder.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearParentPlaceholder.Orientation = Orientation.Horizontal;
      linearParentPlaceholder.SetPadding(0, 50, 20, 0);
      linearParentPlaceholder.AddView(iconPlaceholder, 0);
      linearParentPlaceholder.AddView(travelPlaceholder, 1);

      LinearLayout linearParentHaji = new LinearLayout(this);
      linearParentHaji.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearParentHaji.Orientation = Orientation.Horizontal;
      linearParentHaji.SetPadding(0, 50, 20, 0);
      linearParentHaji.AddView(iconHaji, 0);
      linearParentHaji.AddView(travelHaji, 1);

      LinearLayout linearParentUmroh = new LinearLayout(this);
      linearParentUmroh.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearParentUmroh.Orientation = Orientation.Horizontal;
      linearParentUmroh.SetPadding(0, 50, 20, 0);
      linearParentUmroh.AddView(iconUmroh, 0);
      linearParentUmroh.AddView(travelUmroh, 1);

      LinearLayout linearInfo = new LinearLayout(this);
      linearInfo.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearInfo.Orientation = Orientation.Vertical;
      linearInfo.AddView(linearParentPlaceholder, 0);
      linearInfo.AddView(linearParentHaji, 1);
      linearInfo.AddView(linearParentUmroh, 2);

      ImageView iconTravel = new ImageView(this);
      iconTravel.SetImageResource(Resource.Drawable.home);
      iconTravel.LayoutParameters = new LinearLayout.LayoutParams(400, 300);

      LinearLayout linearParentdesc = new LinearLayout(this);
      linearParentdesc.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearParentdesc.Orientation = Orientation.Horizontal;
      linearParentdesc.AddView(iconTravel, 0);
      linearParentdesc.AddView(linearInfo, 1);

      TextView travelName = new TextView(this);
      travelName.Text = "Airlangga";
      travelName.SetTextColor(Android.Graphics.Color.Black);
      travelName.SetTextSize(Android.Util.ComplexUnitType.Pt, 15);
      travelName.SetPadding(60, 20, 20, 20);

      TextView travelPrice = new TextView(this);
      travelPrice.Text = "Rp." + "24.000.000";
      travelPrice.SetTextColor(Android.Graphics.Color.Black);
      travelPrice.SetTextSize(Android.Util.ComplexUnitType.Pt, 12);
      travelPrice.SetPadding(60, 60, 0, 20);

      LinearLayout linearParent = new LinearLayout(this);
      linearParent.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearParent.Orientation = Orientation.Vertical;
      linearParent.SetPadding(20, 20, 20, 60);
      linearParent.SetBackgroundResource(Resource.Drawable.border);
      linearParent.AddView(travelName, 0);
      linearParent.AddView(linearParentdesc, 1);
      linearParent.AddView(travelPrice, 2);
      linearParent.Click += LinearParent_Click;
      linearParent.Tag = data["id"]+"_"+data["name"];

      criteriaParent.SetPadding(60, 20, 60, 20);
      criteriaParent.AddView(linearParent, 0);
    }

    private void LinearParent_Click(object sender, EventArgs e)
    {
      LinearLayout control = (LinearLayout)sender;
      string id = control.Tag.ToString();
    }

    private void getResultRecommendation(List<List<String>> data)
    {
      //for (int i=0; i<5; i++)
      //{
      //  designCriteriaResult();
      //}

      string dateOfDeparture, dateOfReturn, packeges, price;
      dateOfDeparture = "2019-03-01";
      dateOfReturn = "2019-03-31";
      packeges = "haji";
      price = "5000";

      mProgress.Visibility = Android.Views.ViewStates.Visible;
      mProgress.Progress = 50;
      try
      {
        new Thread(new ThreadStart(delegate
        {
          RunOnUiThread(() =>
          {
            Connection myConnection = new Connection();
            List<MyObjectInJson> CriteriaResult = myConnection.CriteriaProcess(dateOfDeparture, dateOfReturn, packeges, price);
            if (CriteriaResult != null)
            {
              if (CriteriaResult.Count > 0)
              {
                if (CriteriaResult[1].ObjectInJson.ToString() == "")
                {
                  if (CriteriaResult[0].ObjectID == "key")
                  {
                    if (CriteriaResult[0].ObjectInJson.ToString() == "1")
                    {
                      List<Dictionary<string, string>> dataCriteria = (List<Dictionary<string, string>>)CriteriaResult[2].ObjectInJson;
                      for(int i=0;i<dataCriteria.Count;i++)
                      {
                        designCriteriaResult(dataCriteria[i]);
                      }
                    }
                    else
                    {
                      Toast.MakeText(this, CriteriaResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
                    }
                  }
                  else
                  {
                    Toast.MakeText(this, "No key index found", ToastLength.Short).Show();
                  }
                }
                else
                {
                  Toast.MakeText(this, CriteriaResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
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
  }
}