using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading;
using RecommendationHaji.MyClass;
using System.Collections.Generic;
using Android.Content;
using Android.Views.InputMethods;
using System.Diagnostics;
using System;
using Android.Views;

namespace RecommendationHaji
{
  [Activity(Label = "LoginActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
  public class LoginActivity : AppCompatActivity
  {
    EditText usernameTxt, passwordTxt;
    Button loginBtn;
    TextView createAccountBtn;
    ProgressBar mProgress;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.LoginView);

      usernameTxt = FindViewById<EditText>(Resource.Id.usernameTxt);
      passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
      loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
      createAccountBtn = FindViewById<TextView>(Resource.Id.createNewAccountBtn);
      mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);

      createAccountBtn.Click += CreateAccountBtn_Click;
      loginBtn.Click += LoginBtn_Click;

    }

    private void CreateAccountBtn_Click(object sender, EventArgs e)
    {
      Intent userAccountIntent = new Intent(this, typeof(UserRegistrationActivity));
      this.StartActivity(userAccountIntent);
    }

    private void LoginBtn_Click(object sender, System.EventArgs e)
    {
      string username, password;
      username = usernameTxt.Text;
      password = passwordTxt.Text;
      
      InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      MyGlobalClass.hideKeyboard(ref imm, ref usernameTxt);
      MyGlobalClass.hideKeyboard(ref imm ,ref passwordTxt);

      mProgress.Visibility = Android.Views.ViewStates.Visible;
      mProgress.Progress = 50;
      try
      {
        new Thread(new ThreadStart(delegate
        {
          RunOnUiThread(() =>
          {
            Connection myConnection = new Connection();
            List<MyObjectInJson> loginResult = myConnection.LoginProcess(username, password);

            if (loginResult != null)
            {
              if (loginResult.Count > 0)
              {
                if (loginResult[1].ObjectInJson.ToString() == "")
                {
                  if (loginResult[0].ObjectID == "key")
                  {
                    if (loginResult[0].ObjectInJson.ToString() == "1")
                    {
                      Dictionary<string, string> userData = (Dictionary<string, string>)loginResult[2].ObjectInJson;
                      //Session.name = userData["name"];
                      Session.id = int.Parse(userData["id"]);
                      Session.username = userData["username"];
                      Session.userLevel = int.Parse(userData["level"]);

                      Intent serviceIntent = new Intent(this, typeof(CriteriaActivity));

                      ////To pass:
                      //intent.putExtra("Myitem", item);

                      //// To retrieve object in second Activity
                      //getIntent().getSerializableExtra("Myitem");

                      this.StartActivity(serviceIntent);
                    }
                    else
                    {
                      Toast.MakeText(this, loginResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
                    }
                  }
                  else
                  {
                    Toast.MakeText(this, "No key index found", ToastLength.Short).Show();
                  }
                }
                else
                {
                  Toast.MakeText(this, loginResult[1].ObjectInJson.ToString(), ToastLength.Short).Show();
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
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this,ref st, ex.Message), ToastLength.Short).Show();
        mProgress.Progress = 0;
        mProgress.Visibility = Android.Views.ViewStates.Invisible;
      }
    }
  }
}