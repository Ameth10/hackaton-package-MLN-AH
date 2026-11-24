using UnityEngine;

namespace MLNFeedbacks
{
    /// <summary>
    /// Animation de matériau : la couleur de l'objet passe vers une couleur de flash puis revient.
    /// Utilise un MaterialPropertyBlock : le matériau n'est jamais copié ni modifié.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("MLN Feedbacks/Color Flash")]
    public class ColorFlashFeedback : Feedback
    {
        [Tooltip("Couleur du flash.")]
        public Color flashColor = Color.white;

        [Tooltip("Propriété de couleur du shader : _BaseColor (URP / HDRP) ou _Color (pipeline classique). " +
                 "Si elle n'existe pas, la bonne propriété est cherchée automatiquement.")]
        public string colorProperty = "_BaseColor";

        Renderer _renderer;
        MaterialPropertyBlock _block;
        Color _startColor;
        int _propertyId;
        bool _ready;

        protected override void OnStart()
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();
            if (_block == null)
                _block = new MaterialPropertyBlock();

            Material material = _renderer.sharedMaterial;
            string property = FindColorProperty(material);
            _ready = property != null;
            if (!_ready)
            {
                Debug.LogWarning($"[MLN Feedbacks] Color Flash : aucune propriété de couleur trouvée sur le matériau de « {name} ».", this);
                return;
            }

            _propertyId = Shader.PropertyToID(property);
            _startColor = material.GetColor(_propertyId);
        }

        protected override void Animate(float value) =>
            ApplyColor(Color.LerpUnclamped(_startColor, flashColor, value));

        protected override void ResetState() => ApplyColor(_startColor);

        void ApplyColor(Color color)
        {
            if (!_ready)
                return;

            _renderer.GetPropertyBlock(_block);
            _block.SetColor(_propertyId, color);
            _renderer.SetPropertyBlock(_block);
        }

        string FindColorProperty(Material material)
        {
            if (material == null)
                return null;
            if (material.HasProperty(colorProperty))
                return colorProperty;
            if (material.HasProperty("_BaseColor"))
                return "_BaseColor";
            if (material.HasProperty("_Color"))
                return "_Color";
            return null;
        }

        void Reset()
        {
            duration = 0.35f;
            // Flash quasi instantané, puis retour progressif à la couleur d'origine.
            curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.1f, 1f), new Keyframe(1f, 0f));
        }
    }
}
