using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro

public class TypingEffect : MonoBehaviour
{
    [SerializeField]
    TMP_Text _text; // Reference to the TextMeshPro component
    [SerializeField]
    float _typingSpeed = 0.1f; // Speed of typing in seconds
    [SerializeField] 
    string _fullText; // The complete text to be typed

    private void Start()
    {
        _fullText = _text.text; // Store the full text
        _text.text = string.Empty; // Clear the text
    }

    public void StartTypingAnimation()
    {
        // Start typing animation
        StartCoroutine(TypeText()); 
    }

    // Coroutine to simulate typing effect
    IEnumerator TypeText()
    {
        foreach (char letter in _fullText)
        {
            _text.text += letter; // Append each letter to the text
            yield return new WaitForSeconds(_typingSpeed); // Wait for the specified duration
        }
    }
}