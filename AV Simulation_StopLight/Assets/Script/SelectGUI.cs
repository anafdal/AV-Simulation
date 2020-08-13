using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGUI : MonoBehaviour
{

    //Pictograms
    public Texture2D Humanoid1;
    public Texture2D Humanoid2;
    public Texture2D Traffic1;
    public Texture2D Traffic2;
    public Texture2D Vehical1;
    public Texture2D Vehical2;
    //Physical Signs
    public Texture2D HumanoidAV1;
    public Texture2D HumanoidAV2;
    public Texture2D TrafficAV1;
    public Texture2D TrafficAV2;
    //Text Signs
    public Texture2D SingleWord1;
    public Texture2D SingleWord2;
    public Texture2D SingleWord3;
    public Texture2D SingleWord4;
    public Texture2D Sentence1;
    public Texture2D Sentence2;
    public Texture2D Sentence3;
    public Texture2D Sentence4;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI(){
      // GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50,400,90), "Select Interface");

      //page 1 Approach
      if(GUI.Button( new Rect (100,100, 100, 50), Humanoid1)){
        print("humanoid1");

      }


    }
}
