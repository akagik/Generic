using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    public bool isOn;
    public Graphic onGraphic;
    public Graphic offGraphic;
    public Toggle.ToggleEvent onValueChanged;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (offGraphic == null)
        {
            offGraphic = button.targetGraphic;
        }

        button.onClick.AddListener(() =>
        {
            isOn = !isOn;
            onValueChanged.Invoke(isOn);
            setImage();
        });

        setImage();
    }

    private void setImage()
    {
        if (isOn)
        {
            button.targetGraphic = onGraphic;
            onGraphic.gameObject.SetActive(true);
            offGraphic.gameObject.SetActive(false);
        }
        else
        {
            button.targetGraphic = offGraphic;
            onGraphic.gameObject.SetActive(false);
            offGraphic.gameObject.SetActive(true);
        }
    }

}
