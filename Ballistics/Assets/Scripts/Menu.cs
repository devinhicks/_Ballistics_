using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Development()
    {
        SceneManager.LoadScene(1);
    }

    public void Production()
    {
        SceneManager.LoadScene(2);
    }
}
