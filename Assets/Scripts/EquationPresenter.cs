using System;
using TMPro;
using Unity.VisualScripting;
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
    }
    private void OnDisable()
    {
        model.OnErrorOccured -= ShowErrorScreen;
        model.OnUpdateEquation -= UpdateEquationField;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) SaveEquationInput(); 
        model.Equation = PlayerPrefs.GetString("equationInput", "");
    }
    public void UpdateEquationModel()
    {
        model.Equation = inputField.text;
    }
    void UpdateEquationField()
    {
        inputField.text = model.Equation;
    }

    private void SaveEquationInput()
    {
        PlayerPrefs.SetString("equationInput", inputField.text);
        PlayerPrefs.Save();
    }
    public void OnResultClicked()
    {
        OnSolveEquation?.Invoke();
    }

    void ShowErrorScreen()
    {
        inputField.text = "";
        SaveEquationInput();
        equationScreen.SetActive(false);
        errorScreen.SetActive(true);
    }
    public void ShowEquationScreen()
    {
        errorScreen.SetActive(false);
        //inputField.text = "";
        equationScreen.SetActive(true);
    }
}
