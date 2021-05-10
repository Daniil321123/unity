using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tuning : MonoBehaviour
{
    //константы для категорй тюнинга
    public const int FRONTBUMPER = 0;
    public const int REARBUMPER = 1;
    public const int SPOILER = 2;
    public const int ROOF = 3;
    public const int EXHAUST = 4;
    public const int WHEELS = 5;
    
    //класс Cars создан для добавления машины и добавления кол-ва деталей
    [System.Serializable]
    public class Cars
    {
        //машина GameObject
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
        //4 колеса это 1 тип колес
        public int WheelTypeCount;
        public string nameWheels;
    }

    //класс для добавления бамперов машин
    [System.Serializable]
    public class Bumpers
    {
        public GameObject[] fronBumper;
        public GameObject[] rearBumper;
    }
    //класс для добавления спойлеров 
    [System.Serializable]
    public class Spoilers
    {
        public GameObject[] spoiler;
    }   
    //класс для добавления ковшей 
    [System.Serializable]
    public class Roofs
    {
        public GameObject[] roof;
    } 
    //класс для добавления выхлопа 
    [System.Serializable]
    public class Exhausts
    {
        public GameObject[] exhaust;
    }

    //класс для добавления колес
    [System.Serializable]
    public class Wheels
    {
        public GameObject[] FL;
        public GameObject[] FR;
        public GameObject[] RL;
        public GameObject[] RR;
    }

    //класс для добавления материалов 
    [System.Serializable]
    public class Materials
    {
        public Material body;
        public Material windows;
    }

    //Переменные для загрузки сохраненнх данных и использования в скриптах тюнинга и загрузке данных , по стандарту всегда равны 0 
    private int setFrontCarBumper;
    private int setRearCarBumper;
    private int setCarSpoiler;
    private int setCarRoof;
    private int setCarExhaust;
    private int setWheelsType;
    private int setCar;

    //добавление массивов классов
    [SerializeField]
    public Cars[] cars;
    public Bumpers[] bumpers;
    public Spoilers[] spoilers;
    public Roofs[] roofs;
    public Exhausts[] exhausts;
    public Wheels[] wheels;

    //пока один материал для покарски кузова 
    public Materials[] materials = new Materials[1];


    //созданные обьекты для сохранения данных класса SaveLoadManager и SaveLoadManage.Save
    // Save save = new Save();
    public Save save;
    public SaveLoadManager slManager;
    //переменная для хранения выбранной категории
    private int categoreSelected;

    //запускаеться до старата игры 
    private void Awake()
    {
        //устанавливает кол-во деталей для массивов и для удобства заполнения массива деталями
        for (int i = 0; i < cars.Length; i++)
        {
            bumpers[i].fronBumper = SetCountDetails(cars[i].TuningCar, cars[i].nameFronBumperDetail, cars[i].FrontBumpersCount, bumpers[i].fronBumper);
            bumpers[i].rearBumper = SetCountDetails(cars[i].TuningCar, cars[i].nameRearBumperDetail, cars[i].RearBumpersCount, bumpers[i].rearBumper);
            spoilers[i].spoiler = SetCountDetails(cars[i].TuningCar, cars[i].nameSpoilerDetail, cars[i].SpoilersCount, spoilers[i].spoiler);
            roofs[i].roof = SetCountDetails(cars[i].TuningCar, cars[i].nameRoofDetail, cars[i].RoofCount, roofs[i].roof);
            exhausts[i].exhaust = SetCountDetails(cars[i].TuningCar, cars[i].nameExhaustDetail, cars[i].ExhaustCount, exhausts[i].exhaust);
        }
        //получаем ссылку на скрипт SaveLoadManager для сохранения данных
        slManager = GetComponent<SaveLoadManager>();
    }

    private void Start()
    {
        //устанавливает сохраненную машину
        SetCurrentCar();
        //поиск деталей на машине и добавления в массивы 
        for (int i = 0; i < cars.Length; i++)
        {
            //параметры для метода 1) обьект мащтны 2)массив который надо заполнять 3)название детали на машине 4) выбранная деталь из сохранений
            GetComponentCar(cars[i].TuningCar, bumpers[i].fronBumper, cars[i].nameFronBumperDetail, setFrontCarBumper);
            GetComponentCar(cars[i].TuningCar, bumpers[i].rearBumper, cars[i].nameRearBumperDetail, setRearCarBumper);
            GetComponentCar(cars[i].TuningCar, spoilers[i].spoiler, cars[i].nameSpoilerDetail, setCarSpoiler);
            GetComponentCar(cars[i].TuningCar, roofs[i].roof, cars[i].nameRoofDetail, setCarRoof);
            GetComponentCar(cars[i].TuningCar, exhausts[i].exhaust, cars[i].nameExhaustDetail, setCarExhaust);

            //добавляем колеса , 4 колеса равны 1 типу колес 
            GetComponentCar(cars[i].TuningCar, wheels[i].FL, cars[i].nameWheels + "fl", setWheelsType, true, cars[i].WheelTypeCount);
            GetComponentCar(cars[i].TuningCar, wheels[i].FR, cars[i].nameWheels + "fr", setWheelsType, true, cars[i].WheelTypeCount);
            GetComponentCar(cars[i].TuningCar, wheels[i].RL, cars[i].nameWheels + "rl", setWheelsType, true, cars[i].WheelTypeCount);
            GetComponentCar(cars[i].TuningCar, wheels[i].RR, cars[i].nameWheels + "rr", setWheelsType, true, cars[i].WheelTypeCount);
        }
    }

    private void Update()
    {
        GetMaterialsCars();
        //определяем какая категория сейчас выбрана
        categoreSelected = UIButtons.SelectedCategoryTuning;
    }

    //загрузка сохраненных данных и присваивание значений к локальным переменным
    public void LoadData(Save save)
    {
        setCar = save.currentCar;
        setFrontCarBumper = save.currentFrontBumper;
        setRearCarBumper = save.currentRearBumper;
        setCarSpoiler = save.currentSpoiler;
        setCarRoof = save.currentRoof;
        setCarExhaust = save.currentExhaust;

        //загрузка покраски
        UIButtons.r = save.r;
        UIButtons.g = save.g;
        UIButtons.b = save.b;
        UIButtons.bW = save.bW;
        UIButtons.rW = save.rW;
        UIButtons.gW = save.gW;
        UIButtons.aW = save.aW;

    }

    //метод для кнопки смены машины
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
    //метод для кнопки смены машины
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
    //метод для выбора мащшины и сохранения данных 
    public void SelectCar()
    {
        //запись в пременную класса save 
        save.currentCar = setCar;
       
    }
    //метод проходится по всем элементам машины и ищет все детали , и добавляет их в массивы 
    void GetComponentCar(GameObject car, GameObject[] Detail, string NameDetaill, int SetDetail, bool Wheels = false, int CountWheelsType = 0)
    {
        //проверяем что добавляем другие детали кроме колес потому , что для колес нужна другая логика и названия
        if (!Wheels)
        {
            //перебор деталей машины и добавление деталкй в массивы 
            for (int y = 0; y < car.transform.childCount; y++)
            {
                for (int x = 0; x < car.transform.childCount; x++)
                {
                    //проверяем есть ли такая деталь на машине
                    if (car.transform.GetChild(y).name == NameDetaill + x)
                    {
                        if (Detail.Length > 0)
                        {
                            Detail[x] = car.transform.GetChild(y).gameObject;
                            //все найденные детали отключаются , для того что б сделать активным стандартный или сохраненнный 
                            Detail[x].SetActive(false);
                        }
                    }
                }
            }
            //проверка существует ли такой бампер на этой машине , который был сохранен
            if (SetDetail >= Detail.Length)
            {
                if (Detail.Length > 0)
                    //если не существеут , то ставится стандартный бампер
                    Detail[0].SetActive(true);
            }
            else
            {
                //если сохраненный бампер существует , сохраненный бампер становится активным
                Detail[SetDetail].SetActive(true);
            }
        }
        //здесь уже добавляем колеса с определенными названиями 
        else
        {
            for (int y = 0; y < car.transform.childCount; y++)
            {
                for (int x = 0; x < car.transform.childCount; x++)
                {
                    //проверяем есть ли такая деталь на машине
                    if (car.transform.GetChild(y).name == NameDetaill)
                    {
                        //проверяем есть ли в массиве места для добавления детали
                        if (Detail.Length > 0)
                        {
                            //перебираем по новой массив колес отталкиваясь от количества типов колес
                            for (int i = 0; i < CountWheelsType; i++)
                            {
                               
                                Detail[i] = car.transform.GetChild(y).gameObject;
                            }
                            //все найденные бампера отключаются , для того что б сделать активным стандартный или сохраненнный 
                            //Detail[x].SetActive(false);
                        }
                    }
                }
            }
        }
    }
    //метод для изменения цвета кузова и стекол
    public void GetMaterialsCars()
    {
        //посмотреть как поменять текстуру на материале 
        for (int i = 0;i < cars[setCar].car.transform.childCount;i++)
        {
            //исключаем покраску окон 
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
    //метод возвращает новые екземпляры класса детали  , для того , что б можно было правильно заполнить массивы и уже на старте массивы имели свое количество деталей
    public GameObject[] SetCountDetails(GameObject car, string nameDetail, int countDetail, GameObject[] Detail)
    {
        //устанавливается количство деталей для каждой машины   
        for (int y = 0; y < car.transform.childCount; y++)
            {
                for (int x = 0; x < car.transform.childCount; x++)
                {
                //проверяем есть ли такая деатль на машине и создаем массив с заданным количеством детали
                //используеться для того что б до запуска игры уже массивы были готовы для заполнения 
                    if (car.transform.GetChild(y).name == nameDetail + x)
                    {
                        Detail = new GameObject[countDetail];
                    }
                }
        }
        //возвращает новый экземпляр класса детали с готовым количеством деталей 
        return Detail;
    }
    //метод для кнопок переключения передних бамперов
    public void SetCurrentCar()
    {
        for (int i = 0; i< cars.Length; i++)
        {
            cars[i].car.SetActive(false);
        }
        cars[setCar].car.SetActive(true);
    }
    //функция для каждой детали , переключенния 
    public int ChangeNextDetails(int SetDetail, GameObject[] Details)
    {
        SetDetail++;
        if (SetDetail == Details.Length || SetDetail >= Details.Length)
        {
            SetDetail = 0;
        }
        if (SetDetail != 0)
        {
            //проверяем есть ли вообще такая деталь у машины , что б не выпадала ошибка 
            if (Details.Length > 0)
            {
                Details[SetDetail - 1].SetActive(false);
            }   
        }
        else
        {
            if (Details.Length > 0)
            {
                //проверяем есть ли вообще такая деталь у машины , что б не выпадала ошибка 
                Details[Details.Length - 1].SetActive(false);
            }
            SetDetail = 0;
        }
        if (Details.Length > 0)
            Details[SetDetail].SetActive(true);
        return SetDetail;
    }
    //проверитьб функцию на работу с переключением деталей 
    public int ChangePreviewDetails(int SetDetail, GameObject[] Details)
    {
        SetDetail--;
        if (SetDetail < 0)
            //проверяем есть ли вообще такая деталь у машины , что б не выпадала ошибка 
            if (Details.Length > 0)
                SetDetail = Details.Length - 1;
        if (SetDetail >= Details.Length - 1)
            //проверяем есть ли вообще такая деталь у машины , что б не выпадала ошибка 
            if (Details.Length > 0)
                Details[0].SetActive(false);
        if (SetDetail < Details.Length - 1)
            if (Details.Length > 0)
                Details[SetDetail + 1].SetActive(false);
        if (Details.Length > 0)
            Details[SetDetail].SetActive(true);
        return SetDetail;
    }
    //метод для кнопки переключения деталей
    public void NextDetail()
    {
        //определяем какая категоря выбрана 
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
    //метод для кнопок переключения деталей
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
    //метод для установки бампера и сохраения 
    public void SaveDetail()
    {
        //сохраняет выбранный бампер в метод save SaveLoadManafer
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
        //вызывает метод сохранения и записывает отдельно каждую выбранную деталь
        slManager.SaveGame(save);
    }
    //метод для кнопки старт , запускает сцену с игровым миров 
    public void StartRace()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
