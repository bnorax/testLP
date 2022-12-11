using System;
using UnityEngine;
//model
public class EquationModel : MonoBehaviour
{
    [SerializeField] private EquationPresenter presenter;
    public Action OnErrorOccured;
    public Action OnUpdateEquation;
    private string _equation;
    public string Equation { 
        get => _equation;
        set
        { 
            _equation = value;
            OnUpdateEquation.Invoke(); //updating presenter with new equation
        }
    }
    private void OnEnable()
    {
        presenter.OnSolveEquation += SolveEquation;
    }

    private void OnDisable()
    {
        presenter.OnSolveEquation -= SolveEquation;
    }

    int FindFirstDivisor(String str) //finding first divisor 
    {
        for (int i = 0; i < str.Length; i++)
            if (str[i] == '/')
                return i;
        return 0;
    }

    void SolveEquation()
    {
        if(_equation.Length == 0) return;
        int divPos = FindFirstDivisor(_equation);
        string opFirstStr = _equation.Substring(0, divPos); //using divisor position to get two operands from string
        string opSecondStr = _equation.Substring(divPos + 1, _equation.Length - divPos - 1);

        foreach (var ch in opFirstStr) //checking operand for symbols other than 0-9
            if (ch > '9' || ch < '0') { OnErrorOccured?.Invoke(); return; } //if that is the case, telling presenter about error
        if (!decimal.TryParse(opFirstStr, out var opFirst)) { OnErrorOccured?.Invoke(); return; } //getting number from string
        
        foreach (var ch in opSecondStr) 
            if (ch > '9' || ch < '0') { OnErrorOccured?.Invoke(); return; }
        if (!decimal.TryParse(opSecondStr, out var opSecond)) { OnErrorOccured?.Invoke(); return; }
        
        try
        {
            Equation = decimal.Divide(opFirst, opSecond).ToString("0.##########");
        }
        catch(DivideByZeroException e)
        {
            Equation = "Cannot divide by 0";
        }
    }
}
