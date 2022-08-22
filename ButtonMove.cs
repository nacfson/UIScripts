using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonMove : MonoBehaviour
{
    public ItemUsePanelUI ItemUsePanelUI
    {
        get
        {
            _itemUsePanelUI ??= FindObjectOfType<ItemUsePanelUI>();
            return _itemUsePanelUI;
        }
    }

    [SerializeField] private ItemUsePanelUI _itemUsePanelUI;
    [SerializeField] private ButtonSelect _inventoryButtonSelect;
    [SerializeField] private ButtonSelect buttonPanelButtonSelect;

    public void MoveAdmit()
    {
        if(ItemUsePanelUI?.panelOn == true)
        {
            _inventoryButtonSelect.buttonMove = false;
        }
        else
        {
            _inventoryButtonSelect.buttonMove = true;
        }
    }
    private void Update()
    {
        MoveAdmit();
    }
}
