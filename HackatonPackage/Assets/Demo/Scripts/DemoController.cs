using UnityEngine;

namespace MLNFeedbacks.Demo
{
    /// <summary>
    /// Gère la démo : score, touche F pour activer ou désactiver tous les feedbacks,
    /// et textes à l'écran.
    /// </summary>
    public class DemoController : MonoBehaviour
    {
        static int _score;
        GUIStyle _style;

        /// <summary>Ajoute un point au score.</summary>
        public static void AddPoint() => _score++;

        void Awake()
        {
            _score = 0;
            FeedbackPlayer.GlobalEnabled = true;
        }

        void Update()
        {
            if (DemoInput.TogglePressedThisFrame())
                FeedbackPlayer.GlobalEnabled = !FeedbackPlayer.GlobalEnabled;
        }

        void OnDestroy() => FeedbackPlayer.GlobalEnabled = true;

        void OnGUI()
        {
            if (_style == null)
            {
                _style = new GUIStyle(GUI.skin.box)
                {
                    fontSize = Mathf.RoundToInt(Screen.height / 30f),
                    alignment = TextAnchor.MiddleCenter,
                    padding = new RectOffset(12, 12, 6, 6)
                };
                _style.normal.textColor = Color.white;
            }

            float margin = 12f;
            string state = FeedbackPlayer.GlobalEnabled ? "Feedbacks : ON" : "Feedbacks : OFF";
            Draw(new Vector2(margin, margin), "Score : " + _score, TextAnchor.UpperLeft);
            Draw(new Vector2(Screen.width - margin, margin), state, TextAnchor.UpperRight);
            Vector2 bottom = new Vector2(Screen.width / 2f, Screen.height - margin);
            Draw(bottom, "Clic : tirer   ·   F : feedbacks ON/OFF", TextAnchor.LowerCenter);
            float lineHeight = _style.CalcSize(new GUIContent("A")).y + 6f;
            Draw(bottom - new Vector2(0f, lineHeight), "Bleu : transforms   ·   Rouge : matériau   ·   Vert : VFX + son", TextAnchor.LowerCenter);
        }

        void Draw(Vector2 anchor, string text, TextAnchor corner)
        {
            Vector2 size = _style.CalcSize(new GUIContent(text));
            float x = corner == TextAnchor.UpperLeft ? anchor.x
                    : corner == TextAnchor.UpperRight ? anchor.x - size.x
                    : anchor.x - size.x / 2f;
            float y = corner == TextAnchor.LowerCenter ? anchor.y - size.y : anchor.y;
            GUI.Box(new Rect(x, y, size.x, size.y), text, _style);
        }
    }
}
