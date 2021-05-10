using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tuning : MonoBehaviour
{
    //��������� ��� �������� �������
    public const int FRONTBUMPER = 0;
    public const int REARBUMPER = 1;
    public const int SPOILER = 2;
    public const int ROOF = 3;
    public const int EXHAUST = 4;
    public const int WHEELS = 5;
    
    //����� Cars ������ ��� ���������� ������ � ���������� ���-�� �������
    [System.Serializable]
    public class Cars
    {
        //������ GameObject
        public GameObject car;
        public GameObject TuningCar;
        public int FrontBumpersCount;
        public string nameFronBumperDetail;
        public int RearBumpersCount;
        public string nameRearBumperDetail;
        public int SpoilersCount;
        public string nameSpoilerDetail;
        public int RoofCount;
        public string nameRoofDetail;
        public int ExhaustCount;
        public string nameExhaustDetail;
        //4 ������ ��� 1 ��� �����
        public int WheelTypeCount;
        public string nameWheels;
    }

    //����� ��� ���������� �������� �����
    [System.Serializable]
    public class Bumpers
    {
        public GameObject[] fronBumper;
        public GameObject[] rearBumper;
    }
    //����� ��� ���������� ��������� 
    [System.Serializable]
    public class Spoilers
    {
        public GameObject[] spoiler;
    }   
    //����� ��� ���������� ������ 
    [System.Serializable]
    public class Roofs
    {
        public GameObject[] roof;
    } 
    //����� ��� ���������� ������� 
    [System.Serializable]
    public class Exhausts
    {
        public GameObject[] exhaust;
    }

    //����� ��� ���������� �����
    [System.Serializable]
    public class Wheels
    {
        public GameObject[] FL;
        public GameObject[] FR;
        public GameObject[] RL;
        public GameObject[] RR;
    }

    //����� ��� ���������� ���������� 
    [System.Serializable]
    public class Materials
    {
        public Material body;
        public Material windows;
    }

    //���������� ��� �������� ���������� ������ � ������������� � �������� ������� � �������� ������ , �� ��������� ������ ����� 0 
    private int setFrontCarBumper;
    private int setRearCarBumper;
    private int setCarSpoiler;
    private int setCarRoof;
    private int setCarExhaust;
    private int setWheelsType;
    private int setCar;

    //���������� �������� �������
    [SerializeField]
    public Cars[] cars;
    public Bumpers[] bumpers;
    public Spoilers[] spoilers;
    public Roofs[] roofs;
    public Exhausts[] exhausts;
    public Wheels[] wheels;

    //���� ���� �������� ��� �������� ������ 
    public Materials[] materials = new Materials[1];


    //��������� ������� ��� ���������� ������ ������ SaveLoadManager � SaveLoadManage.Save
    // Save save = new Save();
    public Save save;
    public SaveLoadManager slManager;
    //���������� ��� �������� ��������� ���������
    private int categoreSelected;

    //������������ �� ������� ���� 
    private void Awake()
    {
        //������������� ���-�� ������� ��� �������� � ��� �������� ���������� ������� ��������
        for (int i = 0; i < cars.Length; i++)
        {
            bumpers[i].fronBumper = SetCountDetails(cars[i].TuningCar, cars[i].nameFronBumperDetail, cars[i].FrontBumpersCount, bumpers[i].fronBumper);
            bumpers[i].rearBumper = SetCountDetails(cars[i].TuningCar, cars[i].nameRearBumperDetail, cars[i].RearBumpersCount, bumpers[i].rearBumper);
            spoilers[i].spoiler = SetCountDetails(cars[i].TuningCar, cars[i].nameSpoilerDetail, cars[i].SpoilersCount, spoilers[i].spoiler);
            roofs[i].roof = SetCountDetails(cars[i].TuningCar, cars[i].nameRoofDetail, cars[i].RoofCount, roofs[i].roof);
            exhausts[i].exhaust = SetCountDetails(cars[i].TuningCar, cars[i].nameExhaustDetail, cars[i].ExhaustCount, exhausts[i].exhaust);
        }
        //�������� ������ �� ������ SaveLoadManager ��� ���������� ������
        slManager = GetComponent<SaveLoadManager>();
    }

    private void Start()
    {
        //������������� ����������� ������
        SetCurrentCar();
        //����� ������� �� ������ � ���������� � ������� 
        for (int i = 0; i < cars.Length; i++)
        {
            //��������� ��� ������ 1) ������ ������ 2)������ ������� ���� ��������� 3)�������� ������ �� ������ 4) ��������� ������ �� ����������
            GetComponentCar(cars[i].TuningCar, bumpers[i].fronBumper, cars[i].nameFronBumperDetail, setFrontCarBumper);
            GetComponentCar(cars[i].TuningCar, bumpers[i].rearBumper, cars[i].nameRearBumperDetail, setRearCarBumper);
            GetComponentCar(cars[i].TuningCar, spoilers[i].spoiler, cars[i].nameSpoilerDetail, setCarSpoiler);
            GetComponentCar(cars[i].TuningCar, roofs[i].roof, cars[i].nameRoofDetail, setCarRoof);
            GetComponentCar(cars[i].TuningCar, exhausts[i].exhaust, cars[i].nameExhaustDetail, setCarExhaust);

            //��������� ������ , 4 ������ ����� 1 ���� ����� 
            GetComponentCar(cars[i].TuningCar, wheels[i].FL, cars[i].nameWheels + "fl", setWheelsType, true, cars[i].WheelTypeCount);
            GetComponentCar(cars[i].TuningCar, wheels[i].FR, cars[i].nameWheels + "fr", setWheelsType, true, cars[i].WheelTypeCount);
            GetComponentCar(cars[i].TuningCar, wheels[i].RL, cars[i].nameWheels + "rl", setWheelsType, true, cars[i].WheelTypeCount);
            GetComponentCar(cars[i].TuningCar, wheels[i].RR, cars[i].nameWheels + "rr", setWheelsType, true, cars[i].WheelTypeCount);
        }
    }

    private void Update()
    {
        GetMaterialsCars();
        //���������� ����� ��������� ������ �������
        categoreSelected = UIButtons.SelectedCategoryTuning;
    }

    //�������� ����������� ������ � ������������ �������� � ��������� ����������
    public void LoadData(Save save)
    {
        setCar = save.currentCar;
        setFrontCarBumper = save.currentFrontBumper;
        setRearCarBumper = save.currentRearBumper;
        setCarSpoiler = save.currentSpoiler;
        setCarRoof = save.currentRoof;
        setCarExhaust = save.currentExhaust;

        //�������� ��������
        UIButtons.r = save.r;
        UIButtons.g = save.g;
        UIButtons.b = save.b;
        UIButtons.bW = save.bW;
        UIButtons.rW = save.rW;
        UIButtons.gW = save.gW;
        UIButtons.aW = save.aW;

    }

    //����� ��� ������ ����� ������
    public void nextCar()
    {
        setCar++;
        if (setCar == cars.Length)
            setCar = 0;
        if (setCar != 0)
        {
            cars[setCar - 1].car.SetActive(false);
        }
        else
        {
            cars[cars.Length - 1].car.SetActive(false);
            setCar = 0;
        }
        cars[setCar].car.SetActive(true);
        slManager.LoadGame();
    }
    //����� ��� ������ ����� ������
    public void prevCar()
    {
        setCar--;
        if (setCar < 0)
        {
            setCar = cars.Length - 1;
        }
        if (setCar == cars.Length - 1)
        {
            cars[0].car.SetActive(false);
        }
        if (setCar < cars.Length - 1)
        {
            cars[setCar + 1].car.SetActive(false);
        }   
        cars[setCar].car.SetActive(true);
    }
    //����� ��� ������ ������� � ���������� ������ 
    public void SelectCar()
    {
        //������ � ��������� ������ save 
        save.currentCar = setCar;
       
    }
    //����� ���������� �� ���� ��������� ������ � ���� ��� ������ , � ��������� �� � ������� 
    void GetComponentCar(GameObject car, GameObject[] Detail, string NameDetaill, int SetDetail, bool Wheels = false, int CountWheelsType = 0)
    {
        //��������� ��� ��������� ������ ������ ����� ����� ������ , ��� ��� ����� ����� ������ ������ � ��������
        if (!Wheels)
        {
            //������� ������� ������ � ���������� ������� � ������� 
            for (int y = 0; y < car.transform.childCount; y++)
            {
                for (int x = 0; x < car.transform.childCount; x++)
                {
                    //��������� ���� �� ����� ������ �� ������
                    if (car.transform.GetChild(y).name == NameDetaill + x)
                    {
                        if (Detail.Length > 0)
                        {
                            Detail[x] = car.transform.GetChild(y).gameObject;
                            //��� ��������� ������ ����������� , ��� ���� ��� � ������� �������� ����������� ��� ������������ 
                            Detail[x].SetActive(false);
                        }
                    }
                }
            }
            //�������� ���������� �� ����� ������ �� ���� ������ , ������� ��� ��������
            if (SetDetail >= Detail.Length)
            {
                if (Detail.Length > 0)
                    //���� �� ���������� , �� �������� ����������� ������
                    Detail[0].SetActive(true);
            }
            else
            {
                //���� ����������� ������ ���������� , ����������� ������ ���������� ��������
                Detail[SetDetail].SetActive(true);
            }
        }
        //����� ��� ��������� ������ � ������������� ���������� 
        else
        {
            for (int y = 0; y < car.transform.childCount; y++)
            {
                for (int x = 0; x < car.transform.childCount; x++)
                {
                    //��������� ���� �� ����� ������ �� ������
                    if (car.transform.GetChild(y).name == NameDetaill)
                    {
                        //��������� ���� �� � ������� ����� ��� ���������� ������
                        if (Detail.Length > 0)
                        {
                            //���������� �� ����� ������ ����� ������������ �� ���������� ����� �����
                            for (int i = 0; i < CountWheelsType; i++)
                            {
                               
                                Detail[i] = car.transform.GetChild(y).gameObject;
                            }
                            //��� ��������� ������� ����������� , ��� ���� ��� � ������� �������� ����������� ��� ������������ 
                            //Detail[x].SetActive(false);
                        }
                    }
                }
            }
        }
    }
    //����� ��� ��������� ����� ������ � ������
    public void GetMaterialsCars()
    {
        //���������� ��� �������� �������� �� ��������� 
        for (int i = 0;i < cars[setCar].car.transform.childCount;i++)
        {
            //��������� �������� ���� 
            if (cars[setCar].car.transform.GetChild(i).name == "door_lf" || cars[setCar].car.transform.GetChild(i).name == "door_lr" ||
                cars[setCar].car.transform.GetChild(i).name == "door_rr" || cars[setCar].car.transform.GetChild(i).name == "door_rf"
                || cars[setCar].car.transform.GetChild(i).name == "windscreen_ok")
            {
                materials[setCar].windows.color = new Color(UIButtons.rW, UIButtons.gW, UIButtons.bW, UIButtons.aW);
                
            }
            else 
            {
                materials[setCar].body.color = new Color(UIButtons.r, UIButtons.g, UIButtons.b);
            }
            SaveData();
        }
       
    }
    //����� ���������� ����� ���������� ������ ������  , ��� ���� , ��� � ����� ���� ��������� ��������� ������� � ��� �� ������ ������� ����� ���� ���������� �������
    public GameObject[] SetCountDetails(GameObject car, string nameDetail, int countDetail, GameObject[] Detail)
    {
        //��������������� ��������� ������� ��� ������ ������   
        for (int y = 0; y < car.transform.childCount; y++)
            {
                for (int x = 0; x < car.transform.childCount; x++)
                {
                //��������� ���� �� ����� ������ �� ������ � ������� ������ � �������� ����������� ������
                //������������� ��� ���� ��� � �� ������� ���� ��� ������� ���� ������ ��� ���������� 
                    if (car.transform.GetChild(y).name == nameDetail + x)
                    {
                        Detail = new GameObject[countDetail];
                    }
                }
        }
        //���������� ����� ��������� ������ ������ � ������� ����������� ������� 
        return Detail;
    }
    //����� ��� ������ ������������ �������� ��������
    public void SetCurrentCar()
    {
        for (int i = 0; i< cars.Length; i++)
        {
            cars[i].car.SetActive(false);
        }
        cars[setCar].car.SetActive(true);
    }
    //������� ��� ������ ������ , ������������� 
    public int ChangeNextDetails(int SetDetail, GameObject[] Details)
    {
        SetDetail++;
        if (SetDetail == Details.Length || SetDetail >= Details.Length)
        {
            SetDetail = 0;
        }
        if (SetDetail != 0)
        {
            //��������� ���� �� ������ ����� ������ � ������ , ��� � �� �������� ������ 
            if (Details.Length > 0)
            {
                Details[SetDetail - 1].SetActive(false);
            }   
        }
        else
        {
            if (Details.Length > 0)
            {
                //��������� ���� �� ������ ����� ������ � ������ , ��� � �� �������� ������ 
                Details[Details.Length - 1].SetActive(false);
            }
            SetDetail = 0;
        }
        if (Details.Length > 0)
            Details[SetDetail].SetActive(true);
        return SetDetail;
    }
    //���������� ������� �� ������ � ������������� ������� 
    public int ChangePreviewDetails(int SetDetail, GameObject[] Details)
    {
        SetDetail--;
        if (SetDetail < 0)
            //��������� ���� �� ������ ����� ������ � ������ , ��� � �� �������� ������ 
            if (Details.Length > 0)
                SetDetail = Details.Length - 1;
        if (SetDetail >= Details.Length - 1)
            //��������� ���� �� ������ ����� ������ � ������ , ��� � �� �������� ������ 
            if (Details.Length > 0)
                Details[0].SetActive(false);
        if (SetDetail < Details.Length - 1)
            if (Details.Length > 0)
                Details[SetDetail + 1].SetActive(false);
        if (Details.Length > 0)
            Details[SetDetail].SetActive(true);
        return SetDetail;
    }
    //����� ��� ������ ������������ �������
    public void NextDetail()
    {
        //���������� ����� �������� ������� 
        //int categoreSelected = UIButtons.SelectedCategoryTuning;
        if (categoreSelected == FRONTBUMPER)
            setFrontCarBumper = ChangeNextDetails(setFrontCarBumper, bumpers[setCar].fronBumper);
        if (categoreSelected == REARBUMPER)
            setRearCarBumper = ChangeNextDetails(setRearCarBumper, bumpers[setCar].rearBumper);
        if (categoreSelected == SPOILER)
            setCarSpoiler = ChangeNextDetails(setCarSpoiler, spoilers[setCar].spoiler);
        if (categoreSelected == ROOF)
            setCarRoof = ChangeNextDetails(setCarRoof, roofs[setCar].roof);
        if (categoreSelected == EXHAUST)
            setCarExhaust = ChangeNextDetails(setCarExhaust, exhausts[setCar].exhaust);
    }
    //����� ��� ������ ������������ �������
    public void PrevDetail()
    {
        // int categoreSelected = UIButtons.SelectedCategoryTuning;
        if (categoreSelected == FRONTBUMPER)
        {
            setFrontCarBumper = ChangePreviewDetails(setFrontCarBumper, bumpers[setCar].fronBumper);
            SaveData();
        } 
        if (categoreSelected == REARBUMPER)
            setRearCarBumper = ChangePreviewDetails(setRearCarBumper, bumpers[setCar].rearBumper);
        if (categoreSelected == SPOILER)
            setCarSpoiler = ChangePreviewDetails(setCarSpoiler, spoilers[setCar].spoiler);
        if (categoreSelected == ROOF)
            setCarRoof = ChangePreviewDetails(setCarRoof, roofs[setCar].roof);
        if (categoreSelected == EXHAUST)
            setCarExhaust = ChangePreviewDetails(setCarExhaust, exhausts[setCar].exhaust);
    }

    public void SaveData()
    {
        save.currentFrontBumper = setFrontCarBumper;
        save.currentRearBumper = setRearCarBumper;
        save.currentSpoiler = setCarSpoiler;
        save.currentRoof = setCarRoof;
        save.currentExhaust = setCarExhaust;

        save.rW = UIButtons.rW;
        save.gW = UIButtons.gW;
        save.bW = UIButtons.bW;
        save.aW = UIButtons.aW;

        save.b = UIButtons.b;
        save.g = UIButtons.g;
        save.r = UIButtons.r;

        slManager.SaveGame(save);
    }
    //����� ��� ��������� ������� � ��������� 
    public void SaveDetail()
    {
        //��������� ��������� ������ � ����� save SaveLoadManafer
        if (categoreSelected == FRONTBUMPER)
            save.currentFrontBumper = setFrontCarBumper;
        if (categoreSelected == REARBUMPER)
            save.currentRearBumper = setRearCarBumper;
        if (categoreSelected == SPOILER)
            save.currentSpoiler = setCarSpoiler;
        if (categoreSelected == ROOF)
            save.currentRoof = setCarRoof;
        if (categoreSelected == EXHAUST)
            save.currentExhaust = setCarExhaust;
        //�������� ����� ���������� � ���������� �������� ������ ��������� ������
        slManager.SaveGame(save);
    }
    //����� ��� ������ ����� , ��������� ����� � ������� ����� 
    public void StartRace()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
