using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Newtonsoft.Json;
using Org.Apache.Http.Protocol;
using RecommendationHaji.MyClass;
using static System.Net.Mime.MediaTypeNames;

namespace RecommendationHaji
{
  [Activity(Label = "FinisgBookingActivity", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
  public class FinishBookingActivity : AppCompatActivity
  {
    LinearLayout bookingParent;
    Button submitBtn;
    List<ImageView> imageViewList = new List<ImageView>();
    List<EditText> listFullName = new List<EditText>();
    List<EditText> listAddress = new List<EditText>();
    List<EditText> listEmail = new List<EditText>();
    List<EditText> listPhone = new List<EditText>();
    LinearLayout linearIdentityParent;
    EditText editFullName;
    EditText editAddress;
    EditText editEmail;
    EditText editPhone;
    ImageView cameraResult;
    LinearLayout linearCameraResult;
    string bitmapString;
    string mode;
    string travelInfo;
    int pos = 2;
    int indexData = 0;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.FinishBooking);
      initControl();
      travelInfo =(Intent.GetStringExtra("rawData"));
      getResultRawData(travelInfo);
      submitBtn.Click += SubmitBtn_Click;   
    }

    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
      base.OnActivityResult(requestCode, resultCode, data);
      Bitmap bitmap = (Bitmap)data.Extras.Get("data");
      imageViewList[requestCode].SetImageBitmap(bitmap);
      using (MemoryStream m = new MemoryStream())
      {
        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, m);
        byte[] imageBytes = m.ToArray();

        bitmapString = Convert.ToBase64String(imageBytes);
      }

    }

    private void initControl()
    {
      try
      {
        bookingParent = FindViewById<LinearLayout>(Resource.Id.bookingParent);
        submitBtn = FindViewById<Button>(Resource.Id.SubmitBtn);
        
      }
      catch (Exception ex)
      {
        StackTrace st = new StackTrace();
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
      }
    }

    private void getResultRawData(string travelInfo, int mode=0)
    {
      TextView travelPlaceholder = new TextView(this);
      ImageView iconPlaceholder = new ImageView(this);
      TextView travelHaji = new TextView(this);
      ImageView iconHaji = new ImageView(this);
      TextView travelUmroh = new TextView(this);
      ImageView iconUmroh = new ImageView(this);
      LinearLayout linearParentPlaceholder = new LinearLayout(this);
      LinearLayout linearParentHaji = new LinearLayout(this);
      LinearLayout linearParentUmroh = new LinearLayout(this);
      LinearLayout linearInfo = new LinearLayout(this);
      ImageView iconTravel = new ImageView(this);
      LinearLayout linearParentdesc = new LinearLayout(this);
      TextView travelPrice = new TextView(this);
      TextView travelName = new TextView(this);
      LinearLayout linearParent = new LinearLayout(this);
      Button addGuestBtn = new Button(this);
      LinearLayout linearGuestsBtn = new LinearLayout(this);

      if(mode==0)
      {
        System.String rawData = travelInfo;
        var dataString = rawData.Split(';');

        travelPlaceholder.Text = "Departure:" + " " + dataString[1];
        travelPlaceholder.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
        travelPlaceholder.SetTextColor(Android.Graphics.Color.Black);

        iconPlaceholder.SetImageResource(Resource.Drawable.placeholder);
        iconPlaceholder.LayoutParameters = new LinearLayout.LayoutParams(110, 70);

        travelHaji.Text = "Return:" + " " + dataString[2];
        travelHaji.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
        travelHaji.SetTextColor(Android.Graphics.Color.Black);

        iconHaji.SetImageResource(Resource.Drawable.star);
        iconHaji.LayoutParameters = new LinearLayout.LayoutParams(110, 70);

        travelUmroh.Text = "Package:" + " " + dataString[9];
        travelUmroh.SetTextSize(Android.Util.ComplexUnitType.Dip, 13);
        travelUmroh.SetTextColor(Android.Graphics.Color.Black);

        iconUmroh.SetImageResource(Resource.Drawable.star);
        iconUmroh.LayoutParameters = new LinearLayout.LayoutParams(110, 70);

        linearParentPlaceholder.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearParentPlaceholder.Orientation = Orientation.Horizontal;
        linearParentPlaceholder.SetPadding(0, 50, 20, 0);
        linearParentPlaceholder.AddView(iconPlaceholder, 0);
        linearParentPlaceholder.AddView(travelPlaceholder, 1);

        linearParentHaji.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearParentHaji.Orientation = Orientation.Horizontal;
        linearParentHaji.SetPadding(0, 50, 20, 0);
        linearParentHaji.AddView(iconHaji, 0);
        linearParentHaji.AddView(travelHaji, 1);

        linearParentUmroh.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearParentUmroh.Orientation = Orientation.Horizontal;
        linearParentUmroh.SetPadding(0, 50, 20, 0);
        linearParentUmroh.AddView(iconUmroh, 0);
        linearParentUmroh.AddView(travelUmroh, 1);

        linearInfo.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearInfo.Orientation = Orientation.Vertical;
        linearInfo.AddView(linearParentPlaceholder, 0);
        linearInfo.AddView(linearParentHaji, 1);
        linearInfo.AddView(linearParentUmroh, 2);


        var imageBitmap = MyGlobalClass.GetImageBitmapFromUrl(dataString[13]);
        iconTravel.SetImageBitmap(imageBitmap);
        iconTravel.LayoutParameters = new LinearLayout.LayoutParams(380, 280);

        linearParentdesc.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearParentdesc.Orientation = Orientation.Horizontal;
        linearParentdesc.AddView(iconTravel, 0);
        linearParentdesc.AddView(linearInfo, 1);

        travelName.Text = "Airlangga";
        travelName.SetTextColor(Android.Graphics.Color.Black);
        travelName.SetTextSize(Android.Util.ComplexUnitType.Pt, 15);
        travelName.SetPadding(60, 20, 20, 20);

        travelPrice.Text = "Rp." + dataString[10];
        travelPrice.SetTextColor(Android.Graphics.Color.Black);
        travelPrice.SetTextSize(Android.Util.ComplexUnitType.Pt, 12);
        travelPrice.SetPadding(60, 60, 0, 20);

        linearParent.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearParent.Orientation = Orientation.Vertical;
        linearParent.SetPadding(20, 20, 20, 60);
        linearParent.SetBackgroundResource(Resource.Drawable.border);
        linearParent.AddView(travelName, 0);
        linearParent.AddView(linearParentdesc, 1);
        linearParent.AddView(travelPrice, 2);

        addGuestBtn.LayoutParameters = new LinearLayout.LayoutParams(400, 130);
        addGuestBtn.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
        addGuestBtn.Text = "Add Guests";
        addGuestBtn.Click += AddGuestBtn_Click;
        int send = pos;
        addGuestBtn.Tag = send;

        linearGuestsBtn.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
        linearGuestsBtn.Orientation = Orientation.Vertical;
        linearGuestsBtn.SetPadding(10, 30, 10, 0);
        linearGuestsBtn.AddView(addGuestBtn, 0);
      }

      //EditText editFullName = new EditText(this);
      editFullName = new EditText(this);
      editFullName.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      editFullName.SetPadding(10, 10, 10, 10);
      editFullName.SetBackgroundResource(Resource.Drawable.border);
      editFullName.SetTextColor(Android.Graphics.Color.Black);
      editFullName.SetHintTextColor(Android.Graphics.Color.LightGray);
      editFullName.Hint = "Full Name";
      listFullName.Add(editFullName);

      LinearLayout linearFullName = new LinearLayout(this);
      linearFullName.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearFullName.Orientation = Orientation.Vertical;
      linearFullName.SetPadding(10, 30, 10, 10);
      linearFullName.AddView(editFullName, 0);

      //EditText editAddress = new EditText(this);
      editAddress = new EditText(this);
      editAddress.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      editAddress.SetPadding(10, 10, 10, 10);
      editAddress.SetBackgroundResource(Resource.Drawable.border);
      editAddress.SetTextColor(Android.Graphics.Color.Black);
      editAddress.SetHintTextColor(Android.Graphics.Color.LightGray);
      editAddress.Hint = "Address";
      listAddress.Add(editAddress);

      LinearLayout linearAddress = new LinearLayout(this);
      linearAddress.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearAddress.Orientation = Orientation.Vertical;
      linearAddress.SetPadding(10, 30, 10, 10);
      linearAddress.AddView(editAddress, 0);

      //EditText editPhone = new EditText(this);
      editPhone = new EditText(this);
      editPhone.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      editPhone.SetPadding(10, 10, 10, 10);
      editPhone.SetBackgroundResource(Resource.Drawable.border);
      editPhone.SetTextColor(Android.Graphics.Color.Black);
      editPhone.SetHintTextColor(Android.Graphics.Color.LightGray);
      editPhone.SetRawInputType(Android.Text.InputTypes.ClassNumber);
      editPhone.Hint = "Phone Number";
      listPhone.Add(editPhone);

      LinearLayout linearPhone = new LinearLayout(this);
      linearPhone.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearPhone.Orientation = Orientation.Vertical;
      linearPhone.SetPadding(10, 30, 10, 10);
      linearPhone.AddView(editPhone, 0);

      //EditText editEmail = new EditText(this);
      editEmail = new EditText(this);
      editEmail.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      editEmail.SetPadding(10, 10, 10, 10);
      editEmail.SetBackgroundResource(Resource.Drawable.border);
      editEmail.SetTextColor(Android.Graphics.Color.Black);
      editEmail.SetHintTextColor(Android.Graphics.Color.LightGray);
      editEmail.Hint = "Email";
      listEmail.Add(editEmail);

      LinearLayout linearEmail = new LinearLayout(this);
      linearEmail.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearEmail.Orientation = Orientation.Vertical;
      linearEmail.SetPadding(10, 30, 10, 10);
      linearEmail.AddView(editEmail, 0);

      Button cameraBtn = new Button(this);
      cameraBtn.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      cameraBtn.SetBackgroundColor(Android.Graphics.Color.MediumSpringGreen);
      cameraBtn.Text = "Upload Your Identity Card";
      cameraBtn.Tag = indexData;
      cameraBtn.Click += CameraBtn_Click;
  
      LinearLayout linearCamera = new LinearLayout(this);
      linearCamera.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearCamera.Orientation = Orientation.Vertical;
      linearCamera.SetPadding(10, 30, 10, 10);
      linearCamera.AddView(cameraBtn, 0);

      //ImageView cameraResult = new ImageView(this);
      cameraResult = new ImageView(this);
      cameraResult.LayoutParameters = new LinearLayout.LayoutParams(400, 350);
      cameraResult.Visibility = Android.Views.ViewStates.Gone;
      imageViewList.Add(cameraResult);

      //LinearLayout linearCameraResult = new LinearLayout(this);
      linearCameraResult = new LinearLayout(this);
      linearCameraResult.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearCameraResult.SetPadding(10, 30, 10, 10);
      linearCameraResult.AddView(cameraResult, 0);

      Button deleteBtn = new Button(this);
      deleteBtn.Text = "delete";
      deleteBtn.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      deleteBtn.SetLinkTextColor(Android.Graphics.Color.White);
      deleteBtn.SetBackgroundColor(Android.Graphics.Color.Red);
      deleteBtn.Click += DeleteBtn_Click;

      LinearLayout linearDeleteBtn = new LinearLayout(this);
      linearDeleteBtn.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearDeleteBtn.Orientation = Orientation.Vertical;
      linearDeleteBtn.SetPadding(10, 30, 10, 10);
      linearDeleteBtn.AddView(deleteBtn, 0);

      LinearLayout linearIdentity = new LinearLayout(this);
      linearIdentity.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearIdentity.Orientation = Orientation.Vertical;
      linearIdentity.SetBackgroundResource(Resource.Drawable.border);
      linearIdentity.SetPadding(10, 60, 10, 10);
      linearIdentity.AddView(linearFullName, 0);
      linearIdentity.AddView(linearAddress, 1);
      linearIdentity.AddView(linearPhone, 2);
      linearIdentity.AddView(linearEmail, 3);
      linearIdentity.AddView(linearCameraResult, 4);
      linearIdentity.AddView(linearCamera, 5);
      linearIdentity.AddView(linearDeleteBtn, 6);

      LinearLayout linearIdentityParent = new LinearLayout(this);
      linearIdentityParent.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
      linearIdentityParent.Orientation = Orientation.Vertical;
      linearIdentityParent.SetPadding(0, 20, 0, 20);
      linearIdentityParent.AddView(linearIdentity, 0);

      bookingParent.AddView(linearParent);
      bookingParent.AddView(linearGuestsBtn);
      bookingParent.AddView(linearIdentityParent);
    }

    private void DeleteBtn_Click(object sender, EventArgs e)
    {
 
    }

    private void AddGuestBtn_Click(object sender, EventArgs e)
    {
      int mode = 1;
      indexData = indexData + 1;
      pos = pos + 1;
      getResultRawData("",mode);
    }

    private void CameraBtn_Click(object sender, EventArgs e)
    {
      cameraResult.Visibility = Android.Views.ViewStates.Visible;
      Button tagTemp = (Button)sender;
      Intent intent = new Intent(MediaStore.ActionImageCapture);
      StartActivityForResult(intent, int.Parse(tagTemp.Tag.ToString()));
    }

    private void SubmitBtn_Click(object sender, EventArgs e)
    {
      string fullName, address, email, phone, image;
      List<List<string>> dataList = new List<List<string>>();
      List<string> mData = new List<string>();

      for (int j=0; j<=indexData; j++)
      {
        mData.Add(listFullName[j].Text);
        mData.Add(listAddress[j].Text);
        mData.Add(listEmail[j].Text);
        mData.Add(listPhone[j].Text);

        dataList.Add(mData);
        mData = new List<string>();

      }

      InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
      MyGlobalClass.hideKeyboard(ref imm, ref editFullName);
      MyGlobalClass.hideKeyboard(ref imm, ref editAddress);
      MyGlobalClass.hideKeyboard(ref imm, ref editEmail);
      MyGlobalClass.hideKeyboard(ref imm, ref editPhone);

      //mProgress.Visibility = Android.Views.ViewStates.Visible;
      //mProgress.Progress = 50;
      try
      {
        new Thread(new ThreadStart(delegate
        {
          RunOnUiThread(() =>
          {
            Connection myConnection = new Connection();
            List<MyObjectInJson> loginResult = myConnection.finishBookingProcess(dataList);

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

            //mProgress.Progress = 100;
            //mProgress.Visibility = Android.Views.ViewStates.Invisible;
          });
        })).Start();
      }
      catch (Exception ex)
      {
        StackTrace st = new StackTrace();
        Toast.MakeText(this, MyGlobalClass.ErrorMessage(this, ref st, ex.Message), ToastLength.Short).Show();
        //mProgress.Progress = 0;
        //mProgress.Visibility = Android.Views.ViewStates.Invisible;
      }
    }
  }
}
