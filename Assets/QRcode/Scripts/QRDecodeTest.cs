using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TBEasyWebCam;
using System.Collections;
using UnityEngine.Assertions;

public class QRDecodeTest : MonoBehaviour
{
	public QRCodeDecodeController e_qrController;

	public Text UiText;
	public Text UiMovies;

	public GameObject resetBtn;

	public GameObject scanLineObj;
    
	public Image torchImage;

	public Example dataBase;
	public GameObject resultTime;
	/// <summary>
	/// when you set the var is true,if the result of the decode is web url,it will open with browser.
	/// </summary>
	public bool isOpenBrowserIfUrl;
	private bool waitBool = false;
	private string moviesString = null;

	public void qrScanFinished(string dataText)
	{

		if (isOpenBrowserIfUrl) {
			if (Utility.CheckIsUrlFormat(dataText))
			{
				if (!dataText.Contains("http://") && !dataText.Contains("https://"))
				{
					dataText = "http://" + dataText;
				}
				Application.OpenURL(dataText);
			}
		}
		this.UiText.text = dataText;
		moviesString = " ";
		string A;
		//dataText = dataText.ToUpper();

        Debug.Log(dataText);
		//StartCoroutine(showMoviesData());
        foreach (var item in dataBase.moviesData)
        {
			//A = item.actorName.ToUpper();
			//print(A);
			if (item.actorName.ToUpper().Contains(dataText.ToUpper()))
            {
                print("Item : " + item);
				if (!FindObjectOfType<GameManager>().isComparison2(item.actorName))
					FindObjectOfType<GameManager>().ResultShower(item.actorName, item.movies);

				waitBool = true;
				e_qrController.StopWork();

				break;
            }
        }

		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(true);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}

	}

	IEnumerator showMoviesData()
    {
		yield return new WaitUntil(() => waitBool);
		this.UiMovies.text = moviesString;
	}


	public void Reset()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.Reset();
		}

		if (this.UiText != null)
		{
			this.UiText.text = string.Empty;
		}
		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(true);
		}
	}

	public void Play()
	{
		Reset ();
		if (this.e_qrController != null)
		{
			this.e_qrController.StartWork();
		}
	}

	public void Stop()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}

		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}
	}

	public void GotoNextScene(string scenename)
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}
		//Application.LoadLevel(scenename);
		SceneManager.LoadScene(scenename);
	}
    

}
