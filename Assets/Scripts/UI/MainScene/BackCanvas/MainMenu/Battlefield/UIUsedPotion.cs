using UnityEngine;
using UnityEngine.UI;

public class UIUsedPotion : MonoBehaviour
{
    [SerializeField] private Image potionImage = null;
    [SerializeField] private Button showInfoButton = null;
    [SerializeField] private UIPotionInfo uiPotionInfo = null;

    [SerializeField] private Camera cam = null;

    public void Initialize()
    {
        SetShowInfoButton();
        UpdatePotionImageAndPotionInfo();

        PotionManager.Instance.OnPotionChanged += UpdatePotionImageAndPotionInfo;
    }

    private void SetShowInfoButton()
    {
        showInfoButton.onClick.AddListener(() =>
        {
            uiPotionInfo.OnShow();
        });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!TransformService.ContainPos(potionImage.transform as RectTransform, Input.mousePosition, cam))
            {
                if (uiPotionInfo.gameObject.activeSelf)
                {
                    uiPotionInfo.OnHide();
                }
            }
        }
    }

    public void UpdatePotionImageAndPotionInfo()
    {
        if (PotionManager.Instance.HasPotionInUse())
        {
            var potionDataSheet = DataBase.Instance.potionDataSheet;
            if (potionDataSheet == null)
            {
                Debug.Log("potionDataSheet is null!");
                return;
            }

            var potionId = PotionManager.Instance.potionIdInUse;
            if (potionDataSheet.TryGetPotionData(potionId, out var potionData))
            {
                potionImage.sprite = potionData.Image;
                uiPotionInfo.SetDescriptionText(potionData);
            }
        }
        else
        {
            potionImage.sprite = PotionService.DEFAULT_IMAGE;
            uiPotionInfo.SetDescriptionText(PotionService.DEFAULT_POTION_DESCRIPTION);
        }
    }

    private void OnDestroy()
    {
        if (PotionManager.IsAlive)
        {
            PotionManager.Instance.OnPotionChanged -= UpdatePotionImageAndPotionInfo;
        }
    }
}
