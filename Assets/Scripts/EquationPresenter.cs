using System;
using TMPro;
using UnityEngine;
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
    private void ShowErrorScreen()
    {
        inputField.text = "";
        SaveEquationInput();
        equationScreen.SetActive(false);
        errorScreen.SetActive(true);
    }
    private void SaveEquationInput()
    {
        PlayerPrefs.SetString("equationInput", inputField.text);
        PlayerPrefs.Save();
    }
    private void UpdateEquationField()
    {
        inputField.text = model.Equation;
    }
    public void UpdateEquationModel()
    {
        model.Equation = inputField.text;
    }
    public void OnResultClicked()
    {
        OnSolveEquation?.Invoke();
    }
    public void ShowEquationScreen()
    {
        errorScreen.SetActive(false);
        equationScreen.SetActive(true);
    }
}
