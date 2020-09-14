using UnityEngine;

public class TransformService
{
    public static bool ContainPos(RectTransform rt, Vector2 pos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rt, pos);
    }

    public static bool ContainPos(RectTransform rt, Vector2 pos, Camera camera)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rt, pos, camera);
    }

    public static void SetFullSize(RectTransform rt)
    {
        rt.offsetMax = new Vector2(0.0f, 0.0f);
        rt.offsetMin = new Vector2(0.0f, 0.0f);
    }

    // 부모를 설정하고 그 부모를 기준으로 배치
    public static void SetParentAndMoveRelativeToParent(Transform originTransform, GameObject parent)
    {
        originTransform.SetParent(parent.transform);

        RectTransform rect = originTransform as RectTransform;
        rect.offsetMax = new Vector2(rect.offsetMax.x, 0.0f);
        rect.offsetMin = new Vector2(rect.offsetMin.x, 0.0f);
    }
}