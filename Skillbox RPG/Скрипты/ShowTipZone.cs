using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTipZone : MonoBehaviour
{
    [SerializeField] [TextArea(3, 5)] protected string TextToType;
    [SerializeField] [Range(0, 1)] protected float delayBetweenType;
    [SerializeField] protected bool WantToType, ShowByKey, OneActuation;
    [SerializeField] protected KeyCode KeyToSHow;
    [SerializeField] protected string ShowText;
    [SerializeField] protected GameObject panelWithContent;
    [SerializeField] protected TextMeshProUGUI textTipToShow;

    private float timeToType;
    private int typeIndex;
    private string currentText;
    protected TextMeshProUGUI showText;
    private bool typing;
    protected bool canAct;
    

    protected virtual void Start()
    {
        showText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        canAct = true;

        showText.gameObject.SetActive(false);
        panelWithContent.SetActive(false);

        if(ShowByKey)
        {
            showText.text = ShowText;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && canAct)
        {
            if (ShowByKey)
            {
                showText.gameObject.SetActive(true);
                
            }
            else
            {
                panelWithContent.SetActive(true);

                if (WantToType)
                    StartShowText();
                else
                    textTipToShow.text = TextToType;

                if (OneActuation)
                    canAct = false;
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (ShowByKey && Input.GetKey(KeyToSHow) && canAct)
        {
            Debug.Log(KeyToSHow + " is pressed");
            if (OneActuation)
                canAct = false;

            if (WantToType)
            {
                if (!panelWithContent.activeSelf)
                    StartShowText();
            }
            else if (!panelWithContent.activeSelf)
            {
                panelWithContent.SetActive(true);
                textTipToShow.text = TextToType;
            }
        }
    }
    protected void StartShowText()
    {
        panelWithContent.SetActive(true);
        typeIndex = 0;
        timeToType = delayBetweenType;
        currentText = "";
        textTipToShow.text = currentText;
        typing = true;
    }

    private void Update()
    {
        if(typing && typeIndex < TextToType.Length)
        {
            timeToType -= Time.deltaTime;

            if(timeToType <= 0)
            {
                currentText += TextToType[typeIndex];

                typeIndex++;
                textTipToShow.text = currentText;

                timeToType = delayBetweenType;

                if (typeIndex >= TextToType.Length)
                    typing = false;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ShowByKey)
            {
                showText.gameObject.SetActive(false);
            }
            panelWithContent.SetActive(false);
        }
    }

}
