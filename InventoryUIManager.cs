using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{ 
    public InventorySO InventorySO
    {
        get
        {
            return _inventorySO;
        }
    }
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private GameObject _inventoryBox;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _statPanel;
    [SerializeField] private GameObject _weaponInvPanel;
    [SerializeField] private GameObject _panelCanvas;
    [SerializeField] private GameObject _buttonPanel;
    public GameObject _informationPanel;
    [SerializeField] private InformationPanel _information;
    public ButtonSelect inventoryButtonSelect;
    public ButtonSelect buttonPanelButtonSelect;
    public bool panelOn;
    
    public InventorySceneManager inventorySceneManager;


    [SerializeField]
    private List<Button> inventoryBoxes = new List<Button>();

    public int activeTrueBoxCount;
    private int onBoxCount;

    [SerializeField] private StatPanelManager _statPanelManager;


    private void Start()
    {
        onBoxCount = 0;
        
        _informationPanel.SetActive(false);
        _information = _informationPanel.GetComponent<InformationPanel>();
        _buttonPanel.SetActive(true);
        panelOn = false;
        MakeInventoryBox(21);
        ActiveFalsePanel();
        ActiveTrueBox(_inventorySO._itemDataList.Count);
        CheckActiveTrueBox();
    }

    private void Update()
    {
        _inventorySO.onDataChanged = true;
        if (_inventorySO.onDataChanged == true)
        {
            CheckActiveTrueBox();
            InventoryDataChanged();
            _inventorySO.onDataChanged = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) &&_statPanelManager.JokBoPanelOn == false)
        {
            ActiveFalsePanel();
        }
    }
    


    /// <summary>
    /// �ڽ� �������� ���� InventorySO �� ���� �����ͷ� �ٲ��ش�
    /// </summary>
    public void BoxDataUpdate()
    {
        for (int i= 0; i< inventoryBoxes.Count; i++)
        {
            inventoryBoxes[i].GetComponent<InventoryBoxData>().itemName = _inventorySO._itemDataList[i]._name;
            inventoryBoxes[i].GetComponent<InventoryBoxData>().count = _inventorySO._itemDataList[i]._count;
        }
    }



    public void DataToInformationPanel()
    {
        _information._name = inventoryBoxes[inventoryButtonSelect.buttonCount].GetComponent<InventoryBoxData>().itemName;
        _information.price = inventoryBoxes[inventoryButtonSelect.buttonCount].GetComponent<InventoryBoxData>().count;
    }
    /// <summary>
    /// �κ��丮 ������ ������ŭ �����۹ڽ��� ���ش�
    /// </summary>
    
    /// <summary>
    /// ������ �ڽ��� �����Ѵ�
    /// </summary>

    private void MakeInventoryBox(int count, int start = 0)
    {
        for (int i = start; i < count; i++)
        {
            Button obj = Instantiate(_inventoryBox, Vector3.zero, Quaternion.identity).GetComponent<Button>();
            obj.transform.SetParent(_inventoryPanel.transform, true);

            int idx = i;
            obj.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    OnGetBoxData(_inventorySO._itemDataList[idx]);
                });
            obj.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    DataToInformationPanel();
                });
            inventoryButtonSelect.buttonList.Add(obj);
            inventoryBoxes.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
    void InventoryDataChanged()
    {
        int dataListCount = _inventorySO._itemDataList.Count;
        int panelChildCount = _inventoryPanel.transform.childCount;
        if(dataListCount > onBoxCount)
        {
            ActiveTrueBox(dataListCount);
        }
        if (dataListCount < panelChildCount)
        {
            ActiveFalseBox(dataListCount,panelChildCount);
            onBoxCount = 0;
            CheckActiveTrueBox();
        }
        
        
    }

    void CheckActiveTrueBox()
    {
        for (int i = 0; i < inventoryBoxes.Count; i++)
        {
            if (inventoryBoxes[i].gameObject.activeSelf == true)
            {
                onBoxCount++;
            }
        }
    }

    void ActiveFalseBox(int dataListCount, int panelChildCount)
    {
        for(int temp = panelChildCount-1; temp > dataListCount-1; temp--)
        {
            _inventoryPanel.transform.GetChild(temp).gameObject.SetActive(false);
        }

    }

    void ActiveTrueBox(int dataListCount, int panelChildCount = 0)
    {
        int temp = panelChildCount;
        for(int i= panelChildCount; i < dataListCount; i++)
        {
            _inventoryPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    // void DataChanged()
    // {
    //     int dataListCount = _inventorySO._itemDataList.Count;
    //     int panelChildCount = _inventoryPanel.transform.childCount;
    //     if(dataListCount > panelChildCount)
    //     {
    //         MakeInventoryBox(dataListCount- panelChildCount);
    //         //ActiveTrueBox(dataListCount,panelChildCount);
    //     }
    //     if(dataListCount < panelChildCount)
    //     {
    //         ActiveFalseBox(dataListCount,panelChildCount);
    //     }
    // }

    public void OnStatPanel()
    {
        _statPanel.SetActive(true);
        _buttonPanel.SetActive(false);
        buttonPanelButtonSelect.buttonMove = false;
        buttonPanelButtonSelect.buttonCount = 0;
        Debug.Log("stat");
        
    }

    public void OnInventoryPanel()
    {
        _inventoryPanel.SetActive(true);
        _informationPanel.SetActive(true);
        _buttonPanel.SetActive(false);
        inventoryButtonSelect.buttonMove = true;
        inventoryButtonSelect.buttonCount = 0;
        buttonPanelButtonSelect.buttonMove = false;
    }

    public void OnWeaponInvPanel()
    {
        _weaponInvPanel.SetActive(true);
        _informationPanel.SetActive(true);
        _buttonPanel.SetActive(false);
        buttonPanelButtonSelect.buttonCount = 0;
        buttonPanelButtonSelect.buttonMove = false;
    }

    public void ActiveFalsePanel()
    {
        _inventoryPanel.SetActive(false);
        _statPanel.SetActive(false);
        _weaponInvPanel.SetActive(false);
        _buttonPanel.SetActive(true);
        _informationPanel.SetActive(false);
        buttonPanelButtonSelect.buttonMove = true;
    }

    public void IPActiveTrue()
    {
        _informationPanel.SetActive(true);
    }

    public void OnGetBoxData(ItemData data)
    {
        Debug.Log($"count : {data._count}, type : {((EItem)data._eItem).ToString()}, name : {data._name}");
    }

}
