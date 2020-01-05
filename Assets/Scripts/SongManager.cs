using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using NAudio;
using AudioHelpers;
using System.Linq;

public class SongManager : MonoBehaviour
{
    //audio clip is the mp3 file
    public int retryCount;
    public GameObject songButton;
    public GameObject connectButton;
    public GameObject multButton;
    public GameObject quitButton;
    public GameObject songInput;
    public GameObject audio0;
    public GameObject audio0_5;
    public GameObject audio1;
    public GameObject audio1_5;
    public GameObject audio2;
    public GameObject audio2_5;
    public GameObject audio3;
    public GameObject instantiatedAudioLogo;
    public string url = "";
    public Text theInput;
    public InputField inputField;
    public AudioSource audioSource;
    public bool goodAudioFile;
    public static SongManager Instance;

    // public Text txtLog;
    // Start is called before the first frame update

    public void SetInputValue(string valueName)
    {
        url = valueName;
    }
    void Awake()
    {
        if (Instance)
        {
            Instance.updateLogo();
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    void updateLogo()
    {
        if(audioSource.volume *3 <= 3 && audioSource.volume*3 > 2.5f)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = Instantiate(audio3);
        }
        else if (audioSource.volume * 3 <= 2.5f && audioSource.volume*3 > 2)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = Instantiate(audio2_5);
        }
        else if (audioSource.volume * 3 <= 2 && audioSource.volume * 3 > 1.5f)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = Instantiate(audio2);
        }
        else if (audioSource.volume * 3 <= 1.5f && audioSource.volume * 3 > 1)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = Instantiate(audio1_5);
        }
        else if (audioSource.volume * 3  <= 1 && audioSource.volume * 3 > 0.5f)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = Instantiate(audio1);
        }
        else if (audioSource.volume * 3 <= 1 && audioSource.volume * 3 > 0.5f)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = (audio0_5);
        }
        else if (audioSource.volume * 3 <= 0.5f && audioSource.volume * 3 > 0)
        {
            Destroy(instantiatedAudioLogo);
            instantiatedAudioLogo = Instantiate(audio0);
        }
    }
    public void DownloadVideo()
    {
        print("download video");
        url = inputField.text;
        print(url);
        String[] seperator= { "list="};
        String[] strlist3 = url.Split(seperator, 80, StringSplitOptions.None);
        url = strlist3[0];
        print(url);

        ProcessStartInfo info = new ProcessStartInfo(Application.streamingAssetsPath +"/pyDownloader.exe", Application.streamingAssetsPath +"/SongPlaylist/ " + url);
        info.CreateNoWindow = true;
        info.UseShellExecute = false;
        info.RedirectStandardOutput = true;
        Process processChild = Process.Start(info);

        while (!processChild.StandardOutput.EndOfStream)
        {
            string line = processChild.StandardOutput.ReadLine();
            print(line);
            //txtLog.text = line;
            // do something with line
        }
    }

    public void SongManagerButton()
    {

        print("song manager button");
        songButton.SetActive(false);
        multButton.SetActive(false);
        connectButton.SetActive(false);
        quitButton.SetActive(false);

        songInput.SetActive(true);
       


    }
    void Start()
    {

        //StartCoroutine(LoadAudio("cut.mp3"));
        //StartCoroutine(LoadAudio("Fulletal.mp3"));
        updateLogo();
        audioSource.Play();
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(songManagement());
    }


    private IEnumerator LoadAudio(string fileName)
    {

        var filePath = Application.streamingAssetsPath + "/SongPlaylist/" + fileName;
      // var filePath = Application.streamingAssetsPath +"/"+ fileName;
        var extension = Path.GetExtension(fileName);
       // txtLog.text = Application.streamingAssetsPath + "/SongPlaylist/" + fileName;
        print(filePath);
        switch (extension)
        {
            case ".mp3":
                // TODO: Actually run this asynchronously?
                goodAudioFile = true;
                print("the extention is mp3");
                retryCount++;
                audioSource.clip = Mp3Util.GetAudio(filePath);
                //txtLog.text = retryCount.ToString();

                break;
            case ".wav":
                goodAudioFile = true;
                yield return UnityAudioLoader(filePath, AudioType.WAV);
                break;
            default:
                goodAudioFile = false;
                print($"{extension} file type is not yet supported.");
                break;
        }
        
        yield return null;
    }
    private IEnumerator UnityAudioLoader(string filePath, AudioType audioType)
    {
        using (var musicRequest = UnityWebRequestMultimedia.GetAudioClip($"file://{filePath}", audioType))
        {
            yield return musicRequest.SendWebRequest();

            if (musicRequest.isHttpError || musicRequest.isNetworkError)
            {
                print(musicRequest.error);
            }
            else
            {
                audioSource.clip = DownloadHandlerAudioClip.GetContent(musicRequest);
            }
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            audioSource.volume = audioSource.volume + 0.01f;
            updateLogo();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            audioSource.volume = audioSource.volume - 0.01f;
            updateLogo();
        }

    }

    public IEnumerator songManagement()
    {

        while (true)
        {

        
        var filesName = System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/SongPlaylist");
        var list = filesName.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, list.Count);
            var temp = filesName[rand];
            filesName[rand] = filesName[i];
            filesName[i] = temp;
        }

        foreach (string file in filesName)
            {
                print(file);
                char[] myChar = { '/' };
                String[] strlist = file.Split(myChar, 80, StringSplitOptions.None);

                string newString = strlist[strlist.Length - 1];

                
                char[] myChar3 = { '\\' };
                String[] strlist3 = newString.Split(myChar3, 80, StringSplitOptions.None);
                string songName = strlist3[strlist3.Length - 1];


                StartCoroutine(LoadAudio(songName));
                if (goodAudioFile)
                {
                    audioSource.Play();

                    yield return new WaitForSeconds(audioSource.clip.length);

                }

                // audioSource.PlayScheduled(audioSource.clip.length);



            

            }
            //}
        }
    }

}
