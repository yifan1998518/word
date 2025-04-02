using UnityEngine;
using UnityEngine.SceneManagement;  // 引入场景管理命名空间

public class SceneSwitcher : MonoBehaviour
{
    // 这个方法将在按钮点击时被调用
    public void GoToBadEnd()
    {
        // 加载名为 "badend" 的场景
        SceneManager.LoadScene("badend");
    }
}