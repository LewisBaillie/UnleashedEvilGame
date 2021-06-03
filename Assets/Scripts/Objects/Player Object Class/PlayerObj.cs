using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;
using System;

public class PlayerObj : MoveableObj
{
    [SerializeField]
    private HandObj _Hand;
    [SerializeField]
    private LookX _LookX;
    [SerializeField]
    private LookY _LookY;
    [SerializeField]
    private Inventory _Inventory;
    private System.Timers.Timer _SaveTimer;
    [SerializeField]
    private double _SaveTimerLength;
    private bool _SaveNow;
    private bool _MidpointReached;
    private bool _EndReached;
    private bool _FirstUpdate = true;
    private bool _Paused = false;


    [Header("Save Settings")]
    [SerializeField]
    private GameObject[] _Doors;
    [SerializeField]
    private GameObject[] _Enemies;

    struct SaveFile
    {
        public SaveFile(Transform t, Inventory inventory, bool IsStanding, int AIP, GameObject[] Doors, GameObject[] Enemies)
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
            Standing = IsStanding;
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
        _Inventory = new Inventory();
        _SaveNow = false;
        _objType = ObjectType.PlayerObj;
        _SaveTimer = new System.Timers.Timer();
        _SaveTimer.Interval = _SaveTimerLength;
        _SaveTimer.Elapsed += OnTimedEvent;
        _SaveTimer.AutoReset = true;
        SetUpComponent();
        _LookY = GetComponentInChildren<LookY>();
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        _SaveNow = true;
    }


    void Update()
    {
        if(_FirstUpdate)
        {
            _Hand.AquireTorch();
            _FirstUpdate = false;
        }
        if(!_Paused)
        {
            CalculateMovement();
            HieghtMaipulation();
            CalculateLean();
            if (!CanStand() && !IsStanding())
            {
                if (!_SaveTimer.Enabled)
                    _SaveTimer.Enabled = true;
                _SaveTimer.Start();
            }
            else
            {
                _SaveTimer.Stop();
                _SaveTimer.Enabled = false;
            }
            //To be Removed outside of debugging
            if (Input.GetKeyDown(KeyCode.M))
            {
                _SaveNow = false;
                SaveGame();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
            if (_SaveNow)
            {
                SaveGame();
            }
        }
    }

    //https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity
    //https://www.c-sharpcorner.com/article/c-sharp-string-to-byte-array/
    //https://www.tutorialspoint.com/Timer-in-Chash


    public void LoadGame()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/gamesave.save");
            SaveFile save = JsonUtility.FromJson<SaveFile>(json);
            transform.position = save.Pos;
            transform.localScale = save.Scale;
            transform.rotation = save.Rotation;
            SetStanding(save.Standing);
            Vector3 LocalePos = transform.GetChild(0).localPosition;
            if (save.Standing && LocalePos.y > 0.5)
            {
                transform.GetChild(0).localPosition = new Vector3(LocalePos.x, 0.5f, LocalePos.z);
            }
            else
            {
                transform.GetChild(0).localPosition = new Vector3(LocalePos.x, -0.5f, LocalePos.z);
            }
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

    public void Paused(bool b)
    {
        _Paused = b;
        if(b)
        {
            _Hand.FreeCursor(b);
            _LookX.enabled = false;
            _LookY.enabled = false;
        }         
        else      
        {
            _Hand.FreeCursor(b);
            _LookX.enabled = true;
            _LookY.enabled = true;
        }
    }

    public void SaveGame()
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
        _SaveNow = false;
        Debug.Log("File saved at " + Application.persistentDataPath + "/gamesave.save");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Midpoint")
        {
            Debug.Log("Reached Midpoint");
            _MidpointReached = true;
        }
        if (other.tag == "Endpoint" && _MidpointReached)
        {
            _EndReached = true;
            Debug.Log("Reached endpoint");
        }
    }
    public Inventory ReturnInventory()
    {
        return _Inventory;
    }
}
