using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // WaveManger instance for easy access by floaters
    public static WaveManager instance;
    
    // Our Wave functions, same as in the shader
    public Vector4 WaveA = new Vector4(1f, 0.2f, 0.1f, 10f);
    public Vector4 WaveB = new Vector4(0.3f, 0f, 0.1f, 10f);
    public Vector4 WaveC = new Vector4(1f, 0.1f, 0.1f, 15f);

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this) 
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    // Returns the current height of the wave based on x and z positions takes into account all three waves
    public float getWaveHeight(float x, float z)
    {
        Vector3 p = new Vector3(x, 0, z);
        float y = 0;

        y += GerstnerWave(WaveA, p);
        y += GerstnerWave(WaveB, p);
        y += GerstnerWave(WaveC, p);

        return y;
    }

    // Gersnter wave calculation, same math as in the shader script
    float GerstnerWave(Vector4 wave, Vector3 point)
    {
        float s = wave.z;
        float k = (2 * Mathf.PI / wave.w);
        float c = Mathf.Sqrt(Mathf.Abs(Physics.gravity.y) / k);
        float a = s / k;

        Vector2 d = new Vector2(wave.x, wave.y);
        d.Normalize();

        float dot = Vector2.Dot(d, new Vector2(point.x, point.z));
        float f = k * (dot - c * Time.time);
        return a * Mathf.Sin(f);
    }
}
