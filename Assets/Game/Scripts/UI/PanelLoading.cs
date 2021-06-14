using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLoading : MonoBehaviour
{
    public Image img_LoadingBar;
    public Text txt_VersionCode;

    private void Start()
    {
        txt_VersionCode.text = "v" + Application.version;
    }

    public IEnumerator StartLoading()
    {
        txt_VersionCode.gameObject.SetActive(false);
        gameObject.SetActive(true);

        float loadTimeMax = 1f;
        float loadTime = 0f;
        if (loadTime < loadTimeMax)
        {
            loadTime += Time.deltaTime;
            img_LoadingBar.fillAmount = loadTime;
        }

        yield return Yielders.Get(0.5f);

        gameObject.SetActive(false);
    }
}
