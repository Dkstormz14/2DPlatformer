using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // Create text when character takes damage
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // Create text when character takes damage
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
           #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
              Application.Quit();
#elif (UNITY_WEBGL)
              SceneManager.LoadScene("QuitScene");
#endif
        }
    }
}
