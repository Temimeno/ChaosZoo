using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnSpace : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            // โหลดซีนปัจจุบันอีกครั้ง
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
