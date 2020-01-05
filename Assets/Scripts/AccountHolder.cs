using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountHolder : MonoBehaviour
{
    static readonly string rootFolder = @"C:\Program Files\Tulip\";
    //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE 
    static readonly string textFile = @"C:\Program Files\Tulip\Username.txt";
    [SerializeField]
    public Scene usernameScene;
    public object StreamWriter { get; private set; }
    public String usernameTXT;
    public InputField buttonText;
    // Start is called before the first frame update
    void log (String arg){
        Debug.Log(arg);
    }
    void Start()

    {
        string path = Application.dataPath + "/WriteText.txt";
        GameObject txtGameObject = GameObject.Find("Text2");
        Text txtComp = txtGameObject.GetComponent<Text>();
        // buttonText = GetComponentInChildren<InputField>();
        //   Debug.Log(buttonText.text);

        /*
        if (File.Exists(textFile))
        {
            // Read entire text file content in one string  
            string text = File.ReadAllText(textFile);
            Debug.Log(text);

        }
        else
        {
            SceneManager.LoadScene("UsernameListener");


        }*/
        var macAddr =
    (
        from nic in NetworkInterface.GetAllNetworkInterfaces()
        where nic.OperationalStatus == OperationalStatus.Up
        select nic.GetPhysicalAddress().ToString()
    ).FirstOrDefault();

        Debug.Log(macAddr);

    }
    /*
   void OnToggleChanged(string newText)

    {
        usernameTXT = newText;
        Debug.Log(newText);
    }*/


    // Update is called once per frame
    void Update()
    {
        
    }
    /*void OnGUI()
    {
        if (!string.Equals(SceneManager.GetActiveScene().name, "UsernameListener")) { return; }

        Event e = Event.current;
        //Debug.Log(e.keyCode);
        //Debug.Log(SceneManager.GetActiveScene().name);
        if (e.isKey)
        {
            //string path = @"C:\Program Files\Tulip\WriteText.txt";
            string path = Application.dataPath + "/WriteText.txt";
            GameObject txtGameObject = GameObject.Find("Text2");
            Text txtComp = txtGameObject.GetComponent<Text>();

            if (e.keyCode == KeyCode.Return)
            {


                if (!File.Exists(path))
                {
                    Debug.Log("file doesnt exists");

                    File.Create(path).Dispose();

                    using (TextWriter tw = new StreamWriter(path))
                    {

                        tw.WriteLine(txtComp.text);
                    }

                }
                else if (File.Exists(path))
                {
                    Debug.Log("file exists");
                    //System.IO.File.WriteAllText(path, usernameTXT);
                        
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        Debug.Log(txtComp.text);
                        tw.WriteLine(txtComp.text);
                    }
                }
                //System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", usernameTXT);

            }
        }
    }*/
}
