
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolManager;
public class DialogueUI : Singleton<DialogueUI>
{
    [Header("Basic Elements")]
    public Image icon;
    public Text mainText;
    public Button nextButton;

    public GameObject dialoguePanel;

    [Header("Data")]
    public DialougeData_SO currentData;

    int currentIndex = 0;

    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;

    

   public  void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        nextButton.onClick.AddListener(ContinueDialogue);
        ContinueDialogue();
    }

    
    void ContinueDialogue()
    {
        if (currentIndex < currentData.dialoguePieces.Count)
        {
            UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);
        }
        else
        {
            dialoguePanel.SetActive(false);
          
        }
    }

    
    public void UpdateDialogueData(DialougeData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }


    

    public void UpdateMainDialogue(DialoguePiece piece)
    {
        if (piece == null) {
            dialoguePanel.SetActive(false);
            return;
        }
        dialoguePanel.SetActive(true);
        if(piece.targetID!="")
        currentIndex=int.Parse(piece.targetID);
      
        if (piece.image != null)
        {
            icon.enabled = true;
            icon.sprite = piece.image;
        }
        else icon.enabled = false;

        mainText.text = piece.text;
      

      
        if (piece.options.Count == 0 && currentData.dialoguePieces.Count > 0)
        {
            nextButton.interactable = true;
            nextButton.gameObject.SetActive(true);
            //currentIndex++;
        }
        else
        {
            nextButton.gameObject.SetActive(false);
         //   nextButton.transform.GetChild(0).gameObject.SetActive(false);
            nextButton.interactable = false;
        }
        CreateOptions(piece);
    }


    
    void CreateOptions(DialoguePiece piece)
    {
        if (optionPanel.childCount > 0)
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

      
        for (int i = 0; i < piece.options.Count; i++)
        {
            var option = Instantiate(optionPrefab, optionPanel);
            option.UpdateOption(piece, piece.options[i]);
        }

    }

  
}
