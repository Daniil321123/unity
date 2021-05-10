using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerTest : MonoBehaviour
{
    [System.Serializable]
    public class Cars
    {
        public GameObject car;
        public string CarName;
        public int FrontBumpersCount;
        public int RearBumpersCount;
        public int SpoilersCount;
        public int RoofsCount;
        public int ExhaustsCount;
    }
    [SerializeField]
    public Cars[] cars;
    //������� ��� ������� ������ 
    public GameObject[] frontBumpers;
    public GameObject[] reaeBumpers;
    public GameObject[] spoilers;
    public GameObject[] roofs;
    public GameObject[] exhausts;

    //���������� ��� �������� ���������� ������ � ������������� � �������� ������� � �������� ������
    private int setFrontBumper;
    private int setRearBumper;
    private int setSpoiler;
    private int setRoof;
    private int setExhaust;
    private int setCar;

    //��������� ������� ��� ���������� ������ ������ SaveLoadManager � SaveLoadManage.Save
    public SaveLoadManager slManager;
    public GameObject garage;
    private void Awake()
    {
       
    }

    private void Start()
    {
        //����� ������ �� ����� � ����� ������, ����� ������� ����������� � ����� ����� ����� 
        Instantiate(cars[setCar].car);
        
        //�������� ������ �� ������������ ������ , ��� ���������� ���� ��������� �������
        GameObject car = GameObject.Find(cars[setCar].car.name + "(Clone)");
        //������������� ���������� ������� ��� �������� � ��������� ������������ �������
        //frontBumpers = SetCountComponent(car, "bumberF_", frontBumpers, cars[setCar].FrontBumpersCount);
        //reaeBumpers = SetCountComponent(car, "bumberR_", reaeBumpers, cars[setCar].RearBumpersCount);
        //spoilers = SetCountComponent(car, "spoiler_", spoilers, cars[setCar].SpoilersCount);
        //roofs = SetCountComponent(car, "roof_", roofs, cars[setCar].RoofsCount);
        //exhausts = SetCountComponent(car, "exhaust_", exhausts, cars[setCar].ExhaustsCount);
        GetComponentCar();
    }

    public void LoadData(Save save)
    {
        setCar = save.currentCar;
        setFrontBumper = save.currentFrontBumper;
        setRearBumper = save.currentRearBumper;
        setSpoiler = save.currentSpoiler;
        setRoof = save.currentRoof;
        setExhaust = save.currentExhaust;
    }


    void GetComponentCar()
    {
        //for all car find bumper and add bunpers array
        GameObject car = GameObject.Find(cars[setCar].car.name + "(Clone)");
        if (car)
        {
            SetupDetailsInArray(car, "bumberF_", frontBumpers, setFrontBumper);
            SetupDetailsInArray(car, "bumberR_", reaeBumpers, setRearBumper);
            SetupDetailsInArray(car, "spoiler_", spoilers, setSpoiler);
            SetupDetailsInArray(car, "roof_", roofs, setRoof);
            SetupDetailsInArray(car, "exhaust_", exhausts, setExhaust);
        }
    }

    //����� ���� � ������ ������ ��� ������ � ��������� � ������ 
    public void SetupDetailsInArray(GameObject Car, string NameDetail, GameObject[] Details, int SaveDetail)
    {
        for (int i=0;i<Car.transform.childCount;i++)
        {
            if (Car.transform.GetChild(i).name == cars[setCar].CarName)
            {
                for (int y=0;y<Car.transform.GetChild(i).transform.childCount; y++)
                {
                    for (int x=0;x< Car.transform.GetChild(i).transform.childCount; x++)
                    {
                        if (Car.transform.GetChild(i).transform.GetChild(y).name == NameDetail + x)
                        {
                            Car.transform.GetChild(i).transform.GetChild(y).gameObject.SetActive(false);
                        }
                        if (Car.transform.GetChild(i).transform.GetChild(y).name == NameDetail + x && x == SaveDetail)
                        {
                            Details[0] = Car.transform.GetChild(i).transform.GetChild(y).gameObject;
                            Details[0].SetActive(true);
                        }
                    }
                }
            } 
        }
    }
    //����� ������������� ����� ������� ��� ������ ������
    public GameObject[] SetCountComponent(GameObject Car, string NameDetail, GameObject[] Details, int CountDetails)
    { 
        for (int i=0;i<Car.transform.childCount;i++)
        {
            for (int y=0;y<Car.transform.GetChild(i).childCount;y++)
            {
                for (int x=0;x<Car.transform.GetChild(i).transform.childCount;x++)
                {
                    if (Car.transform.GetChild(i).transform.GetChild(y).name== NameDetail + x)
                    {
                        Details = new GameObject[CountDetails];
                    }
                }
            }
        } 
        return Details;
    }

    //public void SetupElements()
    //{
    //    Debug.Log(frontBumpers.Length);
    //    for (int i = 0;i < frontBumpers.Length;i++)
    //    {
    //        frontBumpers[i].SetActive(false);
    //    }
    //    if(frontBumpers.Length > 0)
    //        frontBumpers[setFrontCarBumper].SetActive(true);
    //}
}
