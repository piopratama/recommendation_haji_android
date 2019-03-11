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
using RecommendationHaji.MyClass;

namespace RecommendationHaji
{
  [Activity(Label = "UserRegistrationActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
  public class UserRegistrationActivity : AppCompatActivity
  {
    EditText fullNameTxt, usernameTxt, passwordTxt, dayOfBirthTxt, emailTxt, addressTxt, phoneNumberTxt;
    Button submitBtn;
    ProgressBar mProgress;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.UserRegistration);

      initControl();

      addEvent();
    }

    private void initControl()
    {
      try
      {
        fullNameTxt = FindViewById<EditText>(Resource.Id.FullNameTxt);
        usernameTxt = FindViewById<EditText>(Resource.Id.usernameTxt);
        passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
        dayOfBirthTxt = FindViewById<EditText>(Resource.Id.DayOfBirthTxt);
        emailTxt = FindViewById<EditText>(Resource.Id.EmailTxt);
        addressTxt = FindViewById<EditText>(Resource.Id.AddressTxt);
        phoneNumberTxt = FindViewById<EditText>(Resource.Id.PhoneNumberTxt);
        submitBtn = FindViewById<Button>(Resource.Id.SubmitBtn);
        mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);

        InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
        MyGlobalClass.hideKeyboard(ref imm, ref fullNameTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref usernameTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref passwordTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref dayOfBirthTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref emailTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref addressTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref phoneNumberTxt);
      }
      catch(Exception ex)
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
      string fullName, username, password, dayOfBirth, email, address, phoneNumber;
      fullName = fullNameTxt.Text;
      username = usernameTxt.Text;
      password = passwordTxt.Text;
      fullName = fullNameTxt.Text;
      dayOfBirth = dayOfBirthTxt.Text;
      email = emailTxt.Text;
      address = addressTxt.Text;
      phoneNumber = phoneNumberTxt.Text;

      InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      MyGlobalClass.hideKeyboard(ref imm, ref fullNameTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref usernameTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref passwordTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref dayOfBirthTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref emailTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref addressTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref phoneNumberTxt);

      mProgress.Visibility = Android.Views.ViewStates.Visible;
      mProgress.Progress = 50;
      try
      {
        new Thread(new ThreadStart(delegate
        {
          RunOnUiThread(() =>
          {
            Connection myConnection = new Connection();
            List<MyObjectInJson> UserRegistrationResult = myConnection.UserRegistration(fullName, username, password, dayOfBirth, email, address, phoneNumber);
            if (UserRegistrationResult != null)
            {
              if (UserRegistrationResult.Count > 0)
              {
                if (UserRegistrationResult[1].ObjectInJson.ToString() == "")
                {
                  if (UserRegistrationResult[0].ObjectID == "key")
                  {
                    if (UserRegistrationResult[0].ObjectInJson.ToString() == "1")
                    {
                    //  Dictionary<string, string> userData = (Dictionary<string, string>)UserRegistrationResult[2].ObjectInJson;
                    //  Session.name = userData["name"];
                    //  Session.username = userData["username"];
                    //  Session.userLevel = int.Parse(userData["level"]);

                    //  Intent serviceIntent = new Intent(this, typeof(CriteriaActivity));

                    //  To pass:
                    //  intent.putExtra("Myitem", item);

                    //  To retrieve object in second Activity
                    //  getIntent().getSerializableExtra("Myitem");

                    //  this.StartActivity(serviceIntent);
                    }
                    else
                    {
                      Toast.MakeText(this, UserRegistrationResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
                    }
                  }
                  else
                  {
                    Toast.MakeText(this, "No key index found", ToastLength.Short).Show();
                  }
                }
                else
                {
                  Toast.MakeText(this, UserRegistrationResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
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