using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    private bool _isOn;

    public bool isOn
    {
        get { return _isOn; }

        set
        {
            _isOn = value;
            setImage();
        }
    }

    public Graphic onGraphic;
    public Graphic offGraphic;
    public Toggle.ToggleEvent onValueChanged;

    private Button button;
    private bool isInit;

    private void Awake()
    {
        if (!isInit) init();
    }

    private void init()
    {
        isInit = true;
        button = GetComponent<Button>();
        Debug.Log("ToggleButton Awake : " + button);

        if (offGraphic == null)
        {
            offGraphic = button.targetGraphic;
        }

        button.onClick.AddListener(() =>
        {
            isOn = !isOn;
            onValueChanged.Invoke(isOn);
        });

        setImage();
    }


    private void setImage()
    {
        if (!isInit) init();

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