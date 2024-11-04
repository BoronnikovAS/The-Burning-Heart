using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Gradient _gradient;

    private void Awake()
    {
        progressBar = GetComponent<Image>();        
    }

    public void ProgressBarChanged(float curVariable, float maxVariable)
    {
        progressBar.fillAmount = curVariable / maxVariable;
        progressBar.color = _gradient.Evaluate(curVariable / maxVariable);
    }
}
