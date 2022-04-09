using System.Collections.Generic;
using UnityEngine;


public class Example : MonoBehaviour
{
    public int i = 90;
    public List<MoviesData> moviesData;


    //private void Start()
    //{
    //    foreach (var item in moviesData)
    //    {
    //        item.movies.RemoveAt(0);
    //    }
    //}
}

[System.Serializable]
public class MoviesData
{
    public string actorName;
    public List<string> movies;

    public MoviesData (string actorName, List<string> movies)
    {
        this.actorName = actorName;
        this.movies = movies;
    }
}