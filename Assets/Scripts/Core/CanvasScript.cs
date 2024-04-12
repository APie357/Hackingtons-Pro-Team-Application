using TMPro;
using UnityEngine;

namespace Core {
    public class CanvasScript : MonoBehaviour {
        // Variables
        Canvas _canvas;
        int _score;
        float _buttonX;
        float _buttonY;
        
        [SerializeField] GameObject button;
        [SerializeField] TextMeshProUGUI  scoreText;
        [SerializeField] TextMeshProUGUI noteText;
        [SerializeField] AudioSource clickSound;
        
        void Awake() {
            // Initialize variables
            _canvas = GetComponent<Canvas>();
            _score = PlayerPrefs.GetInt("score", 0);  // Loads score from PlayerPrefs
            UpdateScore();
            
            // Load position
            _buttonX = PlayerPrefs.GetFloat("buttonX", 0f);
            _buttonY = PlayerPrefs.GetFloat("buttonY", 0f);
            UpdateButtonPosition(new Vector3(_buttonX, _buttonY));
        }

        public void ButtonClicked() {
            // Update the score
            _score++;
            UpdateScore();
            
            // Set a random position
            UpdateButtonPosition(RandomPosition());
            
            // Play clicked sound
            clickSound.Play();
        }

        void UpdateButtonPosition(Vector3 position) {
            // Set position
            button.GetComponent<RectTransform>().localPosition = position;
            
            // Update staved position
            PlayerPrefs.SetFloat("buttonX", position.x);
            PlayerPrefs.SetFloat("buttonY", position.y);
        }

        Vector3 RandomPosition() {
            // Get the transforms for the canvas and button
            var canvasTransform = _canvas.GetComponent<RectTransform>();
            var buttonTransform = button.GetComponent<RectTransform>();
            
            // Generate the maximum values for the position
            var xRange = (canvasTransform.rect.width - buttonTransform.rect.width) / 2 ;
            var yRange = (canvasTransform.rect.height - buttonTransform.rect.height) / 2;
            
            // Generate the random number
            var x = Random.Range(-xRange, xRange);
            var y = Random.Range(-yRange, yRange);
            
            // Return the new position
            return new Vector3(x, y);
        }
        
        void UpdateScore() {
            // Set the score
            scoreText.text = _score.ToString();

            // Set the note to only be visible when the score is one
            noteText.enabled = _score == 1;
            
            // Saves score in PlayerPrefs
            PlayerPrefs.SetInt("score", _score);
        }

        public void ResetData() {
            // Set score to zero
            _score = 0;
            UpdateScore();
            
            // Reset the button position
            UpdateButtonPosition(Vector3.zero);
        }
    }
}
