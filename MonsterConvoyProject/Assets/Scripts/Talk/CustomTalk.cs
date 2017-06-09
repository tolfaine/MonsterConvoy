using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;



[AddComponentMenu("Seize Studios/RPG Talk")]
public class CustomTalk : MonoBehaviour {

	[Tooltip("Should the talk be initiated whe the script awakes?")]
	public bool startOnAwake = true;

	[Tooltip("Any object that should be shown or hidden with the text")]
	public GameObject[] showWithDialog;
	
	[Tooltip("UI with the text")]
	public Text textUI;

	[Tooltip("Sould have the name of the dialoger?")]
	public bool dialoger;

	[Tooltip("UI with the talker text")]
	public Text dialogerUI;

	[Tooltip("Should the element follow someone?")]
	public bool shouldFollow;

	[Tooltip("Who to follow?")]
	public GameObject follow;

	[Tooltip("If he is following someone, should be an offset?")]
	public Vector3 followOffset;

	[Tooltip("The show with objects should be Billboard?")]
	public bool billboard = true;

	[Tooltip("Billboard based on main camera?")]
	public bool mainCamera = true;

	[Tooltip("Billboard based on other camera")]
	public Camera otherCamera;

	[Tooltip("Text file to be the talk")]
	public TextAsset txtToParse;

    public TextAsset monsterInitTalk;

    public TextAsset humanTalk;
    public TextAsset monsterTalk;

    public TextAsset humanTalkReac;
    public TextAsset monsterTalkReac;

    public TextAsset humanFailAttack;

    public bool talkHasBeenInit = false;
    public bool combatHasBeenInit = false;

    public TextAsset bardTalk;
    public TextAsset edTalk;

    public TextAsset actualTextToParse;

    [Tooltip("If the player click on it, should it be skipped?")]
	public bool enableQuickSkip = true;

	[Tooltip("Some script to look for a feedback when the talk is finished. Leave blank if no feedback is needed")]
	public MonoBehaviour callbackScript;

	[Tooltip("Function to be called when the talk finish")]
	public string callbackFunction;

	[Tooltip("An animator boolean can be set when the character is talking")]
	public Animator animatorWhenTalking;

	[Tooltip("Name of the boolean property in animator")]
	public string animatorBooleanName;

	[Tooltip("Name of the int property in animator meaning the talker")]
	public string animatorIntName;

	/*[Tooltip("Name fo the trigger to be set in line. Leave null if none should be used")]
	public string triggerInLineName;

	[Tooltip("What line should the previous trigger be playerd?")]
	public int triggerInLine;*/


	
	// Wich position of the talk are we?
	private int cutscenePosition = 0;
	
	[Tooltip("Speed of the text, in characters per second")]
	public float textSpeed = 50.0f;
	//wich character are we?
	private float currentChar = 0.0f;

	//a list with every element of the cutscene
	private List<RpgtalkElement> rpgtalkElements;

	[Tooltip("A GameObject to blink when the text is finished")]
	public GameObject blinkWhenReady;

	[Tooltip("Is there any variable in the text?")]
	public RPGTalkVariable[] variables;

	[Tooltip("Should there be photos?")]
	public bool shouldUsePhotos;

	[Tooltip("The photo of the person who is talking")]
	public RPGTalkPhoto[] photos;

	[Tooltip("The UI that the photo should be applied to")]
	public Image UIPhoto;

	[Tooltip("If the dialog should stay on screen even if the text has ended")]
	public bool shouldStayOnScreen;

	bool lookForClick = true;

	[Tooltip("Audio to be played while the character is talking")]
	public AudioClip textAudio;
	[Tooltip("Audio to be played when player passes the Talk")]
	public AudioClip passAudio;
	AudioSource rpgAudioSorce;


	[Tooltip("Pass with mouse Click")]
	public bool passWithMouse = true;

	[Tooltip("Pass with some button set on Project Settings > Input")]
	public string passWithInputButton;

	[Tooltip("Line to Start reading the text")]
	public int lineToStart = 1;
	[Tooltip("Line to Stop reading the text (Leave -1 if should read until the end)")]
	public int lineToBreak = -1;

    public int humanFirstNormalTalkLine = 2;
    public int humanLastNormalTalkLine = 22;
    public int humanFirstNormalAttaqueLine = 10;
    public int humanLastNormalAttaqueLine = 15;
   // public int humanFirstEchecAttaqueLine = 18;
   // public int humanLastEchecAttaqueLine = 23;
    public int humanFirstFuiteLine = 26;
    public int humanLastFuiteLine = 31;
    public int humanFirstNormalFearLine = 10;
    public int humanLastNormalFearLine = 15;

    public int monsterFirstNormalTalkLine = 2;
    public int monsterLastNormalTalkLine = 24;
    public int monsterFirstNormalFearLine = 27;
    public int monsterLastNormalFearLine = 57;
   // public int monsterFirstFailFearLine = 18;
   // public int monsterLastFailFearLine = 23;
    public int monsterFirstNormalAttaqueLine = 26;
    public int monsterLastNormalAttaqueLine = 31;

    private int actualLineToStart;
	private int actualLineToBreak;

	[Tooltip("Tries to break long lines into several talks")]
	public bool wordWrap = true;

	public int maxCharInWidth = 50;
	public int maxCharInHeight = 4;

  //  private bool canReaction = true;

  //  public CustomTalk reactTalk;
 //   public bool needReaction;
 //   public CreatureType creatureReaction;
 //   public ActionType actionReaction;

//    public bool isReaction;

    public GameObject humanAnchor;
    public GameObject monsterAnchor;

    public bool isMageTalk = false;

    public string caractMonster;
    public string caractHumain;

    void Awake(){
		if (startOnAwake) {
			//NewTalk ();
		}

        talkHasBeenInit = false;
        combatHasBeenInit = false;
        /*
         * 
        if (isReaction)
        {
           monsterFirstNormalTalkLine = 2;
           monsterLastNormalTalkLine = 22;

           humanFirstNormalTalkLine = 2;
           humanLastNormalTalkLine = 22;

           humanFirstNormalFearLine =25;
           humanLastNormalFearLine = 57;
        }

        */
        /*
        if (GameObject.FindGameObjectWithTag("ProtoManager"))
        {
            canReaction = false;
        }
        */
    }

    /// <summary>
    /// Starts a new Talk.
    /// </summary>
    /// 

    public void NewTalkScripted(TextAsset textAss, int lineMin, int lineMax)
    {

        if (isMageTalk)
        {
            AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }

        txtToParse = textAss;

        lineToStart = lineMin;
        lineToBreak = lineMax;

        //reduce one for the line to Start and break
        if (lineToBreak == -1)
        {
            actualLineToBreak = lineToBreak;
        }
        else
        {
            actualLineToBreak = lineToBreak - 1;
        }
        actualLineToStart = lineToStart - 1;

        if (textAudio != null)
        {
            if (rpgAudioSorce == null)
            {
                rpgAudioSorce = gameObject.AddComponent<AudioSource>();
            }
        }

        lookForClick = true;

        //Stop any blinking arrows that shouldn't appear
        CancelInvoke("blink");
        if (blinkWhenReady)
        {
            blinkWhenReady.SetActive(false);
        }


        //reset positions
        cutscenePosition = 1;
        currentChar = 0;


        //create a new CutsCeneElement
        rpgtalkElements = new List<RpgtalkElement>();

        if (txtToParse != null)
        {
            // read the TXT file into the elements list
            StringReader reader = new StringReader(txtToParse.text);

            string line = reader.ReadLine();
            int currentLine = 0;

            while (line != null)
            {

                if (currentLine >= actualLineToStart)
                {
                    if (actualLineToBreak == -1 || currentLine <= actualLineToBreak)
                    {

                        if (wordWrap)
                        {
                            CheckIfTheTextFits(line);
                        }
                        else
                        {
                            rpgtalkElements.Add(readSceneElement(line));
                        }
                    }
                }


                line = reader.ReadLine();
                currentLine++;
            }


            if (rpgtalkElements.Count == 0)
            {
                Debug.LogError("The Line To Start and the Line To Break are not fit for the given TXT");
                return;
            }
        }





        //Set the speaker name and photo
        if (dialoger)
        {
            dialogerUI.text = rpgtalkElements[0].speakerName;
            if (shouldUsePhotos)
            {
                for (int i = 0; i < photos.Length; i++)
                {
                    if (photos[i].name == rpgtalkElements[0].originalSpeakerName)
                    {
                        UIPhoto.sprite = photos[i].photo;
                        if (animatorWhenTalking && animatorIntName != "")
                        {
                            animatorWhenTalking.SetInteger(animatorIntName, i);
                        }
                    }
                }
            }
        }

        //show what need to be shown
        textUI.enabled = true;
        if (dialoger)
        {
            dialogerUI.enabled = true;
        }
        for (int i = 0; i < showWithDialog.Length; i++)
        {
            showWithDialog[i].SetActive(true);
        }


        //if we have an animator.. play it
        if (animatorWhenTalking != null)
        {
            animatorWhenTalking.SetBool(animatorBooleanName, true);
        }
    }
    public void NewTalkED(int lineNb)
    {

        lineToStart = lineNb;
        lineToBreak = lineNb;

        txtToParse = edTalk;

        //reduce one for the line to Start and break
        if (lineToBreak == -1)
        {
            actualLineToBreak = lineToBreak;
        }
        else
        {
            actualLineToBreak = lineToBreak - 1;
        }
        actualLineToStart = lineToStart - 1;

        if (textAudio != null)
        {
            if (rpgAudioSorce == null)
            {
                rpgAudioSorce = gameObject.AddComponent<AudioSource>();
            }
        }

        lookForClick = true;

        //Stop any blinking arrows that shouldn't appear
        CancelInvoke("blink");
        if (blinkWhenReady)
        {
            blinkWhenReady.SetActive(false);
        }


        //reset positions
        cutscenePosition = 1;
        currentChar = 0;


        //create a new CutsCeneElement
        rpgtalkElements = new List<RpgtalkElement>();

        if (txtToParse != null)
        {
            // read the TXT file into the elements list
            StringReader reader = new StringReader(txtToParse.text);

            string line = reader.ReadLine();
            int currentLine = 0;

            while (line != null)
            {

                if (currentLine >= actualLineToStart)
                {
                    if (actualLineToBreak == -1 || currentLine <= actualLineToBreak)
                    {


                        if (line.Contains("*mutation*"))
                        {
                            line = line.Replace("*mutation*", caractMonster);
                        }

                        if (line.Contains("*cheveux*"))
                        {
                            line = line.Replace("*cheveux*", caractHumain);
                        }

                        if (wordWrap)
                        {
                            CheckIfTheTextFits(line);
                        }
                        else
                        {
                            rpgtalkElements.Add(readSceneElement(line));
                        }
                    }
                }

                line = reader.ReadLine();
                currentLine++;
            }


            if (rpgtalkElements.Count == 0)
            {
                Debug.LogError("The Line To Start and the Line To Break are not fit for the given TXT");
                return;
            }
        }





        //Set the speaker name and photo
        if (dialoger)
        {
            dialogerUI.text = rpgtalkElements[0].speakerName;
            if (shouldUsePhotos)
            {
                for (int i = 0; i < photos.Length; i++)
                {
                    if (photos[i].name == rpgtalkElements[0].originalSpeakerName)
                    {
                        UIPhoto.sprite = photos[i].photo;
                        if (animatorWhenTalking && animatorIntName != "")
                        {
                            animatorWhenTalking.SetInteger(animatorIntName, i);
                        }
                    }
                }
            }
        }

        //show what need to be shown
        textUI.enabled = true;
        if (dialoger)
        {
            dialogerUI.enabled = true;
        }
        for (int i = 0; i < showWithDialog.Length; i++)
        {
            showWithDialog[i].SetActive(true);
        }


        //if we have an animator.. play it
        if (animatorWhenTalking != null)
        {
            animatorWhenTalking.SetBool(animatorBooleanName, true);
        }
    }
    public void NewTalkBard(int lineNb, bool start, bool run)
    {

        lineToStart = lineNb;
        lineToBreak = lineNb;

        if (start)
        {
            lineToStart = 2;
            lineToBreak = 2;

        }
        if (run)
        {
            lineToStart = 1;
            lineToBreak = 1;
        }

        txtToParse = bardTalk;

        //reduce one for the line to Start and break
        if (lineToBreak == -1)
        {
            actualLineToBreak = lineToBreak;
        }
        else
        {
            actualLineToBreak = lineToBreak - 1;
        }
        actualLineToStart = lineToStart - 1;

        if (textAudio != null)
        {
            if (rpgAudioSorce == null)
            {
                rpgAudioSorce = gameObject.AddComponent<AudioSource>();
            }
        }

        lookForClick = true;

        //Stop any blinking arrows that shouldn't appear
        CancelInvoke("blink");
        if (blinkWhenReady)
        {
            blinkWhenReady.SetActive(false);
        }


        //reset positions
        cutscenePosition = 1;
        currentChar = 0;


        //create a new CutsCeneElement
        rpgtalkElements = new List<RpgtalkElement>();

        if (txtToParse != null)
        {
            // read the TXT file into the elements list
            StringReader reader = new StringReader(txtToParse.text);

            string line = reader.ReadLine();
            int currentLine = 0;

            while (line != null)
            {

                if (currentLine >= actualLineToStart)
                {
                    if (actualLineToBreak == -1 || currentLine <= actualLineToBreak)
                    {


                        if (line.Contains("*mutation*"))
                        {
                            line = line.Replace("*mutation*", caractMonster);
                        }

                        if (line.Contains("*cheveux*"))
                        {
                            line = line.Replace("*cheveux*", caractHumain);
                        }

                        if (wordWrap)
                        {
                            CheckIfTheTextFits(line);
                        }
                        else
                        {
                            rpgtalkElements.Add(readSceneElement(line));
                        }
                    }
                }

                line = reader.ReadLine();
                currentLine++;
            }


            if (rpgtalkElements.Count == 0)
            {
                Debug.LogError("The Line To Start and the Line To Break are not fit for the given TXT");
                return;
            }
        }





        //Set the speaker name and photo
        if (dialoger)
        {
            dialogerUI.text = rpgtalkElements[0].speakerName;
            if (shouldUsePhotos)
            {
                for (int i = 0; i < photos.Length; i++)
                {
                    if (photos[i].name == rpgtalkElements[0].originalSpeakerName)
                    {
                        UIPhoto.sprite = photos[i].photo;
                        if (animatorWhenTalking && animatorIntName != "")
                        {
                            animatorWhenTalking.SetInteger(animatorIntName, i);
                        }
                    }
                }
            }
        }

        //show what need to be shown
        textUI.enabled = true;
        if (dialoger)
        {
            dialogerUI.enabled = true;
        }
        for (int i = 0; i < showWithDialog.Length; i++)
        {
            showWithDialog[i].SetActive(true);
        }


        //if we have an animator.. play it
        if (animatorWhenTalking != null)
        {
            animatorWhenTalking.SetBool(animatorBooleanName, true);
        }
    }

    public void NewTalk(CreatureType type,ActionType action, float roll){


        if (isMageTalk)
        {
            AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }

        int minLine = 0;
        int maxLine = 0;

        /*
        if (isReaction)
        {
            
            if (type == CreatureType.Human)
                follow = humanAnchor;
            else
                follow = monsterAnchor;
                
        }
        */


        if(type == CreatureType.Human)
        {

            if (action == ActionType.ATTACK)
            {
                if (roll < 0.1f)
                {
                    txtToParse = humanFailAttack;

                    minLine = 1;
                    maxLine = 33;
                }
                else
                {
                    minLine = humanFirstNormalAttaqueLine;
                    maxLine = humanLastNormalAttaqueLine;
                }
            }
            else if (action == ActionType.TALK)
            {
                if (!talkHasBeenInit)
                {
                    talkHasBeenInit = true;
                    txtToParse = humanTalk;
                }
                else
                {
                    txtToParse = humanTalkReac;
                }

                minLine = 1;
                maxLine = 22;

                /*
                if (!isReaction && canReaction)
                {
                    needReaction = true;
                    creatureReaction = CreatureType.Monster;
                    actionReaction = action;
                }
                */
            }
            else if (action == ActionType.FEAR)
            {
                minLine = humanFirstNormalFearLine;
                maxLine = humanLastNormalFearLine;
            }
            else
                return;

        }
        else if (type == CreatureType.Monster)
        {


            if (action == ActionType.ATTACK)
            {
                minLine = monsterFirstNormalAttaqueLine;
                maxLine = monsterLastNormalAttaqueLine;
            }
            else if (action == ActionType.TALK)
            {
                if (!talkHasBeenInit)
                {
                    txtToParse = monsterTalk;
                    talkHasBeenInit = true;

                    if (!combatHasBeenInit)
                    {
                        combatHasBeenInit = true;
                        minLine = 77;
                        maxLine = 107;

                    }
                    else
                    {
                        minLine = 2;
                        maxLine = 22;
 
                    }

                }
                else
                {
                    txtToParse = monsterTalkReac;
                    minLine = 2;
                    maxLine = 22;
                }



                /*
                if (!isReaction && canReaction)
                {
                    needReaction = true;
                    creatureReaction = CreatureType.Human;
                    actionReaction = action;
                }
                */
            }
            else if (action == ActionType.FEAR)
            {
                if(roll < 0.2f)
                {
                    minLine = 27;
                    maxLine = 57;
                }
                else
                {
                    minLine = 27;
                    maxLine = 57;
                }

                /*
                if (!isReaction && canReaction)
                {
                    needReaction = true;
                    creatureReaction = CreatureType.Human;
                    actionReaction = action;
                }*/
            }
            else
                return;
        }

        int rand = Random.Range(minLine, maxLine);

        lineToStart = rand;
        lineToBreak = rand;

		//reduce one for the line to Start and break
		if(lineToBreak == -1){
			actualLineToBreak = lineToBreak;
		}else{
			actualLineToBreak = lineToBreak-1;
		}
		actualLineToStart = lineToStart-1;

		if (textAudio != null) {
			if (rpgAudioSorce == null) {
				rpgAudioSorce = gameObject.AddComponent<AudioSource> ();
			}
		}

		lookForClick = true;

		//Stop any blinking arrows that shouldn't appear
		CancelInvoke ("blink");
		if (blinkWhenReady) {
			blinkWhenReady.SetActive (false);
		}


		//reset positions
		cutscenePosition = 1;
		currentChar = 0;


		//create a new CutsCeneElement
		rpgtalkElements = new List<RpgtalkElement>();

        if (txtToParse != null) {
			// read the TXT file into the elements list
			StringReader reader = new StringReader (txtToParse.text);
			
			string line = reader.ReadLine(); 
			int currentLine = 0;

			while (line != null) {
				
				if (currentLine >= actualLineToStart) {
					if (actualLineToBreak == -1 || currentLine <= actualLineToBreak) {


                        if (line.Contains("*mutation*"))
                        {
                            line = line.Replace("*mutation*", caractMonster);
                        }

                        if (line.Contains("*cheveux*"))
                        {
                            line = line.Replace("*cheveux*", caractHumain);
                        }

                        if (wordWrap) {
							CheckIfTheTextFits (line);
						} else {
							rpgtalkElements.Add (readSceneElement (line));
						}
					}
				}
				
				line = reader.ReadLine();
				currentLine++;
			}


			if(rpgtalkElements.Count == 0){
				Debug.LogError ("The Line To Start and the Line To Break are not fit for the given TXT");
				return;
			}
		}





		//Set the speaker name and photo
		if (dialoger) {
			dialogerUI.text = rpgtalkElements [0].speakerName;
			if (shouldUsePhotos) {
				for (int i = 0; i < photos.Length; i++) {
					if (photos [i].name == rpgtalkElements [0].originalSpeakerName) {
						UIPhoto.sprite = photos [i].photo;
						if(animatorWhenTalking && animatorIntName != ""){
							animatorWhenTalking.SetInteger (animatorIntName, i);
						}
					}
				}
			}
		}

		//show what need to be shown
		textUI.enabled = true;
		if (dialoger) {
			dialogerUI.enabled = true;
		}
		for (int i = 0; i < showWithDialog.Length; i++) {
			showWithDialog[i].SetActive(true);
		}


		//if we have an animator.. play it
		if (animatorWhenTalking != null) {
			animatorWhenTalking.SetBool (animatorBooleanName, true);
		}
	}

	private RpgtalkElement readSceneElement(string line) {
		
		RpgtalkElement newElement = new RpgtalkElement();

		newElement.originalSpeakerName = line;

        //replace any variable that may exist on the text
        if (line.Contains("*mutation*"))
        {
            line = line.Replace("*mutation*", caractMonster);
        }

        if (line.Contains("*cheveux*"))
        {
            line = line.Replace("*cheveux*", caractHumain);
        }


        //If we want to show the dialoger's name, slipt the line at the ':'
        if (dialoger) {

			if (line.IndexOf (':') != -1) {

				string[] splitLine = line.Split (new char[] { ':' }, 2);

				newElement.speakerName = splitLine [0].Trim ();

				newElement.dialogText = splitLine [1].Trim ();

				string[] originalSplitLine = newElement.originalSpeakerName.Split (new char[] { ':' },2);

				newElement.originalSpeakerName = originalSplitLine [0].Trim ();

			} else {
				newElement.dialogText = line;
			}
		} else {
			newElement.dialogText = line;
		}

		newElement.hasDialog = true;


		
		return newElement;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!textUI.gameObject.activeInHierarchy) {
			return;
		}


		if(follow != null){
			Vector3 newPos = follow.transform.position - followOffset;
			Quaternion newRotation = follow.transform.rotation;
			if (billboard) {
				if (mainCamera) {
					newRotation = Camera.main.transform.rotation;
				} else {
					newRotation = otherCamera.transform.rotation;
				}
			}

			for (int i = 0; i < showWithDialog.Length; i++) {
				showWithDialog[i].transform.position = newPos;
				showWithDialog [i].transform.rotation = newRotation;
			}




		}


		if (textUI.enabled &&
			currentChar >= rpgtalkElements [cutscenePosition - 1].dialogText.Length) {

			//if we hit the end of the talk, but we should stay on screen, return.
			//but if we have a callback, he can click on it once more.
			if (cutscenePosition >= rpgtalkElements.Count && shouldStayOnScreen) {
				if(lookForClick && (
					(passWithMouse && Input.GetMouseButtonDown (0)) ||
					(passWithInputButton != "" && Input.GetButtonDown(passWithInputButton))
				)){

                
                    //if have an audio... playit
                    if (passAudio != null && !rpgAudioSorce.isPlaying) {
						rpgAudioSorce.clip = passAudio;
						rpgAudioSorce.Play ();
					}
					if(callbackScript != null){
						callbackScript.Invoke(callbackFunction,0f);
						//Stop any blinking arrows that shouldn't appear
						CancelInvoke ("blink");
						if (blinkWhenReady) {
							blinkWhenReady.SetActive (false);
						}
					}
					lookForClick = false;
				}

				CancelInvoke ("blink");
				if (blinkWhenReady) {
					blinkWhenReady.SetActive (false);
				}
				return;
			}

			//if we reached the end of the line and click on the screen...
			if (
				(passWithMouse && Input.GetMouseButtonDown (0)) ||
				(passWithInputButton != "" && Input.GetButtonDown(passWithInputButton))
			) {//if have an audio... playit
				if (passAudio != null) {

                    rpgAudioSorce.clip = passAudio;
					rpgAudioSorce.Play ();
				}

                textUI.enabled = false;

                

                PlayNext();

			}
			return;
		}




		//if we're currently showing dialog, then start scrolling it
		if(textUI.enabled) {
			// if there's still text left to show
			if(currentChar < rpgtalkElements[cutscenePosition - 1].dialogText.Length) {
				
				//ensure that we don't accidentally blow past the end of the string
				currentChar = Mathf.Min(
					currentChar + textSpeed * Time.deltaTime,
					rpgtalkElements[cutscenePosition - 1].dialogText.Length);
				
				textUI.text =
					rpgtalkElements[cutscenePosition - 1].dialogText.Substring(0, (int)currentChar)
					;


				//if have an audio... playit
				if (textAudio != null && !rpgAudioSorce.isPlaying) {
					rpgAudioSorce.clip = textAudio;
					rpgAudioSorce.Play ();
				}

			} 
			
			if(enableQuickSkip == true &&
				(
					(passWithMouse && Input.GetMouseButtonDown (0)) ||
					(passWithInputButton != "" && Input.GetButtonDown(passWithInputButton))
				)
				&& currentChar > 3) {
				textUI.text = rpgtalkElements[cutscenePosition - 1].dialogText;
				currentChar = rpgtalkElements[cutscenePosition - 1].dialogText.Length;
			}

			if(currentChar >= rpgtalkElements[cutscenePosition - 1].dialogText.Length){
				blink();

				//if we have an animator.. stop it
				if (animatorWhenTalking != null) {
					animatorWhenTalking.SetBool (animatorBooleanName, false);
				}

				if(cutscenePosition >= rpgtalkElements.Count && callbackScript == null){
					//Stop any blinking arrows that shouldn't appear
					/*CancelInvoke ("blink");
					if (blinkWhenReady) {
						blinkWhenReady.SetActive (false);
					}*/
				}
			}




			
		}


	}

	void blink(){
		if (blinkWhenReady) {
			blinkWhenReady.SetActive (!blinkWhenReady.activeInHierarchy);
			Invoke ("blink", .5f);
		}
	}

	void CheckIfTheTextFits(string line){

		//make base calculations for the size of the font and the textUI.

		int widthBase = Mathf.FloorToInt(4f * textUI.fontSize);
		int heightBase = Mathf.FloorToInt(textUI.fontSize/3);
        //int maxCharInWidth = Mathf.FloorToInt ((widthBase * textUI.rectTransform.rect.width) / 438);
        //int maxCharInHeight = Mathf.FloorToInt ((heightBase * textUI.rectTransform.rect.height) / 71);

        //int maxCharInWidth = 25;
        //  int maxCharInHeight = 2;

        for (int i = 0; i < variables.Length; i++)
        {
            if (line.Contains(variables[i].variableName))
            {
                line = line.Replace(variables[i].variableName, variables[i].variableValue);
            }

            if (line.Contains("*mutation*"))
            {
                line = line.Replace("*mutation*", caractMonster);
            }

            if (line.Contains("*cheveux*"))
            {
                line = line.Replace("*cheveux*", caractHumain);
            }
        }
        

        int maxCharsOnUI = maxCharInWidth * maxCharInHeight;
		if (line.Length > maxCharsOnUI) {

			//how many talks would be necessary to fit this text?
			int howMuchMore = Mathf.CeilToInt((float)line.Length / (float)maxCharsOnUI);
			string newLine = "";
			int cuttedInSpace = -1;

			for (int i = 0; i < howMuchMore; i++) {
				//get the characeter that we should start saying
				int startChar = i * maxCharsOnUI;
				if(cuttedInSpace != -1){
					startChar = cuttedInSpace;
					cuttedInSpace = -1;
				}


				//if the new line fits the talk...
				if (line.Substring (startChar, 
					line.Length - (startChar)).Length < maxCharsOnUI) {
					newLine = line.Substring (startChar, 
						line.Length - (startChar));
				} else {
					//if it not, search for spaces near to the last word and cut it
					cuttedInSpace = line.IndexOf (" ", startChar+ (maxCharsOnUI) - 3); // SI YA DES MOTS DE DE PLUS DE 8 PLEASE
					if(cuttedInSpace != -1){
						newLine = line.Substring (startChar, cuttedInSpace-startChar);
					}else{
						newLine = line.Substring (startChar, maxCharsOnUI);
					}
				}

				rpgtalkElements.Add (readSceneElement (newLine));
			}
		} else {

			rpgtalkElements.Add (readSceneElement (line));
		}
	}


	/// <summary>
	/// Finish the talk, skipping every dialog. The callback function still going to be called
	/// </summary>
	public void EndTalk() {
		if (textUI.gameObject.activeInHierarchy) {
			if (shouldStayOnScreen) {
				cutscenePosition = rpgtalkElements.Count-1;
			} else {
				cutscenePosition = rpgtalkElements.Count;
			}
			PlayNext ();
		}
	}



	/// <summary>
	/// Plays the next dialog in the current Talk.
	/// </summary>
	public void PlayNext() {
		// increment the cutscene counter
		cutscenePosition++;
		currentChar = 0;


		/*if (triggerInLineName != "" && cutscenePosition == triggerInLine) {
			animatorWhenTalking.SetTrigger (triggerInLineName);
		}*/

		CancelInvoke ("blink");
		if (blinkWhenReady) {
			blinkWhenReady.SetActive (false);
		}
		
		if(cutscenePosition <= rpgtalkElements.Count) {


            if (isMageTalk)
            {
                AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
            }

            textUI.enabled = true;
			
			RpgtalkElement currentRpgtalkElement = rpgtalkElements[cutscenePosition - 1];

			if (dialoger) {
				dialogerUI.enabled = true;

				dialogerUI.text = currentRpgtalkElement.speakerName;
				if (shouldUsePhotos) {
					for (int i = 0; i < photos.Length; i++) {
						if (photos [i].name == currentRpgtalkElement.originalSpeakerName) {
							UIPhoto.sprite = photos [i].photo;
							if(animatorWhenTalking && animatorIntName != ""){
								animatorWhenTalking.SetInteger (animatorIntName, i);

							}
						}
					}
				}
			}



			//if we have an animator.. play it
			if (animatorWhenTalking != null) {
				animatorWhenTalking.SetBool (animatorBooleanName, true);
			}




		} else {

            /*
            if (needReaction)
            {
                reactTalk.NewTalk(creatureReaction, actionReaction, 1f);
                needReaction = false;
            }
            */

            if (!shouldStayOnScreen) {
				textUI.enabled = false;
				if (dialoger) {
					dialogerUI.enabled = false;
				}
				for (int i = 0; i < showWithDialog.Length; i++) {
					showWithDialog [i].SetActive (false);
				}
			}

			if(callbackScript != null){
				callbackScript.Invoke(callbackFunction,0f);
			}
		}

		
	}
		


	private class RpgtalkElement {
		public bool hasDialog = false;
		public bool allowPlayerAdvance = true;
		public string speakerName;
		public string originalSpeakerName;
		public string dialogText;
		
		public override string ToString () {
			return  "(" + this.hasDialog + ")" + this.speakerName + "::" + this.dialogText + "\n";
		}
	}

}
