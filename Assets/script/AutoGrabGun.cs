using UnityEngine;


public class AutoGrabGun : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor leftHandInteractor;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor rightHandInteractor;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable gun;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 检查是否有可用的手，直接让按 G 的手抓住枪
            if (!leftHandInteractor.hasSelection)
            {
                ForceGrab(leftHandInteractor);
            }
            else if (!rightHandInteractor.hasSelection)
            {
                ForceGrab(rightHandInteractor);
            }
        }
    }

    void ForceGrab(UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor interactor)
    {
        // 如果已经抓了别的东西，先放开
        if (interactor.hasSelection)
        {
            interactor.EndManualInteraction();
        }

        // 抓住枪
        interactor.StartManualInteraction(gun as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable);
    }
}
