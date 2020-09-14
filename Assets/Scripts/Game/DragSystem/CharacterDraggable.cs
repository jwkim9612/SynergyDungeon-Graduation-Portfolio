using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private Transform root;
    private Camera mainCamera;
    private UICharacter uiCharacter;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        root = transform.root;

        uiCharacter = GetComponent<UICharacter>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        root.BroadcastMessage("PointerDown", uiCharacter, SendMessageOptions.DontRequireReceiver);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!uiCharacter.isFightingOnBattlefield)
        {
            root.BroadcastMessage("BeginDrag", uiCharacter, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!uiCharacter.isFightingOnBattlefield)
        {
            Vector2 currentPos = Input.mousePosition;
            //mousePosition의 좌표를 카메라의 월드 좌표로 변환
            currentPos = mainCamera.ScreenToWorldPoint(currentPos);
            transform.position = currentPos;

            uiCharacter.FollowCharacter();

            root.BroadcastMessage("Drag", uiCharacter, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!uiCharacter.isFightingOnBattlefield)
        {
            root.BroadcastMessage("EndDrag", uiCharacter, SendMessageOptions.DontRequireReceiver);
        }
    }
}
