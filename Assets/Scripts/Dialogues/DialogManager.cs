using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager> 
{

    private Queue<string> sentences;

    public Text nameText;
    public Text dialogueText;
    private GameObject npc;
    public Animator animator;
    private Interact _interact;
    public Button next;
    public GameObject DialogBox;
    public GameObject[] array;
    protected DialogManager () {} 
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject npc)
    {
        DialogBox.SetActive(true);
        print("dialogue with " + dialogue.name);
        _interact = npc.GetComponent<Interact>();
        animator.SetBool("IsOpen", true);
        next.gameObject.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        print("NEXT");
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    void EndDialogue()
    {
        DialogBox.SetActive(false);
        _interact.ReloadSpriteOnEndDialog();
        animator.SetBool("IsOpen", false);
        next.gameObject.SetActive(false);
        DialogBox.SetActive(false);
        Debug.Log("End of dialogue");
    }
    
}

