using UnityEngine;
using UnityEngine.UI;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

public class ColorPickerScript : MonoBehaviour {

    ////Red = 0, Green = 1, Blue = 2, Alpha = 3
    [SerializeField]
    Slider[] sliders = new Slider[4];
    [SerializeField]
    Image colorSwatch;

    InputField[] inputs = new InputField[4];
    float rValue = 255, gValue = 1, bValue = 1, aValue = 255;   //public field ignores initial values
    float[] tempValues = new float[4];

    void Awake()
    {
        for(int i = 0; i < sliders.Length; i++)
        {
            inputs[i] = sliders[i].transform.FindChild("InputField").GetComponent<InputField>();
        }
    }

    void Update()
    {
        if(isActiveAndEnabled)
        {
            colorSwatch.color = new Color(rValue / 255, gValue / 255, bValue / 255, aValue / 255);
        }
    }

    public void SetRedValue()
    {
        rValue = sliders[0].value;
        tempValues[0] = rValue;
    }
    public void SetGreenValue()
    {
        gValue = sliders[1].value;
        tempValues[1] = gValue;
    }
    public void SetBlueValue()
    {
        bValue = sliders[2].value;
        tempValues[2] = bValue;
    }
    public void SetAlphaValue()
    {
        aValue = sliders[3].value;
        tempValues[3] = aValue;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
    public void ClampValue(InputField input)
    {
        if(int.Parse(input.text) > 255)
        {
            input.text = "255";
        }
    }

    public Color GetBlockColor()
    {
        return new Color(rValue / 255, gValue / 255, bValue / 255, aValue / 255);
    }
}

/*public void SetSliderPosition(Slider sli)
    {
        sli.value = rValue;
    }*/
