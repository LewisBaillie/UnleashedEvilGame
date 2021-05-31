using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObj : MoveableObj
{
    private HandObj _Hand;
    private Inventory _Inventory;

    [Header("Save Settings")]
    [SerializeField]
    private GameObject[] _Doors;
    [SerializeField]
    private GameObject[] _Enemies;

    struct SaveFile
    {
        public SaveFile(Transform t, Inventory inventory, bool IsCrouched, int AIP, GameObject[] Doors, GameObject[] Enemies)
        {
            DoorsActive = new List<bool>();
            EnemyPos = new List<Vector3>();
            EnemyScale = new List<Vector3>();
            EnemyRot = new List<Quaternion>();
            I = new GameObject[10];
            Pos = t.position;
            Scale = t.localScale;
            Rotation = t.rotation;
            for (int i = 0; i < I.Length; i++)
            {
                I[i] = inventory.GrabObjectFromInvent(i);
            }
            foreach (GameObject item in Doors)
            {
                if (item.activeInHierarchy)
                {
                    DoorsActive.Add(true);
                }
                else
                {
                    DoorsActive.Add(false);
                }
            }
            foreach (GameObject item in Enemies)
            {
                EnemyPos.Add(item.transform.position);
                EnemyScale.Add(item.transform.localScale);
                EnemyRot.Add(item.transform.rotation);
            }
            Standing = IsCrouched;
            ActiveInventoryPos = AIP;
        }
        public Vector3 Pos;
        public Vector3 Scale;
        public Quaternion Rotation;
        public GameObject[] I;
        public bool Standing;
        public int ActiveInventoryPos;
        public List<bool> DoorsActive;
        public List<Vector3> EnemyPos;
        public List<Vector3> EnemyScale;
        public List<Quaternion> EnemyRot;
        
    }


    virtual public void SetUpComponent()
    {
        _objType = ObjectType.PlayerObj;
        _type = "Player";
    }

    void Start()
    {
        Application.persistentDataPath.Replace(Application.persistentDataPath, "C:/Users/h014086j/Documents");
        _Inventory = new Inventory();
        SetUpComponent();
    }

    void Update()
    {
        CalculateMovement();
        HieghtMaipulation();
        CalculateLean();
        if(!CanStand())
        {
            SaveGame();
        }
        //To be Removed outside of debugging
        if(Input.GetKeyDown(KeyCode.M))
        {
            SaveGame();
            SceneManager.LoadScene("NewTestScene");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    //https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity
    //https://www.c-sharpcorner.com/article/c-sharp-string-to-byte-array/

    private void LoadGame()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/gamesave.save");
            SaveFile save = JsonUtility.FromJson<SaveFile>(json);
            transform.position = save.Pos;
            transform.localScale = save.Scale;
            transform.rotation = save.Rotation;
            for (int i = 0; i < save.DoorsActive.Count; i++)
            {
                _Doors[i].SetActive(save.DoorsActive[i]);
            }
            for (int i = 0; i < save.DoorsActive.Count; i++)
            {
                _Doors[i].SetActive(save.DoorsActive[i]);
            }
            for (int i = 0; i < save.EnemyPos.Count; i++)
            {
                _Enemies[i].transform.position = save.EnemyPos[i];
                _Enemies[i].transform.localScale = save.EnemyScale[i];
                _Enemies[i].transform.rotation = save.EnemyRot[i];
            }
        }
    }

    public void NewObjectInHand()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            System.IO.File.Delete(Application.persistentDataPath + "/gamesave.save");
            SaveFile save = new SaveFile(transform, _Inventory, IsStanding(), _Inventory.ActivePosition(), _Doors, _Enemies);
            string json = JsonUtility.ToJson(save);
            System.IO.FileStream file = System.IO.File.Create(Application.persistentDataPath + "/gamesave.save");
            byte[] bytes = Encoding.ASCII.GetBytes(json);
            file.Write(bytes, 0, bytes.Length);
            file.Close();
        }
        else
        {
            SaveFile save = new SaveFile(transform, _Inventory, IsStanding(), _Inventory.ActivePosition(), _Doors, _Enemies);
            string json = JsonUtility.ToJson(save);
            System.IO.FileStream file = System.IO.File.Create(Application.persistentDataPath + "/gamesave.save");
            byte[] bytes = Encoding.ASCII.GetBytes(json);
            file.Write(bytes, 0, bytes.Length);
            file.Close();
        }
        Debug.Log("File saved at " + Application.persistentDataPath + "/gamesave.save");
    }

    public Inventory ReturnInventory()
    {
        return _Inventory;
    }
}
