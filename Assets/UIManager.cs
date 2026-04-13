using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Unity.VisualScripting.Dependencies.NCalc;
using System.Net.Http.Headers;
using System.Diagnostics;

public class UIManager : MonoBehaviour
{
    public Text Result;

    [SerializeField]double SavedNumber;
    [SerializeField]string Operation;
    [SerializeField]bool AddNumber=false;
    
    public void OnNumberPressed(string pressed)
    {
        if (AddNumber)
        {
            int pressedNumber;
            if (int.TryParse(pressed, out pressedNumber))
            {
                this.Result.text = pressedNumber.ToString();
                AddNumber = false;
            }

        }
        else
        {
            if (this.Result.text.Contains("."))
            {
                this.Result.text += pressed;
            }
            else
            {
                double number;
                int pressedNumber;
                if (double.TryParse(this.Result.text, out number) && int.TryParse(pressed, out pressedNumber))
                {
                    double newNumber = (number * 10) + pressedNumber;
                    this.Result.text = newNumber.ToString();

                }

            }
        }

        
    }
    public void OnCleanPress()
    {
        this.Result.text = 0.ToString();
        SavedNumber = 0;
        Operation = string.Empty;
        AddNumber = false;

    }
    public void OnDotPress()
    {
        if (AddNumber)
        {
            this.Result.text = "0.";
            AddNumber = false;
        }
        if(!this.Result.text.Contains("."))
        {
            this.Result.text += ".";
        }
    }
    public void OnPourcentagepress()
    {
        double number;
        if(double.TryParse(this.Result.text,out number))
        {
            number = number / 100;
        }
        this.Result.text = number.ToString();
    }
    public void OnSignePress()
    {
        double number;
        if (double.TryParse(this.Result.text, out number))
        {
            number = -number;
        }
        this.Result.text = number.ToString();
    }
    public void OnEqualPress()
    {
        if (!AddNumber && Operation!="")
        {
            double number;
            if (double.TryParse(this.Result.text, out number))
            {
                double newNumber = Calcule(Operation, number);
                Result.text = newNumber.ToString();
                SavedNumber = 0;
                AddNumber= true;

            }
        }
    }
    public void OnOperationPress(string pressed)
    {
        double number;
        if (!AddNumber && Operation != "")
        {
            if (double.TryParse(this.Result.text, out number))
            {
                double newNumber = Calcule(Operation, number);
                SavedNumber = newNumber;
                Operation = pressed;
                AddNumber = true;  
            }
        }
        else
        {
            if (double.TryParse(this.Result.text, out number))
            {
                SavedNumber = number;
                Operation = pressed;
                AddNumber = true;
            }

        }
        
    }
    double Calcule(string op, double number)
    {
        double newNumber = 0;
        switch (op)
        {
            case "plus":
                newNumber = SavedNumber + number;
                break;
            case "minus":
                newNumber = SavedNumber - number;
                break;

            case "multiply":
                newNumber = SavedNumber * number;
                break;
            case "divide":
                newNumber = SavedNumber / number;
                break;
        }


        return newNumber;
    }

}
