using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
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
    EditText fullNameTxt, usernameTxt, passwordTxt, emailTxt, addressTxt, phoneNumberTxt;
    ImageView imageUserImg;
    Button submitBtn, imageUserBtn;
    ProgressBar mProgress;
    string bitmapString;
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
        emailTxt = FindViewById<EditText>(Resource.Id.EmailTxt);
        addressTxt = FindViewById<EditText>(Resource.Id.AddressTxt);
        phoneNumberTxt = FindViewById<EditText>(Resource.Id.PhoneNumberTxt);
        submitBtn = FindViewById<Button>(Resource.Id.SubmitBtn);
        imageUserImg = FindViewById<ImageView>(Resource.Id.imageUser);
        imageUserBtn = FindViewById<Button>(Resource.Id.imagetBtn);
        mProgress = FindViewById<ProgressBar>(Resource.Id.progressBar);

        InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
        MyGlobalClass.hideKeyboard(ref imm, ref fullNameTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref usernameTxt);
        MyGlobalClass.hideKeyboard(ref imm, ref passwordTxt);
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
      imageUserBtn.Click += ImageUserBtn_Click;
      submitBtn.Click += SubmitBtn_Click;
    }

    private void ImageUserBtn_Click(object sender, EventArgs e)
    {
      Intent intent = new Intent(MediaStore.ActionImageCapture);
      StartActivityForResult(intent, 0);
    }

    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
      base.OnActivityResult(requestCode, resultCode, data);
      if (data.Extras != null)
      {
        Bitmap bitmap = (Bitmap)data.Extras.Get("data");
        imageUserImg.SetImageBitmap(bitmap);
        using (MemoryStream m = new MemoryStream())
        {
          bitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, m);
          byte[] imageBytes = m.ToArray();
          bitmapString = Convert.ToBase64String(imageBytes);
        }
      }
    }

    private void SubmitBtn_Click(object sender, EventArgs e)
    {
      string fullName, username, password, email, address, phoneNumber, imageUser;
      fullName = fullNameTxt.Text;
      username = usernameTxt.Text;
      password = passwordTxt.Text;
      fullName = fullNameTxt.Text;
      email = emailTxt.Text;
      address = addressTxt.Text;
      phoneNumber = phoneNumberTxt.Text;
      imageUser = bitmapString;

      InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      MyGlobalClass.hideKeyboard(ref imm, ref fullNameTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref usernameTxt);
      MyGlobalClass.hideKeyboard(ref imm, ref passwordTxt);
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
            List<MyObjectInJson> UserRegistrationResult = myConnection.UserRegistration(fullName, username, password, email, address, phoneNumber, imageUser);
            if (UserRegistrationResult != null)
            {
              if (UserRegistrationResult.Count > 0)
              {
                Intent serviceIntent = new Intent(this, typeof(LoginActivity));
                this.StartActivity(serviceIntent);

                if (UserRegistrationResult[1].ObjectInJson.ToString() == "")
                {
                  if (UserRegistrationResult[0].ObjectID == "key")
                  {
                    if (UserRegistrationResult[0].ObjectInJson.ToString() == "1")
                    {
                      Dictionary<string, string> userData = (Dictionary<string, string>)UserRegistrationResult[2].ObjectInJson;
                    //Session.id_user = userData["id"];
                    //  Session.username = userData["username"];
                    //  Session.userLevel = int.Parse(userData["level"]);

                     //Intent serviceIntent = new Intent(this, typeof(LoginActivity));

                    //  To pass:
                    //  intent.putExtra("Myitem", item);

                    //  To retrieve object in second Activity
                    //  getIntent().getSerializableExtra("Myitem");

                     //this.StartActivity(serviceIntent);
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