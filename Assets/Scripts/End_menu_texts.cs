using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_menu_texts : MonoBehaviour
{
    // Start is called before the first frame update
    public Text name1;
    public Text name2;
    public Text name3;
    public Text time1;
    public Text time2;
    public Text time3;
    
    public void setTimes(string n1, string n2, string n3, string t1, string t2, string t3){
        name1.text = n1;
        name2.text = n2;
        name3.text = n3;
        time1.text = t1;
        time2.text = t2;
        time3.text = t3;
    }
    void Start()
    {
        setTimes("Verstappen", "Hamilton", "Bottas", "1:31:14", "1:33:61", "1:40:13");
    }

}
