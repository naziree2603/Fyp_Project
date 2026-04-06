using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Items> CollectedItems = new List<Items>();
    public Transform Container;
    public GameObject BtnItem;
    [SerializeField] private GameObject Inventory;
    private const string InventoryFilePath = "inventory.dat";
    public Transform swordPlace, shieldPlace;

    private bool isNewGameSession = false;
    private bool hasInitialized = false;

    private GameObject EquipedSword;
    private GameObject EquipedShield;

    private int EquipedSwordID;
    private int EquipedShieldID;

    GameObject Player;
    PlayerAttack attack;
    PlayerHealth health;

    public void InitializeAfterSpawn()
    {
        Debug.Log("Final Init After Player Ready");

        ShowItems();
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded → Reconnecting UI");
        FindUI();
    }



    void FindUI()
    {
        UIReferences ui = FindFirstObjectByType<UIReferences>();

        if (ui != null)
        {
            Container = ui.content;
            Inventory = ui.bg;

            Debug.Log("UI Connected Successfully");
        }
        else
        {
            Debug.LogError("UIReferences NOT FOUND!");
        }
    }

    Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
    public void SetPlayer(GameObject player)
    {
        Player = player;

        attack = Player.GetComponent<PlayerAttack>();
        health = Player.GetComponent<PlayerHealth>();



        
        EquipPoint[] points = Player.GetComponentsInChildren<EquipPoint>();

        foreach (EquipPoint p in points)
        {
            if (p.type == ItemType.Sword)
                swordPlace = p.transform;

            if (p.type == ItemType.Shield)
                shieldPlace = p.transform;
        }


        // 🔥 CHECK NEW GAME OR CONTINUE
        // 🔥 IMPORTANT: only run ONCE
        if (!hasInitialized)
        {
            hasInitialized = true;

            int isNewGame = PlayerPrefs.GetInt("IsNewGame", 0);

            if (isNewGame == 1)
            {
                Debug.Log("NEW GAME → Reset Inventory");

                CollectedItems.Clear();
                EquipedSwordID = -1;
                EquipedShieldID = -1;

                string path = Path.Combine(Application.persistentDataPath, InventoryFilePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                PlayerPrefs.SetInt("IsNewGame", 0);
            }
            else
            {
                Load();
            }
        }



    }

    private void Start()
    {
        
        
    }

    public void AddItem(Items item)
    {
        CollectedItems.Add(item);

        Save();

        ShowItems(); // 🔥 ALWAYS CALL

    }



    public void ShowItems()
    {
        if (Container == null)
        {
            Debug.LogError("Container NULL → cannot show items");
            return;
        }

        foreach (Transform item in Container)
        {
            Destroy(item.gameObject);
        }
        foreach (Items item in CollectedItems)
        {
            GameObject GO = Instantiate(BtnItem, Container);
            var icon = GO.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = item.ItemSprite;

            //delete btn
            var BtnRemove = GO.transform.Find("BtnDelete").GetComponent<Button>();
            BtnRemove.onClick.AddListener(() => RemoveItem(item, GO));

            var BtnUse = GO.transform.Find("BtnUseItem").GetComponent<Button>();
            BtnUse.onClick.AddListener(() => UseItem(item));


        }
    }

    public void RemoveItem(Items item, GameObject btnItem)
    {
  

        switch (item.itemType)
        {
            case ItemType.Sword:
                if (EquipedSwordID == item.ID)
                {
                    EquipedSwordID = -1;

                    if (EquipedSword != null)
                    {
                        Destroy(EquipedSword);
                        EquipedSword = null;
                    }

                    attack.UpdateSwordDamage();
                }
                break;

            case ItemType.Shield:
                if (EquipedShieldID == item.ID)
                {
                    EquipedShieldID = -1;

                    if (EquipedShield != null)
                    {
                        Destroy(EquipedShield);
                        EquipedShield = null;
                    }

                    health.UpdateDefenceValue();
                }
                break;
            }

        CollectedItems.Remove(item);
        Destroy(btnItem);
        ShowItems();
        Save();
    }

    public void CloseInventory()
    {
        if (Inventory != null)
            Inventory.SetActive(false);
        

    }

    public void OpenInventory()
    {
        

        if (Inventory == null || Container == null)
        {
            Debug.LogWarning("UI not ready");
            return;
        }

        Inventory.SetActive(true);
        
        ShowItems();
        


    }
    public int GetSwordDamage()
    {
        Items sword = GetItemByID(EquipedSwordID);
        return sword != null? sword.value : 0; 
    }

    public int GetShieldValue()
    {
        Items shield = GetItemByID(EquipedShieldID);
        return shield != null ? shield.value : 0;
    }
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        string FilePath = Path.Combine(Application.persistentDataPath, InventoryFilePath);
        Stream stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);

        InventoryContainer container = new InventoryContainer();

        container.EquipedSwordID = EquipedSwordID;
        container.EquipedShieldID = EquipedShieldID;






        foreach (Items item in CollectedItems)
        {
            container.ItemId.Add(item.ID);
        }

        formatter.Serialize(stream, container);
        stream.Close();

    }

    private void Load()
    {
        string FilePath = Path.Combine(Application.persistentDataPath, InventoryFilePath);

        if (File.Exists(FilePath))
        {

            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

            InventoryContainer container = (InventoryContainer)formatter.Deserialize(stream);

            stream.Close();

            CollectedItems.Clear();

            EquipedSwordID = container.EquipedSwordID;
            EquipedShieldID = container.EquipedShieldID;

            foreach (int itemId in container.ItemId)
            {
                Items item = GetItemByID(itemId);
                if (item != null)
                {
                    CollectedItems.Add(item);
                }
            }
            ShowItems();
            ShowPreviousEquipedItems();

        }

    }

    private void ShowPreviousEquipedItems()
    {
        if (EquipedSwordID != -1)
        {
            Items swordItem = GetItemByID(EquipedSwordID);
            if (swordItem != null)
            {
                UseItem(swordItem);
            }
        }

        if (EquipedShieldID != -1)
        {
            Items shieldItem = GetItemByID(EquipedShieldID);
            if (shieldItem != null)
            {
                UseItem(shieldItem);
            }
        }
    }

    private Items GetItemByID(int id)
    {
        Items[] allItems = Resources.LoadAll<Items>("Items");

        foreach (Items item in allItems)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void UseItem(Items item)
    {
        if (swordPlace == null || shieldPlace == null)
        {
            Debug.LogWarning("EquipPoints not ready → blocking UseItem");
            return;
        }

        Transform targetPlace = null;
        GameObject previousItem = null;

     

        switch (item.itemType)
        {
            case ItemType.Sword:
                targetPlace = swordPlace;
                previousItem = EquipedSword;
                EquipedSwordID = item.ID;
                break;

            case ItemType.Shield:
                targetPlace = shieldPlace;
                previousItem = EquipedShield;
                EquipedShieldID = item.ID;
                break;

            default:
                break;
        }

        if (targetPlace == null)
        {
            Debug.LogWarning("EquipPoint lost → reconnecting player");

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                SetPlayer(playerObj);
            }

            return;
        }





        if (previousItem != null)
        {
            Destroy(previousItem);
        }

        GameObject GO = Instantiate(item.ItemPrefab, targetPlace.position, Quaternion.identity);
        GO.transform.SetParent(targetPlace);
        GO.transform.localRotation = item.ItemPrefab.transform.localRotation;
        GO.transform.localPosition = item.ItemPrefab.transform.localPosition;
        GO.transform.localScale = item.ItemPrefab.transform.localScale;


        switch (item.itemType)
        {
            case ItemType.Sword:
                EquipedSword = GO;

                

                attack.UpdateSwordDamage();


                break;

            case ItemType.Shield:
                EquipedShield = GO;

                
                
                health.UpdateDefenceValue();
                break;

         

        }

        Save();

    }

    

}
