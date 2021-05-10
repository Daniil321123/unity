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


    //������ ������� ���� �������� , ��� � �� ���� ���� ���������� ������ � �� �������� ������ ��� ������������ � ������ ����� ����
    public Slider Red;
    public Slider RedW;
    public Slider Green;
    public Slider GreenW;
    public Slider Blue;
    public Slider BlueW;
    public Slider AlphaW;

    //����������� ���������� ��� ����� ����� � �������
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
        //�������� ���������� ��������� � ������� UI
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
    //����� ��� �������� � ����� ��������
    public void selectPaint()
    {
        SetPanel("Paint");
    }

    //������ ��� ������ ��������� ��������� ����� ������ ���� ��������� 
    public void Rw()
    {
         rW = RedW.value;
        //��������� �������� ��������
    }    
    public void Gw()
    {
         gW = GreenW.value;
        //��������� �������� ��������
    }    
    public void Bw()
    {
         bW = BlueW.value;
        //��������� �������� ��������
    }
    //��� �� ����� ��� ��������� ����� ������ , ��� ������������ ������
    public void Aw()
    {
         aW = AlphaW.value;
        //��������� �������� ��������
    }

    //������ ��������� RGB ��� ��������� ����� ������ � ���� ��� ��������� 
    public void R()
    {
         r = Red.value;
        //��������� �������� ��������
    }    
    public void G()
    {
         g = Green.value;
        //��������� �������� ��������
    }    
    public void B()
    {
         b = Blue.value;
        //��������� �������� ��������
    }

    //����� ����� ��� ��������� ������� ����
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
    //����� ��� ������ ��������� ������� ������ 
    public void SelectCategory(int SelectedCategory)
    {
        //������� ����� ��������� � ������ �� �����
        GameObject categoryName = GameObject.Find("CategoryName");
        //������� ������ � �����  , ��� ���� ��� � ��� ������ ��������� ��������� ������ 
        GameObject mainCamera = GameObject.Find("CameraTarget");
        //�������� ��������� Text , ��� � ������ ��� ����� 
        Text textCategory = categoryName.GetComponent<Text>();
        //������ ����� ��������� �� ������
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
