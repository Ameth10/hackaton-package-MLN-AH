# MLN Feedbacks

MLN Feedbacks ajoute facilement des effets de « feedback » à un jeu Unity : un objet qui grossit quand on le touche, qui tremble, qui clignote quand il prend un coup, une explosion de particules avec son bruitage…

## Sommaire

1. [Présentation](#présentation)
2. [Compatibilité](#compatibilité)
3. [Installation](#installation)
4. [Démarrage rapide](#démarrage-rapide)
5. [Référence des feedbacks](#référence-des-feedbacks)
6. [Utilisation par code](#utilisation-par-code)
7. [Créer son propre feedback](#créer-son-propre-feedback)
8. [Scène de démonstration](#scène-de-démonstration)
9. [Changer de version](#changer-de-version)
10. [Licence](#licence)

## Présentation

Un **feedback**, c'est la réaction visuelle ou sonore d'un jeu quand il se passe quelque chose : un coup reçu, un objet ramassé, un bouton cliqué.

MLN Feedbacks propose trois familles d'effets :

| Famille | Feedbacks |
|---|---|
| **Highlight de transforms** | Punch Scale (grossit puis revient), Shake (tremble) |
| **Animation de matériaux** | Color Flash (flash de couleur puis retour) |
| **VFX et son** | VFX + Sound (particules et son ensemble) |

Chaque effet se règle dans l'Inspector et remet toujours l'objet exactement dans son état d'origine à la fin.

## Compatibilité

- Unity 6 (6000.0) ou une version plus récente.
- Fonctionne avec l'URP, le HDRP et le pipeline classique (Built-in).
- Aucune dépendance : rien d'autre à installer.
- La scène de démonstration utilise des matériaux URP.

## Installation

1. Dans Unity, ouvre **Window > Package Manager**.
2. Clique sur le bouton **+** en haut à gauche, puis sur **Install package from git URL**.
3. Colle cette adresse, puis clique sur **Install** :

   ```
   https://github.com/Ameth10/hackaton-package-mln.git#upm
   ```

4. Le package **MLN Feedbacks** apparaît dans la liste : c'est installé.

## Démarrage rapide

En 3 étapes, sans écrire plus d'une ligne de code :

1. Sélectionne l'objet à animer, puis **Add Component > MLN Feedbacks > Feedback Player**.
2. Sur le même objet, ajoute un ou plusieurs feedbacks : **Add Component > MLN Feedbacks > Punch Scale** (ou Shake, Color Flash, VFX + Sound).
3. Déclenche-les depuis ton script :

   ```csharp
   GetComponent<MLNFeedbacks.FeedbackPlayer>().Play();
   ```

**Pour essayer sans code :** passe en mode Play, sélectionne l'objet et clique sur **Tester** dans l'Inspector du Feedback Player.

## Référence des feedbacks

### Réglages communs

| Réglage | Rôle | Par défaut |
|---|---|---|
| Delay | Attente avant le début de l'effet, en secondes | 0 |
| Duration | Durée de l'effet, en secondes | 0.3 |
| Intensity | Force de l'effet (1 = normal, 2 = deux fois plus fort) | 1 |
| Curve | Forme de l'effet dans le temps (horizontal : temps, vertical : force) | Monte vite, redescend doucement |

### Feedback Player

| Réglage | Rôle |
|---|---|
| Include Children | Lancer aussi les feedbacks des objets enfants |
| On Play | Événement appelé à chaque lancement (pour brancher autre chose dans l'Inspector) |

### Les feedbacks

| Feedback | Ce qu'il fait | Réglages propres |
|---|---|---|
| **Punch Scale** | L'objet grossit puis revient à sa taille | Strength : agrandissement maximal (0.3 = +30 %) |
| **Shake** | L'objet tremble autour de sa position | Strength : distance maximale du tremblement |
| **Color Flash** | La couleur passe vers une couleur de flash puis revient | Flash Color ; Color Property (`_BaseColor` en URP, `_Color` en Built-in, détectée automatiquement) |
| **VFX + Sound** | Fait apparaître des particules et joue un son | Vfx Prefab, Vfx Offset ; Sound, Volume, Pitch Variation |

## Utilisation par code

```csharp
using MLNFeedbacks;

FeedbackPlayer player = GetComponent<FeedbackPlayer>();
player.Play();   // lance tous les feedbacks de l'objet
player.Stop();   // les arrête et remet l'objet comme avant

FeedbackPlayer.GlobalEnabled = false;   // coupe tous les feedbacks du jeu (option « effets »)
```

Chaque feedback peut aussi être lancé seul : `GetComponent<PunchScaleFeedback>().Play();`

## Créer son propre feedback

Hérite de la classe `Feedback` et écris deux méthodes. Le délai, la durée, l'intensité et la courbe sont gérés pour toi, et le `FeedbackPlayer` lancera ton feedback automatiquement.

```csharp
using MLNFeedbacks;
using UnityEngine;

public class SpinFeedback : Feedback
{
    public float angle = 45f;
    Quaternion _start;

    protected override void OnStart() => _start = transform.localRotation;                                // mémoriser l'état de départ
    protected override void Animate(float value) => transform.localRotation = _start * Quaternion.Euler(0f, angle * value, 0f);
    protected override void ResetState() => transform.localRotation = _start;                             // remettre comme avant
}
```

## Scène de démonstration

La scène de démonstration n'est pas installée avec le package, pour ne pas encombrer ton projet. Pour l'ajouter :

1. Dans le Package Manager, sélectionne **MLN Feedbacks**.
2. Ouvre l'onglet **Samples**.
3. Clique sur **Import** à côté de **Demo (stand de tir)**.
4. Ouvre la scène `Assets/Samples/MLN Feedbacks/<version>/Demo (stand de tir)/Scene/Demo.unity` et lance le mode Play.

Dans la démo : clique sur les cibles. La bleue montre les transforms, la rouge le matériau, la verte les VFX et le son. La touche **F** active ou désactive tous les feedbacks, pour comparer.

## Changer de version

L'adresse d'installation donne toujours la dernière version. Pour une version précise, remplace `#upm` par `#upm/` suivi du numéro de version, par exemple :

```
https://github.com/Ameth10/hackaton-package-mln.git#upm/v1.0.0
```

Les versions et leurs nouveautés sont listées dans le [CHANGELOG](../CHANGELOG.md).

## Licence

Licence MIT : libre d'utilisation, de modification et de partage, y compris dans un projet commercial, à condition de garder la mention de l'auteur. Voir [LICENSE.md](../LICENSE.md).
