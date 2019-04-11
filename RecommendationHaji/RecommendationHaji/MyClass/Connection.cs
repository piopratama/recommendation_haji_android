using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RecommendationHaji.MyClass
{
  class Connection
  {
    const int SUCCESS = 1;
    const int NOERROR = 0;
    const int ERROR = -1;
    const string IP = "192.168.43.63";

    private HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + IP + "/github/recommendation_haji_service/service.php");

    public List<MyObjectInJson> GetTableRestaurant()
    {
      List<MyObjectInJson> listData = new List<MyObjectInJson>();
      List<MyObjectInJson> listDataResult = null;

      MyObjectInJson myObjectJson = new MyObjectInJson();

      try
      {
        myObjectJson.ObjectID = "url";
        myObjectJson.ObjectInJson = "tablemenu";

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "username";
        myObjectJson.ObjectInJson = Session.username;

        listData.Add(myObjectJson);

        var myResult = GetJSONData(ListObjectToJson(listData));

        if (myResult.ErrorCode == 0)
        {
          if (myResult.Result.Length > 0)
          {
            listDataResult = JsonConvert.DeserializeObject<List<MyObjectInJson>>(myResult.Result);

            if (listDataResult.Count > 2)
            {
              int length_data_json = listDataResult[2].ObjectInJson.ToString().Length;
              string data_json = listDataResult[2].ObjectInJson.ToString().Substring(1, length_data_json - 2);
              var x = JsonConvert.DeserializeObject<Dictionary<string, string>>(data_json);
              listDataResult[2].ObjectInJson = x;
            }
          }
        }
        else if (myResult.ErrorCode == -1)
        {
          listDataResult = new List<MyObjectInJson>();

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "key";
          myObjectJson.ObjectID = "-1";
          listDataResult.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "message";
          myObjectJson.ObjectID = myResult.ErrorMessage;
          listDataResult.Add(myObjectJson);
        }

        return listDataResult;
      }
      catch(Exception e)
      {
        listDataResult = new List<MyObjectInJson>();

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "key";
        myObjectJson.ObjectID = "-1";
        listDataResult.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "message";
        myObjectJson.ObjectID = ErrorMessage(e.Message);
        listDataResult.Add(myObjectJson);

        return listDataResult;
      }
    }

    public List<MyObjectInJson> LoginProcess(string username, string password)
    {
      List<MyObjectInJson> listData = new List<MyObjectInJson>();
      List<MyObjectInJson> listDataResult = null;

      MyObjectInJson myObjectJson = new MyObjectInJson();

      try
      {
        myObjectJson.ObjectID = "url";
        myObjectJson.ObjectInJson = "login";

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "username";
        myObjectJson.ObjectInJson = username;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "password";
        myObjectJson.ObjectInJson = password;

        listData.Add(myObjectJson);

        string resultListObj = ListObjectToJson(listData);
        var myResult = GetJSONData(resultListObj);

        if (myResult.ErrorCode == 0)
        {
          if (myResult.Result.Length > 0)
          {
            listDataResult = JsonConvert.DeserializeObject<List<MyObjectInJson>>(myResult.Result);

            if (listDataResult.Count > 2)
            {
              int length_data_json = listDataResult[2].ObjectInJson.ToString().Length;
              string data_json = listDataResult[2].ObjectInJson.ToString().Substring(1, length_data_json - 2);
              var x = JsonConvert.DeserializeObject<Dictionary<string, string>>(data_json);
              listDataResult[2].ObjectInJson = x;
            }
          }
        }
        else if(myResult.ErrorCode==-1)
        {
          listDataResult = new List<MyObjectInJson>();

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "key";
          myObjectJson.ObjectID = "-1";
          listDataResult.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "message";
          myObjectJson.ObjectID = myResult.ErrorMessage;
          listDataResult.Add(myObjectJson);
        }

        return listDataResult;
      }
      catch(Exception e)
      {
        listDataResult = new List<MyObjectInJson>();

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "key";
        myObjectJson.ObjectID = "-1";
        listDataResult.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "message";
        myObjectJson.ObjectID = ErrorMessage(e.Message);
        listDataResult.Add(myObjectJson);

        return listDataResult;
      }
    }

    public List<MyObjectInJson> CriteriaProcess(string dateOfDeparture, string dateOfReturn, string packeges, string price)
    {
      List<MyObjectInJson> listData = new List<MyObjectInJson>();
      List<MyObjectInJson> listDataResult = null;

      MyObjectInJson myObjectJson = new MyObjectInJson();

      try
      {
        myObjectJson.ObjectID = "url";
        myObjectJson.ObjectInJson = "criteria";

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "dateOfDepartureCriteria";
        myObjectJson.ObjectInJson = dateOfDeparture;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "dateOfReturnCriteria";
        myObjectJson.ObjectInJson = dateOfReturn;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "packegesCriteria";
        myObjectJson.ObjectInJson = packeges;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "priceCriteria";
        myObjectJson.ObjectInJson = price;

        listData.Add(myObjectJson);

        string resultListObj = ListObjectToJson(listData);
        var myResult = GetJSONData(resultListObj);

        if (myResult.ErrorCode == 0)
        {
          if (myResult.Result.Length > 0)
          {
            listDataResult = JsonConvert.DeserializeObject<List<MyObjectInJson>>(myResult.Result);
            if (listDataResult.Count > 2)
            {
              List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
              Dictionary<string, string> myDictionary = new Dictionary<string, string>();
              JArray x = JsonConvert.DeserializeObject<JArray>(listDataResult[2].ObjectInJson.ToString());
              foreach (JObject content in x.Children<JObject>())
              {
                foreach (JProperty prop in content.Properties())
                {
                  myDictionary.Add(prop.Name, prop.Value.ToString());
                }
                data.Add(myDictionary);
                myDictionary = new Dictionary<string, string>();
              }
              listDataResult[2].ObjectInJson = data;
            }
          }
        }
        else if (myResult.ErrorCode == -1)
        {
          listDataResult = new List<MyObjectInJson>();

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "key";
          myObjectJson.ObjectID = "-1";
          listDataResult.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "message";
          myObjectJson.ObjectID = myResult.ErrorMessage;
          listDataResult.Add(myObjectJson);
        }

        return listDataResult;
      }
      catch (Exception e)
      {
        listDataResult = new List<MyObjectInJson>();

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "key";
        myObjectJson.ObjectID = "-1";
        listDataResult.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "message";
        myObjectJson.ObjectID = ErrorMessage(e.Message);
        listDataResult.Add(myObjectJson);

        return listDataResult;
      }
    }

    public List<MyObjectInJson> StatusBookingProcess()
    {
      List<MyObjectInJson> listData = new List<MyObjectInJson>();
      List<MyObjectInJson> listDataResult = null;

      MyObjectInJson myObjectJson = new MyObjectInJson();

      try
      {
        myObjectJson.ObjectID = "url";
        myObjectJson.ObjectInJson = "statusbooking";

        listData.Add(myObjectJson);
        
        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "idUserStatusBooking";
        myObjectJson.ObjectInJson = Session.id;

        listData.Add(myObjectJson);

        string resultListObj = ListObjectToJson(listData);
        var myResult = GetJSONData(resultListObj);

        if (myResult.ErrorCode == 0)
        {
          if (myResult.Result.Length > 0)
          {
            listDataResult = JsonConvert.DeserializeObject<List<MyObjectInJson>>(myResult.Result);
            if (listDataResult.Count > 2)
            {
              List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
              Dictionary<string, string> myDictionary = new Dictionary<string, string>();
              JArray x = JsonConvert.DeserializeObject<JArray>(listDataResult[2].ObjectInJson.ToString());
              foreach (JObject content in x.Children<JObject>())
              {
                foreach (JProperty prop in content.Properties())
                {
                  myDictionary.Add(prop.Name, prop.Value.ToString());
                }
                data.Add(myDictionary);
                myDictionary = new Dictionary<string, string>();
              }
              listDataResult[2].ObjectInJson = data;
            }
          }
        }
        else if (myResult.ErrorCode == -1)
        {
          listDataResult = new List<MyObjectInJson>();

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "key";
          myObjectJson.ObjectID = "-1";
          listDataResult.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "message";
          myObjectJson.ObjectID = myResult.ErrorMessage;
          listDataResult.Add(myObjectJson);
        }

        return listDataResult;
      }
      catch (Exception e)
      {
        listDataResult = new List<MyObjectInJson>();

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "key";
        myObjectJson.ObjectID = "-1";
        listDataResult.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "message";
        myObjectJson.ObjectID = ErrorMessage(e.Message);
        listDataResult.Add(myObjectJson);

        return listDataResult;
      }
    }

    public List<MyObjectInJson> UserRegistration(string fullName, string username, string password, string email, string address, string phoneNumber, string imageUser)
    {
      List<MyObjectInJson> listData = new List<MyObjectInJson>();
      List<MyObjectInJson> listDataResult = null;

      MyObjectInJson myObjectJson = new MyObjectInJson();

      try
      {
        myObjectJson.ObjectID = "url";
        myObjectJson.ObjectInJson = "registrationUser";

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "fullNameRegistration";
        myObjectJson.ObjectInJson = fullName;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "usernameRegistration";
        myObjectJson.ObjectInJson = username;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "passwordRegistration";
        myObjectJson.ObjectInJson = password;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "emailRegistration";
        myObjectJson.ObjectInJson = email;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "addressRegistration";
        myObjectJson.ObjectInJson = address;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "phoneNumberRegistration";
        myObjectJson.ObjectInJson = phoneNumber;

        listData.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "imageRegistration";
        myObjectJson.ObjectInJson = imageUser;

        listData.Add(myObjectJson);

        string resultListObj = ListObjectToJson(listData);
        var myResult = GetJSONData(resultListObj);

        if (myResult.ErrorCode == 0)
        {
          if (myResult.Result.Length > 0)
          {
            listDataResult = JsonConvert.DeserializeObject<List<MyObjectInJson>>(myResult.Result);

            if (listDataResult.Count > 2)
            {
              int length_data_json = listDataResult[2].ObjectInJson.ToString().Length;
              string data_json = listDataResult[2].ObjectInJson.ToString().Substring(1, length_data_json - 2);
              var x = JsonConvert.DeserializeObject<Dictionary<string, string>>(data_json);
              listDataResult[2].ObjectInJson = x;
            }
          }
        }
        else if (myResult.ErrorCode == -1)
        {
          listDataResult = new List<MyObjectInJson>();

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "key";
          myObjectJson.ObjectID = "-1";
          listDataResult.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "message";
          myObjectJson.ObjectID = myResult.ErrorMessage;
          listDataResult.Add(myObjectJson);
        }

        return listDataResult;
      }
      catch (Exception e)
      {
        listDataResult = new List<MyObjectInJson>();

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "key";
        myObjectJson.ObjectID = "-1";
        listDataResult.Add(myObjectJson);

        myObjectJson = new MyObjectInJson();
        myObjectJson.ObjectID = "message";
        myObjectJson.ObjectID = ErrorMessage(e.Message);
        listDataResult.Add(myObjectJson);

        return listDataResult;
      }
    }

    public List<MyObjectInJson> finishBookingProcess(List<List<string>>dataList)
    {
      List<List<MyObjectInJson>> listData = new List<List<MyObjectInJson>>();
      List<MyObjectInJson> mListData = new List<MyObjectInJson>();
      List<List<MyObjectInJson>> listDataResult = new List<List<MyObjectInJson>>();
      List<MyObjectInJson> mListDataResult = null;
      string dataOut = dataList[0][0];
      MyObjectInJson myObjectJson = new MyObjectInJson();

      try
      {
        myObjectJson.ObjectID = "url";
        myObjectJson.ObjectInJson = "finishBooking";
        mListData.Add(myObjectJson);
        //listData.Add(mListData);

        for (int i=0; i < 2; i++)
        
        {
          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "fullNameFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][0];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "addressFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][1];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "emailFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][2];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "phoneFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][3];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "imageFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][4];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "idUserFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][5];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "idPacketFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][6];

          mListData.Add(myObjectJson);

          myObjectJson = new MyObjectInJson();
          myObjectJson.ObjectID = "descriptionFinishBooking";
          myObjectJson.ObjectInJson = dataList[i][7];

          mListData.Add(myObjectJson);

          listData.Add(mListData);
          mListData = new List<MyObjectInJson>();
        }

        string resultListObj = ListObjectToJson(listData);
        var myResult = GetJSONData(resultListObj);

        //if (myResult.ErrorCode == 0)
          //{
          //  if (myResult.Result.Length > 0)
          //  {
          //    listDataResult = JsonConvert.DeserializeObject<List<MyObjectInJson>>(myResult.Result);

          //    if (listDataResult.Count > 2)
          //    {
          //      int length_data_json = listDataResult[2].ObjectInJson.ToString().Length;
          //      string data_json = listDataResult[2].ObjectInJson.ToString().Substring(1, length_data_json - 2);
          //      var x = JsonConvert.DeserializeObject<Dictionary<string, string>>(data_json);
          //      listDataResult[2].ObjectInJson = x;
          //    }
          //  }
          //}
          //else if (myResult.ErrorCode == -1)
          //{
          //  listDataResult = new List<MyObjectInJson>();

          //  myObjectJson = new MyObjectInJson();
          //  myObjectJson.ObjectID = "key";
          //  myObjectJson.ObjectID = "-1";
          //  listDataResult.Add(myObjectJson);

          //  myObjectJson = new MyObjectInJson();
          //  myObjectJson.ObjectID = "message";
          //  myObjectJson.ObjectID = myResult.ErrorMessage;
          //  listDataResult.Add(myObjectJson);
          //}

          //return listDataResult;
          return null;
      }
      catch (Exception e)
      {
        //listDataResult = new List<MyObjectInJson>();

        //myObjectJson = new MyObjectInJson();
        //myObjectJson.ObjectID = "key";
        //myObjectJson.ObjectID = "-1";
        //listDataResult.Add(myObjectJson);

        //myObjectJson = new MyObjectInJson();
        //myObjectJson.ObjectID = "message";
        //myObjectJson.ObjectID = ErrorMessage(e.Message);
        //listDataResult.Add(myObjectJson);

        //return listDataResult;
        return null;
      }
    }

    private List<MyObjectInJson> JsonToListObject(String json)
    {
      return JsonConvert.DeserializeObject<List<MyObjectInJson>>(json);
    }

    private string ListObjectToJson(List<MyObjectInJson> listData)
    {
      return JsonConvert.SerializeObject(listData);
    }

    private string ListObjectToJson(List<List<MyObjectInJson>> listData)
    {
      return JsonConvert.SerializeObject(listData);
    }

    private MyResultObject GetJSONData(string json)
    {
      try
      {
        //+PIO 20190108 pass and get json data from the server
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = json.Length;
        request.Timeout = 5000;

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
          streamWriter.Write(json);
          streamWriter.Close();

          var httpResponse = (HttpWebResponse)request.GetResponse();
          using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
          {
            var result = streamReader.ReadToEnd();

            MyResultObject myResultObject = new MyResultObject();
            myResultObject.ErrorCode = NOERROR;
            myResultObject.ErrorMessage = "";
            myResultObject.Result = result;

            return myResultObject;
          }
        }
      }
      catch(Exception e)
      {
        MyResultObject myResultObject = new MyResultObject();
        myResultObject.ErrorCode = ERROR;
        myResultObject.ErrorMessage = ErrorMessage(e.Message);
        myResultObject.Result = "";

        return myResultObject;
      }
    }

    public string ErrorMessage(string eMessage)
    {
      //+PIO 20190108 get class and method name
      var st = new StackTrace();
      var sf = st.GetFrame(0);

      var currentMethodName = sf.GetMethod();

      return "Class: " + this.GetType().Name + "\nMethod: " + currentMethodName + "\nError: " + eMessage;
    }
  }
}