using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_menu_texts : MonoBehaviour
{
    // Start is called before the first frame update
    public Text name1;
    public Text name2;
    public Text time1;
    public Text time2;
    
    public void setTimes(string n1, string n2, string t1, string t2){
        name1.text = n1;
        name2.text = n2;
        time1.text = t1;
        time2.text = t2;
    }
    void Start()
    {
        setTimes(StaticClass.HumanName, StaticClass.AiName, StaticClass.HumanTime, StaticClass.AiTime);
    }

}
