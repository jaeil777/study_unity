using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    

    }
    public void OnClickHome()
    {
        SceneManager.LoadScene("UIPracticeMain");
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("UIPracticePlay");
    }


    public void OnClickGuide()
    {
        SceneManager.LoadScene("UIPracticeGuide");

    }

    public void OnClickCookie()
    {
        SceneManager.LoadScene("UIPracticeCookie");

    }
}
