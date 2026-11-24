using UnityEditor;
using UnityEngine;

namespace MLNFeedbacks.Editor
{
    /// <summary>
    /// Inspector du FeedbackPlayer : liste les feedbacks trouvés et ajoute
    /// des boutons « Tester » et « Arrêter » pour les essayer sans écrire de code.
    /// </summary>
    [CustomEditor(typeof(FeedbackPlayer))]
    public class FeedbackPlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var player = (FeedbackPlayer)target;
            Feedback[] feedbacks = player.Feedbacks;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"Feedbacks trouvés : {feedbacks.Length}", EditorStyles.boldLabel);
            foreach (Feedback feedback in feedbacks)
                EditorGUILayout.LabelField("• " + ObjectNames.NicifyVariableName(feedback.GetType().Name));

            if (feedbacks.Length == 0)
                EditorGUILayout.HelpBox("Ajoute des feedbacks : Add Component > MLN Feedbacks.", MessageType.Warning);

            EditorGUILayout.Space();
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Passe en mode Play pour tester les feedbacks.", MessageType.Info);
                return;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Tester"))
                    player.Play();
                if (GUILayout.Button("Arrêter"))
                    player.Stop();
            }
        }
    }
}
