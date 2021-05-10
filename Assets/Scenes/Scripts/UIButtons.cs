using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    //canvas with all panel for UI tuning 
    public GameObject tuningMenu;
    public static int SelectedCategoryTuning;
    private int countElement;


    //каждый слайдер надо добавить , что б на него была постоянная ссылка и не выбивало ошибку при перехождении в другой пункт меню
    public Slider Red;
    public Slider RedW;
    public Slider Green;
    public Slider GreenW;
    public Slider Blue;
    public Slider BlueW;
    public Slider AlphaW;

    //статические переменные для смены цвета в тюнмнге
    public static float r;
    public static float rW;
    public static float g;
    public static float gW;
    public static float b;
    public static float bW;
    public static float aW;


    private void Start()
    {
        LoadValue();
        //получаем количество элементов в канвасе UI
        countElement = tuningMenu.transform.childCount;
    }

    //method for button back to panel switch cars
    public void back()
    {
        int countElement = tuningMenu.transform.childCount;
        for (int i = 0; i < countElement; i++)
        {
            tuningMenu.transform.GetChild(i).transform.gameObject.SetActive(false);
            if (tuningMenu.transform.GetChild(i).name == "SwitchCars")
            {
                tuningMenu.transform.GetChild(i).transform.gameObject.SetActive(true);
            }
        }
    }

    //method for button tuning panel
    public void selectTuning()
    {
        SetPanel("SwitchDetails");
        SetPanel("CategoryTuning");
    }
    //метод для перехода в режим покраски
    public void selectPaint()
    {
        SetPanel("Paint");
    }

    //методы для группы слайдеров настройки цвета стекол типо тонировка 
    public void Rw()
    {
         rW = RedW.value;
        //сохраняем значения покраски
    }    
    public void Gw()
    {
         gW = GreenW.value;
        //сохраняем значения покраски
    }    
    public void Bw()
    {
         bW = BlueW.value;
        //сохраняем значения покраски
    }
    //так же метод для получения альфа канала , для прозрачности стекол
    public void Aw()
    {
         aW = AlphaW.value;
        //сохраняем значения покраски
    }

    //группа слайдеров RGB для настройки цвета кузова и всех его элементов 
    public void R()
    {
         r = Red.value;
        //сохраняем значения покраски
    }    
    public void G()
    {
         g = Green.value;
        //сохраняем значения покраски
    }    
    public void B()
    {
         b = Blue.value;
        //сохраняем значения покраски
    }

    //общий метод для активации нужного меню
    private void SetPanel(string NamePanel)
    {
        //set active false for all elemnt canvas 
        for (int i = 0; i < countElement; i++)
        {
            tuningMenu.transform.GetChild(i).transform.gameObject.SetActive(false);
            //set active true for tuning bumpers panel
            if (tuningMenu.transform.GetChild(i).name == NamePanel)
            {
                tuningMenu.transform.GetChild(i).transform.gameObject.SetActive(true);
            }
        }
    }
    //метод для выбора категорий тюнинга детали 
    public void SelectCategory(int SelectedCategory)
    {
        //находим текст категории в панели на сцене
        GameObject categoryName = GameObject.Find("CategoryName");
        //находим камеру в сцене  , для того что б при выборе категории повернуть камеру 
        GameObject mainCamera = GameObject.Find("CameraTarget");
        //получаем компонент Text , что б менять его текст 
        Text textCategory = categoryName.GetComponent<Text>();
        //меняем текст категории на нужную
        switch (SelectedCategory)
        {
            case 0:
                textCategory.text = "Front Bumper";
                break;
            case 1:
                textCategory.text = "Rear Bumper";
                break;
            case 2:
                textCategory.text = "Spoiler";
                break;
            case 3:
                textCategory.text = "Roof";
                break;
            case 4:
                textCategory.text = "Exhaust";
                break;
            default:
                textCategory.text = "";
                break;
        }
        SelectedCategoryTuning = SelectedCategory;
    }

    public void LoadValue()
    {
         Red.value = r;
         RedW.value = rW;
         Green.value = g;
         GreenW.value = gW;
         Blue.value = b;
         BlueW.value = bW;
         AlphaW.value = aW;
    }
}
