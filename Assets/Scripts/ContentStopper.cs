using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStopper : MonoBehaviour
{
    public int numberOfButtons; // dynamic int of button #
    private int buttonSize = 75; // min height of item prefab
    RectTransform cont;

	void Start ()
    {
        cont = this.GetComponent<RectTransform>();
	}
	
	void Update ()
    {
        if (numberOfButtons != 0)
        {
            if (cont.offsetMax.y < 0)
            {
                cont.offsetMax = new Vector2(0, 1);
                cont.offsetMin = new Vector2(0, 1);
            }
            if (cont.offsetMax.y > (numberOfButtons * buttonSize))
            {
                cont.offsetMax = new Vector2(0, (numberOfButtons * buttonSize));
                cont.offsetMin = new Vector2();
            }
        }
    }
}
