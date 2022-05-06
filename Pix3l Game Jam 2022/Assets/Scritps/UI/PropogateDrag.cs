using UnityEngine;

using UnityEngine.EventSystems;

public class PropogateDrag : MonoBehaviour
{
    //con este codigo overrideo el eventTrigger para que el scrollView Siga con su funcionalidad de scroll aunque el contenido posea eventtriggers
    //solo debo colocar este codigo en cada objeto en el container y decirles cual es su scrollview
    public UnityEngine.UI.ScrollRect scrollView;
    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entryBegin = new EventTrigger.Entry(), entryDrag = new EventTrigger.Entry(), entryEnd = new EventTrigger.Entry(), entrypotential = new EventTrigger.Entry()
            , entryScroll = new EventTrigger.Entry();

        entryBegin.eventID = EventTriggerType.BeginDrag;
        entryBegin.callback.AddListener((data) => { scrollView.OnBeginDrag((PointerEventData)data); });
        trigger.triggers.Add(entryBegin);

        entryDrag.eventID = EventTriggerType.Drag;
        entryDrag.callback.AddListener((data) => { scrollView.OnDrag((PointerEventData)data); });
        trigger.triggers.Add(entryDrag);

        entryEnd.eventID = EventTriggerType.EndDrag;
        entryEnd.callback.AddListener((data) => { scrollView.OnEndDrag((PointerEventData)data); });
        trigger.triggers.Add(entryEnd);

        entrypotential.eventID = EventTriggerType.InitializePotentialDrag;
        entrypotential.callback.AddListener((data) => { scrollView.OnInitializePotentialDrag((PointerEventData)data); });
        trigger.triggers.Add(entrypotential);

        entryScroll.eventID = EventTriggerType.Scroll;
        entryScroll.callback.AddListener((data) => { scrollView.OnScroll((PointerEventData)data); });
        trigger.triggers.Add(entryScroll);
    }
}