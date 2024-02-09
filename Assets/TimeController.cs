using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using Assembly_CSharp;
using System.Runtime.InteropServices;




public class TimeController : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
     [DllImport("__Internal")]
    private static extern void Mes(string mes);
#endif
    TMP_Text text;
 
    void Start()
    {
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
        text = GameObject.Find("TExtTime").GetComponent<TMP_Text>();
    }
 
    void GetData()
    {
        StartCoroutine(GetMoscowTime());
    }
 
    IEnumerator GetMoscowTime()
    {
        string uri = "https://worldtimeapi.org/api/timezone/Europe/Moscow";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                string jsonString = request.downloadHandler.text;
                Root root = JsonConvert.DeserializeObject<Root>(jsonString);
                text.text = root.datetime.Substring(11, 8);
#if UNITY_WEBGL && !UNITY_EDITOR
    Mes(root.datetime.Substring(11, 8));
#endif

            }
        }
    }
}