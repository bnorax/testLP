using System;
using TMPro;
using UnityEngine;

//presenter
public class EquationPresenter : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject equationScreen;
    [SerializeField] private GameObject errorScreen;
    [SerializeField] private EquationModel model;
    public Action OnSolveEquation;
    private void OnEnable()
    {
        model.OnErrorOccured += ShowErrorScreen; 
        model.OnUpdateEquation += UpdateEquationField;
        
        model.Equation = PlayerPrefs.GetString("equationInput");
    }
    private void OnDisable()
    {
        PlayerPrefs.SetString("equationInput", model.Equation);
        PlayerPrefs.Save();
        
        model.OnErrorOccured -= ShowErrorScreen;
        model.OnUpdateEquation -= UpdateEquationField;
    }
    public void UpdateEquationModel()
    {
        model.Equation = inputField.text;
    }
    void UpdateEquationField()
    {
        inputField.text = model.Equation;
    }

    public void OnResultClicked()
    {
        OnSolveEquation?.Invoke();
    }

    void ShowErrorScreen()
    {
        equationScreen.SetActive(false);
        errorScreen.SetActive(true);
    }
    public void ShowEquationScreen()
    {
        errorScreen.SetActive(false);
        inputField.text = "";
        equationScreen.SetActive(true);
    }
}
