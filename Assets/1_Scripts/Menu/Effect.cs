using UnityEngine;
using UnityEngine.EventSystems;

public class HoverShowObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToShow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objectToShow != null)
            objectToShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectToShow != null)
            objectToShow.SetActive(false);
    }
}
