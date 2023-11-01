using UnityEngine;
using UnityEngine.UI;

public class SliderSpeedControl : MonoBehaviour
{
    public Slider timerSlider;
    public float baseSliderSpeed = 1.0f; // Adjust this value to control the base slider speed
    private float timerDuration = 2.0f; // The initial timer duration

    private void Update()
    {
        // Calculate the speed based on the timer duration
        float speed = 1.0f / timerDuration * baseSliderSpeed;

        // Modify the slider's handle speed
        timerSlider.value += Time.deltaTime * speed;

        // Ensure the slider value stays within the 0-1 range
        timerSlider.value = Mathf.Clamp01(timerSlider.value);
    }

    public void SetTimerDuration(float duration)
    {
        timerDuration = duration;
    }
}
