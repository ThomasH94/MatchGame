using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public void Clicked()
    {
        //Update to take the correct scenes
        SceneManager.LoadScene("SampleScene");
    }

}
