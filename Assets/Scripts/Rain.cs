using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rain : MonoBehaviour
{
    
    public Light dirLight; // для интенсивности света до начала дождя
    private ParticleSystem _ps;
    private bool _isRain = false; // идет ли сейчас дождь или нет

    // создаем метод Start который запускается при старте игры
    private void Start()
    {
        // тут мы устанавливаем компонент где и находится сам скрипт
        _ps = GetComponent<ParticleSystem>();
        // для того чтобы дождь стартовал автоматически через определенное время то используем StartCoroutine
        StartCoroutine(Weather());
    }

    // создаем метод для плавности интенсивности света
    private void Update() 
    {
        if (_isRain && dirLight.intensity > 0.25f)
            LightIntensity(-1);
        else if(!_isRain && dirLight.intensity < 0.5f)
            LightIntensity(1);

        
    }

    // тут мы уменьшаем интенсивность света
    private void LightIntensity(int mult) 
    {
        dirLight.intensity += 0.1f * Time.deltaTime * mult;
    }      

    IEnumerator Weather()
    {
        while (true) 
        {
         // это ожидание по времени где мы будем ждать столько то количество секунд для того чтобы дождь
         // начинался и заканчивался в случаный период времени
            yield return new WaitForSeconds(UnityEngine.Random.Range(60f, 180f));

            if (_isRain)          
                _ps.Stop();
            else 
                _ps.Play();
                
            // устанавливаем противоположное значение этого поля, для того чтобы они менялись
            _isRain = !_isRain;
        }
    }
}
