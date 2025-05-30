using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
     private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();

            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];

            }


        }
    }

   public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());


    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {

            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }


    }

    public void NextLine()
    {
        if (index < lines.Length-1 )
        {
            index++;
            textComponent.text += string.Empty;
            StartCoroutine (TypeLine());

        }
        else
        {
            gameObject.SetActive(false);

        }

    }

    
}
