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

    [SerializeField]
    struct SaveFile
    {
        public SaveFile(Transform t, Inventory inventory, bool IsCrouched, int AIP)
        {
            I = new GameObject[10];
            Pos = t.position;
            Scale = t.localScale;
            Rotation = t.rotation;
            for (int i = 0; i < I.Length; i++)
            {
                I[i] = inventory.GrabObjectFromInvent(i);
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

        }
    }

    private void SaveGame()
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            System.IO.File.Delete(Application.persistentDataPath + "/gamesave.save");
            SaveFile save = new SaveFile(transform, _Inventory, IsStanding(), _Inventory.ActivePosition());
            string json = JsonUtility.ToJson(save);
            System.IO.FileStream file = System.IO.File.Create(Application.persistentDataPath + "/gamesave.save");
            byte[] bytes = Encoding.ASCII.GetBytes(json);
            file.Write(bytes, 0, bytes.Length);
            file.Close();
        }
        else
        {
            SaveFile save = new SaveFile(transform, _Inventory, IsStanding(), _Inventory.ActivePosition());
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
