using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentence;

    // Start is called before the first frame update
    void Start()
    {
        sentence = new Queue<string>();
    }

    public void startConvo(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentence.Clear();

        foreach(string line in dialogue.sentence)
        {
            sentence.Enqueue(line);
        }

        displayNextLine();
    }

    public void displayNextLine()
    {
        if(sentence.Count == 0)
        {
            endDialogue();
            return;
        }

        string line = sentence.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeLine(line));
    }

    IEnumerator typeLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void endDialogue()
    {
        animator.SetBool("isOpen", false);

    }
}
