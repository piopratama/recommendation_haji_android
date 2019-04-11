using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace RecommendationHaji
{
  [Activity(Label = "MainMenuActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
  public class MainMenuActivity : AppCompatActivity
  {
    Button searchBtn, statusBookingBtn;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.MainMenu);

      searchBtn = FindViewById<Button>(Resource.Id.searchBtn);
      statusBookingBtn = FindViewById<Button>(Resource.Id.statusBookingBtn);

      searchBtn.Click += SearchBtn_Click;
      statusBookingBtn.Click += StatusBookingBtn_Click;
    }

    private void StatusBookingBtn_Click(object sender, EventArgs e)
    {
      Intent serviceIntent = new Intent(this, typeof(StatusBookingActivity));

      this.StartActivity(serviceIntent);
    }

    private void SearchBtn_Click(object sender, EventArgs e)
    {
      Intent serviceIntent = new Intent(this, typeof(CriteriaActivity));

      this.StartActivity(serviceIntent);
    }
  }
}