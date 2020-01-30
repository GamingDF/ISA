using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParser : MonoBehaviour
{
    private char commandCharStart = '[';
    private char commandCharEnd = ']';
    private char portraitCommand = 'f';

    private string portraitName;
    private float textSpeed = 0.25f;

    public IEnumerator Parse(string original) {
        bool checkForCommand = false;
        bool checkForPortrait = false;

        foreach(char c in original) {
            //Área de recebimento de comandos
            if (c == commandCharEnd) {  //Termina o comando
                if (checkForPortrait) {
                    Debug.Log(portraitName);
                    GetComponent<DialogueController>().ChangePortrait(portraitName);
                    portraitName = null;
                    checkForPortrait = false;
                }

                checkForCommand = false;
                continue;
            }

            if (checkForCommand) {
                if(c == portraitCommand && portraitName == null) {  //Comando de retrato
                    checkForPortrait = true;
                    continue;
                }

                if (checkForPortrait) {
                    portraitName += c;
                    continue;
                }
            }

            if(c == commandCharStart) {  //Começa o comando
                checkForCommand = true;
                continue;
            }
            //Fim dos comandos

            //Processamento de texto
            GetComponent<DialogueController>().InsertChar(c);

            yield return new WaitForSeconds(textSpeed);
        }
    }
}
