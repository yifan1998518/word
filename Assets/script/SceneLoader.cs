using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneChange(string curScene){
        SceneManager.LoadScene(curScene);
    }
}