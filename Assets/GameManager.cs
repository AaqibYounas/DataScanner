using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GameManager : MonoBehaviour
{
    public Example dataBase;
    public GameObject _resultTime;
    public GameObject _mainmenu;
    public GameObject _filmogragy;
    public GameObject _compare2;

    public QRDecodeTest deviceCameraController;

    // Result Time 
    public GameObject movieNameText;
    public Transform resultContentTransorm;
    public Text actorName;
    // 
    // Filmography
    public AutoCompleteComboBox autocompleteBox;

    /// Compare 2
    public bool isCompare;
    public Button firstActor;
    public Button secondActor;
    public string firstActorString;
    public string secondActorString;

    // Start is called before the first frame update
    void Start()
    {
        //ResultShower("Apple ", new List<string>() { "1","2","3","4","6","7" });
        //foreach (var item in dataBase.moviesData)
        //{
        //    autocompleteBox.AvailableOptions.Add(item.actorName);
        //}
        //autocompleteBox.gameObject.SetActive(true);

        //string json = JsonConvert.SerializeObject(dataBase.moviesData);
        //print(json);

    }

    public void ResultShower(string name,List<string> movies)
    {
        name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

        actorName.text = name;
        _resultTime.SetActive(true);

        foreach (Transform item in resultContentTransorm)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in movies)
        {
            if (item == "")
                break;
            GameObject movieName = Instantiate(movieNameText, resultContentTransorm);
            movieName.GetComponent<Text>().text = item;
            movieName.SetActive(true);
        }

        if(movies.Count <= 0)
        {
            print ("They have not shared a movie");
            GameObject movieName = Instantiate(movieNameText, resultContentTransorm);
            movieName.GetComponent<Text>().text = "They have not shared a movie";
            movieName.SetActive(true);
        }
    }


    public void SearchMovies(string name)
    {
        print(name);
        foreach (var item in dataBase.moviesData)
        {
            print(item.actorName);
            if (item.actorName.Equals(name,System.StringComparison.OrdinalIgnoreCase))
            {
                print("Placed");
                ResultShower(item.actorName, item.movies);
                break;
            }
        }

    }
    


    public void Home()
    {
        firstActor.GetComponentInChildren<Text>().text = "Actor 1";
        secondActor.GetComponentInChildren<Text>().text = "Actor 2";

        firstActorString = "";
        secondActorString = "";

        _mainmenu.SetActive(true);
        _resultTime.SetActive(false);
        _filmogragy.SetActive(false);
        _compare2.SetActive(false);
        isCompare = false;
        deviceCameraController.Stop();
    }
    public void Filmography()
    {
        _filmogragy.SetActive(true);
        _mainmenu.SetActive(false);
        _resultTime.SetActive(false);
        _compare2.SetActive(false);
        //deviceCameraController.Play();
       
    }
    public void Compare2()
    {
        _compare2.SetActive(true);
        _filmogragy.SetActive(false);
        _mainmenu.SetActive(false);
        _resultTime.SetActive(false);
        deviceCameraController.Stop();
        isCompare = true;
    }

    public void Actor1()
    {
        firstActorString = "me";
        Filmography();
    }
    public void Actor2()
    {
        secondActorString = "me";
        Filmography();
    }

    public bool isComparison2(string name)
    {

        if(firstActorString == "me")
        {
            firstActorString = name;
            firstActor.GetComponentInChildren<Text>().text = name;
            Compare2();
            return true;
        }
        else if (secondActorString == "me")
        {
            secondActorString = name;
            secondActor.GetComponentInChildren<Text>().text = name;
            Compare2();
            return true;
        }
        else
            return false;
    }

    public void CompareBoth ()
    {
        if (firstActorString.Length >= 3 && secondActorString.Length >=3)
        {
            List<string> firstActorList = new List<string>();
            List<string> secondActorList = new List<string>();
            
            print("firstActorString : " + firstActorString);
            print("secondActorString : " + secondActorString);

            foreach (var item in dataBase.moviesData)
            {
                //print(item.actorName);
                if (item.actorName.Equals(firstActorString, System.StringComparison.OrdinalIgnoreCase))
                {
                    print(item.actorName);
                    firstActorList = item.movies;
                }
                else if (item.actorName.Equals(secondActorString, System.StringComparison.OrdinalIgnoreCase))
                {
                    print(item.actorName);
                    secondActorList = item.movies;
                }
            }


            var result = firstActorList.Intersect(secondActorList).ToList();
            
            ResultShower(firstActorString + " | " + secondActorString, result);
            _compare2.SetActive(false);
        }
    }



    public void GetRequestCall()
    {
        StartCoroutine( ProcessRequest("https://moovees.herokuapp.com/api/movies/"));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        print("1");
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
        print("2");
            yield return request.SendWebRequest();

        print("3");
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        print("4");
        }
    }
}
