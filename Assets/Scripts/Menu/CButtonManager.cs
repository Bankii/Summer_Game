using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CButtonManager : MonoBehaviour {

    public void changeScene(int aScene)
    {
        SceneManager.LoadScene(aScene);
        // TODO: add a loading image.
    }
}
