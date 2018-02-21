/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity;

/// Displays dialogue lines to the player, and sends
/// user choices back to the dialogue system.

/** Note that this is just one way of presenting the
 * dialogue to the user. The only hard requirement
 * is that you provide the RunLine, RunOptions, RunCommand
 * and DialogueComplete coroutines; what they do is up to you.
 */
public class DialogueUI : DialogueUIBehaviour
{

    /// The object that contains the dialogue and the options.
    /** This object will be enabled when conversation starts, and 
     * disabled when it ends.
     */
    //public GameObject dialogueContainer;

    /// The UI element that displays lines
    //public Text lineText;

    /// A UI element that appears after lines have finished appearing
    public GameObject continuePrompt;

    /// A delegate (ie a function-stored-in-a-variable) that
    /// we call to tell the dialogue system about what option
    /// the user selected
    private Yarn.OptionChooser SetSelectedOption;

    /// How quickly to show the text, in seconds per character
    [Tooltip("How quickly to show the text, in seconds per character")]
    public float textSpeed = 0.025f;

    /// The buttons that let the user choose an option
    public List<Button> optionButtons;

    /// Make it possible to temporarily disable the controls when
    /// dialogue is active and to restore them when dialogue ends
    public RectTransform gameControlsContainer;

    private bool runningOptions;

    private float elapsedTime;
    private float startTime;
    private float optionStartTime;

    private int stage;

    void Awake()
    {
        stage = 0;
        elapsedTime = 0f;
        startTime = Time.time;
        // Start by hiding the container, line and option buttons
       /* if (dialogueContainer != null)
            dialogueContainer.SetActive(false);

        lineText.gameObject.SetActive(false);*/

        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }

        // Hide the continue prompt if it exists
        if (continuePrompt != null)
            continuePrompt.SetActive(false);
    }

    void Start(){
        //Services.EventManager.Register<NewChatLineAdded>(NewChatLineAdded);
    }

    void Update(){
        //check time since running options
        if(runningOptions){
            if (Time.time - startTime - optionStartTime > 0)
            {
                elapsedTime = Time.time - startTime - optionStartTime;
            } else{
                elapsedTime = Time.time - optionStartTime;
            }

            if (elapsedTime > Random.Range(10f, 30f)){
                GetComponent<DialogueRunner>().Stop();
                Services.Main.chatManager.endedNodeTime = Time.time;
                runningOptions = false;
                foreach (var button in optionButtons)
                {
                    button.gameObject.SetActive(false);
                }

            }
        }


       /* if(optionButtons[0].gameObject.activeSelf){
            
        }*/
        if(Services.Main.chatManager.chatIsHidden){
            foreach (var button in optionButtons)
            {
                button.GetComponent<Image>().color = new Color(
                    button.GetComponent<Image>().color.r,
                    button.GetComponent<Image>().color.g,
                    button.GetComponent<Image>().color.b,0f);
            }
        } else{
            foreach (var button in optionButtons)
            {
                button.GetComponent<Image>().color = new Color(
                    button.GetComponent<Image>().color.r,
                    button.GetComponent<Image>().color.g,
                    button.GetComponent<Image>().color.b, 1f);
            }
        }



    }

    /// Show a line of dialogue, gradually
    public override IEnumerator RunLine(Yarn.Line line)
    {
        float timeSpent = 0f;
        string reline = line.text;
        string result = "";
        if (line.text.Contains("me: ")){
            timeSpent = 0.1f;
            result = reline.Replace("me: ", "<b>me:</b> ");
        } else if(line.text.Contains("ivory: ")){

            timeSpent = line.text.Length * Random.Range(0.05f, 0.2f);
            result = reline.Replace("ivory: ", "<b>ivory: </b>");
        }
        yield return new WaitForSeconds(timeSpent);
        // Show the text
        //  lineText.gameObject.SetActive(true);

        /*if (textSpeed > 0.0f)
        {
            // Display the line one character at a time
            var stringBuilder = new StringBuilder();

            foreach (char c in line.text)
            {
                stringBuilder.Append(c);
               // lineText.text = stringBuilder.ToString();
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            // Display the line immediately if textSpeed == 0
          //  lineText.text = line.text;
        }*/
        GameObject chatline = Instantiate(Services.Prefabs.ChatLine,
                                          Services.Prefabs.ChatLine.transform.position,
                                          Services.Prefabs.ChatLine.transform.rotation);

        //chatline.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        chatline.transform.SetParent(Services.Main.ScrollingObj.transform,false);


        chatline.GetComponent<Text>().text = result;
        if(line.text.Contains("ivory: ")){
            Services.EventManager.Fire(new NewChatLineAdded());
        }
        Canvas.ForceUpdateCanvases();
        Services.Main.ScrollingObj.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;

        Canvas.ForceUpdateCanvases();
        // Show the 'press any key' prompt when done, if we have one
        if (continuePrompt != null)
            continuePrompt.SetActive(true);

        // Wait for any user input
       /* while (Input.anyKeyDown == false)
        {
            yield return null;
        }*/

        // Hide the text and prompt
       // lineText.gameObject.SetActive(false);

        if (continuePrompt != null)
            continuePrompt.SetActive(false);

    }

    /// Show a list of options, and wait for the player to make a selection.
    public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                            Yarn.OptionChooser optionChooser)
    {
        //running options
        runningOptions = true;
        optionStartTime = Time.time;
        // Do a little bit of safety checking
        if (optionsCollection.options.Count > optionButtons.Count)
        {
            Debug.LogWarning("There are more options to present than there are" +
                             "buttons to present them in. This will cause problems.");
        }

        // Display each option in a button, and make it visible
        int i = 0;
        foreach (var optionString in optionsCollection.options)
        {
            optionButtons[i].gameObject.SetActive(true);
            optionButtons[i].GetComponentInChildren<Text>().text = optionString;
            i++;
        }

        // Record that we're using it
        SetSelectedOption = optionChooser;

        // Wait until the chooser has been used and then removed (see SetOption below)
        while (SetSelectedOption != null)
        {
            yield return null;
        }

        runningOptions = false;
        // Hide all the buttons
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    /// Called by buttons to make a selection.
    public void SetOption(int selectedOption)
    {

        // Call the delegate to tell the dialogue system that we've
        // selected an option.
        SetSelectedOption(selectedOption);

        // Now remove the delegate so that the loop in RunOptions will exit
        SetSelectedOption = null;
    }

    /// Run an internal command.
    public override IEnumerator RunCommand(Yarn.Command command)
    {
        // "Perform" the command
        Debug.Log("Command: " + command.text);

        yield break;
    }

    /// Called when the dialogue system has started running.
    public override IEnumerator DialogueStarted()
    {
        Debug.Log("Dialogue starting!");

        // Enable the dialogue controls.
       /* if (dialogueContainer != null)
            dialogueContainer.SetActive(true);*/

        // Hide the game controls.
        if (gameControlsContainer != null)
        {
            gameControlsContainer.gameObject.SetActive(false);
        }

        yield break;
    }

    /// Called when the dialogue system has finished running.
    public override IEnumerator DialogueComplete()
    {
        Debug.Log("Complete!");

        // Hide the dialogue interface.
        /*if (dialogueContainer != null)
            dialogueContainer.SetActive(false);*/

        // Show the game controls.
        if (gameControlsContainer != null)
        {
            gameControlsContainer.gameObject.SetActive(true);
        }

        yield break;
    }

}
