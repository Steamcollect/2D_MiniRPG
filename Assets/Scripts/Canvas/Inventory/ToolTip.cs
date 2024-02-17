using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public int maxCharacter;

    public TMP_Text headerField;
    public TMP_Text itemTypeField;
    public TMP_Text contentField;

    public TMP_Text damageField;
    public TMP_Text healthField;

    LayoutElement layoutElement;

    Vector2 posOffset = new Vector2(-30, 0);

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
    }

    private void Update()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > maxCharacter || contentLength > maxCharacter) ? true : false;
    }

    // from ToolTipSystem
    public void SetVisual(ItemData currentItem, Vector2 slotPos)
    {
        UiTextManager.instance.ResetText();

        transform.position = slotPos + posOffset;

        UiTextManager.instance.SetText( headerField, currentItem.itemName);
        UiTextManager.instance.SetText( contentField, currentItem.itemDescription);

        if (currentItem.itemType == ItemType.Weapon) UiTextManager.instance.SetText(itemTypeField, "Type : " + currentItem.weaponType.ToString());
        else itemTypeField.text = "";

        if (currentItem.damageGiven != 0) UiTextManager.instance.SetText(damageField, "Degats + " + currentItem.damageGiven.ToString());
        else damageField.text = "";
        if (currentItem.healthGiven != 0) UiTextManager.instance.SetText(healthField, "Vie + " + currentItem.healthGiven.ToString());
        else healthField.text = "";        
    }
}
