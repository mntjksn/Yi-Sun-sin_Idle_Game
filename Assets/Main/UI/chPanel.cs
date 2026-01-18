using UnityEngine;

public class chPanel : MonoBehaviour
{
    public GameObject panel;

    public void Ch_Panel()
    {
        Time.timeScale = 1f;
        Destroy(panel);
    }
}