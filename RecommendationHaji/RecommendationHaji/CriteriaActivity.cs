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
using static Android.App.DatePickerDialog;

namespace RecommendationHaji
{
  [Activity(Label = "CriteriaActivity", Theme = "@style/AppTheme", WindowSoftInputMode =SoftInput.AdjustResize|SoftInput.StateHidden)]
  public class CriteriaActivity : AppCompatActivity, IOnDateSetListener
  {
    EditText dateOfDepartureTxt, dateOfReturnTxt, priceTxt;
    Spinner packagesTxt;
    Button submitBtn;
    ProgressBar mProgress;

    List<KeyValuePair<string, string>> packages;
    int selectedPackages = 0;
    const int DATE_DIALOG = 1;

    int whichDate = 0;

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
        packagesTxt = FindViewById<Spinner>(Resource.Id.packagesTxt);
        priceTxt = FindViewById<EditText>(Resource.Id.priceTxt);
        submitBtn = FindViewById<Button>(Resource.Id.submitCriteriaBtn);
        mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);

        packages = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("haji", "Haji"),
            new KeyValuePair<string, string>("umroh", "Umroh")
        };

        List<string> packagesNames = new List<string>();
        foreach (var item in packages)
        {
          packagesNames.Add(item.Key);
        }

        var adapter = new ArrayAdapter<string>(this, Resource.Layout.spinner_item, packagesNames);
        adapter.SetDropDownViewResource(Resource.Layout.spinner_item);
        packagesTxt.Adapter = adapter;

        packagesTxt.SetSelection(0);

        InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
        MyGlobalClass.hideKeyboard(ref imm, ref dateOfDepartureTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref dateOfReturnTxt);
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
      dateOfDepartureTxt.Click += delegate
      {
        whichDate = 1;
        this.RemoveDialog(DATE_DIALOG);
        ShowDialog(DATE_DIALOG);
      };

      dateOfReturnTxt.Click += delegate
      {
        whichDate = 2;
        this.RemoveDialog(DATE_DIALOG);
        ShowDialog(DATE_DIALOG);
      };

      submitBtn.Click += SubmitBtn_Click;
      packagesTxt.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
    }

    protected override Dialog OnCreateDialog(int id)
    {
      switch (id)
      {
        case DATE_DIALOG:
        {
          return new DatePickerDialog(this, this, DateTime.Now.Year, (DateTime.Now.Month - 1), DateTime.Now.Day);
        }
        default:
          break;
      }
      return null;
    }

    public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
    {
      if(whichDate==1)
      {
        dateOfDepartureTxt.Text = year + "-" + (month + 1).ToString("00") + "-" + dayOfMonth.ToString("00");
      }
      else if(whichDate==2)
      {
        dateOfReturnTxt.Text = year + "-" + (month + 1).ToString("00") + "-" + dayOfMonth.ToString("00");
      }
      
    }

    private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
    {
      selectedPackages = e.Position;
    }

    private void SubmitBtn_Click(object sender, EventArgs e)
    {
      mProgress.Visibility = Android.Views.ViewStates.Visible;
      mProgress.Progress = 50;

      string dateOfDeparture, dateOfReturn, packeges, price;
      dateOfDeparture = dateOfDepartureTxt.Text;
      dateOfReturn = dateOfReturnTxt.Text;
      packeges = packagesTxt.GetItemAtPosition(selectedPackages).ToString();
      price = priceTxt.Text;

      InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      MyGlobalClass.hideKeyboard(ref imm, ref dateOfDepartureTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref dateOfReturnTxt);
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
      mProgress.Progress = 100;
      mProgress.Visibility = Android.Views.ViewStates.Invisible;
    }
  }
}