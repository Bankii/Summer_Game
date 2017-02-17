using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CButtonManager : MonoBehaviour {

    public void changeScene(int aScene)
    {
        SceneManager.LoadScene(aScene);
        // TODO: add a loading image.
        if (aScene == 1)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void eraseLoad()
    {
        CSaveLoad.eraseLoad();
    }
}
