using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCloseUI : MonoBehaviour

{
    public GameObject UIpanelClose;
    public GameObject UIPanelOpen;
    public Toggle Toggle;

    private bool UIactive = false;
    void Start()
    {
        Toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            Invoke("CloseAndOpenUI", 3f);
            UIactive = true;
        }
    }
    void CloseAndOpenUI()
    {
        if (UIactive)
        {
            UIpanelClose.SetActive(false);

            UIPanelOpen.SetActive(true);
            UIactive = false;
        }
    }
}