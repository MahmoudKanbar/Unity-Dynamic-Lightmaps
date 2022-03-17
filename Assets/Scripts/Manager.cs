using DynamicLightmaps;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] environments;
    [SerializeField] private LightState[] lightStates;

    private int index = -1;
    private LightState currentState;
    private GameObject currentEnvironment;

    private void Awake()
    {
        if (environments.Length != lightStates.Length)
            throw new System.Exception("Environment and Light States Mismatch");
    }

    private void Start()
    {
        // NOTE IF YOU WANT TO CHANGE THE STATE OF THE LIGHTMAPS AT THE BEGINING OF THE GAME
        // CHANGE IT IN START NOT AWAKE BECAUSE THE LIGHT PROBES IN THE SCENE MIGHT BE NOT INITIALIZED
        NextState();
    }

    public void NextState()
    {
        index++;
        if (index >= environments.Length) index = 0;

        if (currentEnvironment != null) currentEnvironment.SetActive(false);
        currentState = lightStates[index];
        currentEnvironment = environments[index];

        currentEnvironment.SetActive(true);
        currentState.UpdateLightMapsSettings();
    }
}
