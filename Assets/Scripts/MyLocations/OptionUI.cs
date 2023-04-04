
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionText;
    private Button thisButton;
    private DialoguePiece currentPiece;


    private bool takeQuest;
    private string nextPieceID;
    void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }

    //update the text displayed in the option
    public void UpdateOption(DialoguePiece piece, DialogueOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
      
    }


    //update the dialogue when player clicks next
    public void OnOptionClicked()
    {
       
        if (nextPieceID == "")
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);//the story ends
            return;
        }
        else if (!DialogueUI.Instance.currentData.dialogueIndex.ContainsKey(nextPieceID)) {

            DialogueUI.Instance.dialoguePanel.SetActive(false);
            return;
        }
        else {

         
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextPieceID]);
        }
        
            
        
    }

}
