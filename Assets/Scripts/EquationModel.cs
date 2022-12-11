using System;
using UnityEngine;
//model
public class EquationModel : MonoBehaviour
{
    [SerializeField] private EquationPresenter presenter;
    public Action OnErrorOccured;//
    public Action OnUpdateEquation;//
    private string _equation;
    public string Equation { 
        get => _equation;
        set
        { 
            _equation = value;
            OnUpdateEquation.Invoke();
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

    int FindFirstDivisionSymbol(String str)
    {
        for (int i = 0; i < str.Length; i++)
            if (str[i] == '/')
                return i;
        return 0;
    }

    void SolveEquation()
    {
        if(_equation.Length == 0) return;
        int divPos = FindFirstDivisionSymbol(_equation);
        string opFirstStr = _equation.Substring(0, divPos);
        string opSecondStr = _equation.Substring(divPos + 1, _equation.Length - divPos - 1);

        foreach (var ch in opFirstStr)
            if (ch > '9' || ch < '0') { OnErrorOccured.Invoke(); return; }
        if (!decimal.TryParse(opFirstStr, out var opFirst)) { OnErrorOccured.Invoke(); return; }
        
        foreach (var ch in opSecondStr) 
            if (ch > '9' || ch < '0') { OnErrorOccured.Invoke(); return; }
        if (!decimal.TryParse(opSecondStr, out var opSecond)) { OnErrorOccured.Invoke(); return; }
        
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
