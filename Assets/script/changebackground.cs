using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class changebackground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Material skybox1;
    [SerializeField] Material skybox2;
    void Start()
    {
        XRSimpleInteractable backChange = GetComponent<XRSimpleInteractable>();
        backChange.selectEntered.AddListener(PrintMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PrintMessage(SelectEnterEventArgs arg){
        Debug.Log("Hello");
        RenderSettings.skybox = skybox2;

    }
}
