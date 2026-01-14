using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FadeButtonEvent : MonoBehaviour
{
    public Image FadePanel;
    public string SceneName;

    float time = 0f;
    float F_time = 1f;

    public void Fade()
    {
        StartCoroutine("FadeFlow");
    }

    private IEnumerator FadeFlow()
    {
        FadePanel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = FadePanel.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            FadePanel.color = alpha;
            yield return null;
        }
        time = 0f;
        /*
        yield return new WaitForSeconds(0.3f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            FadePanel.color = alpha;
            yield return null;
        }
        FadePanel.gameObject.SetActive(false);
        */
        yield return new WaitForSeconds(0.25f);

        SceneManager.LoadScene(SceneName);
    }
}
