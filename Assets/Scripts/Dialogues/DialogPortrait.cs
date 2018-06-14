using UnityEngine;
using UnityEngine.UI;

public class DialogPortrait : MonoBehaviour 
{

    public void SetImage(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
    
}
