# MLN Feedbacks

**Un package Unity pour ajouter facilement des effets de « feedback » à un jeu** : un objet qui grossit quand on le touche, qui tremble, qui clignote quand il prend un coup, une explosion de particules avec son bruitage…

> **Version 1.0.0.** Quatre feedbacks prêts à l'emploi, une scène de démonstration et une documentation complète.

![La scène de démonstration « stand de tir » : trois cibles bleue, rouge et verte sur des socles, devant un mur rayé](Docs/images/apercu-demo.png)

## Sommaire

1. [C'est quoi ?](#cest-quoi-)
2. [Voir la démo](#voir-la-démo)
3. [Installer dans Unity](#installer-dans-unity)
4. [Changer de version](#changer-de-version)
5. [Organisation du dépôt](#organisation-du-dépôt)
6. [Pour les développeurs](#pour-les-développeurs)
7. [Licence](#licence)

## C'est quoi ?

Un **feedback**, c'est la réaction visuelle ou sonore d'un jeu quand il se passe quelque chose : un coup reçu, un objet ramassé, un bouton cliqué. C'est ce qui rend un jeu agréable à jouer.

MLN Feedbacks propose trois familles d'effets :

| Famille | Feedbacks |
|---|---|
| **Transforms** | Punch Scale (grossit puis revient), Shake (tremble) |
| **Matériaux** | Color Flash (flash de couleur puis retour) |
| **VFX + son** | VFX + Sound (particules et son déclenchés ensemble) |

Les effets se règlent dans l'Inspector de Unity et se déclenchent en une ligne de code :

```csharp
GetComponent<MLNFeedbacks.FeedbackPlayer>().Play();
```

Le guide complet (démarrage rapide, réglages, créer son propre feedback) est dans la [documentation du package](com.niangahmed821.hackatonpackagepluginmln/Documentation~/hackatonpackagepluginmln.md).

## Voir la démo

Les démos jouables (`.exe` pour Windows) et les vidéos sont publiées dans la page **[Releases](https://github.com/Ameth10/hackaton-package-mln/releases)** du dépôt, une par version.

*Pas encore disponible.*

## Installer dans Unity

**Il faut :** Unity 6 (6000.0) ou une version plus récente.

1. Dans Unity, ouvre **Window > Package Manager**.
2. Clique sur le bouton **+** en haut à gauche, puis sur **Install package from git URL**.
3. Colle cette adresse, puis clique sur **Install** :

   ```
   https://github.com/Ameth10/hackaton-package-mln.git#upm
   ```

4. Le package **MLN Feedbacks** apparaît dans la liste : c'est installé.

`#upm` désigne la branche **upm** du dépôt, qui contient uniquement le package. Autre adresse possible, qui donne le même résultat : `https://github.com/Ameth10/hackaton-package-mln.git?path=/com.niangahmed821.hackatonpackagepluginmln`

**Scène de démonstration (optionnelle) :** dans le Package Manager, sélectionne **MLN Feedbacks**, ouvre l'onglet **Samples** et clique sur **Import** à côté de **Demo (stand de tir)**. Ouvre ensuite la scène `Demo` et lance le mode Play : clique sur les cibles, et appuie sur **F** pour activer ou désactiver les feedbacks.

## Changer de version

Sans rien ajouter, l'adresse ci-dessus installe toujours **la dernière version**.

Pour installer une version précise, ou revenir à une version précédente, remplace `#upm` par `#upm/` suivi du numéro de version, puis refais l'étape 3 de l'installation. Unity remplace la version installée, sans rien supprimer.

Exemple pour la version 0.2.0 :

```
https://github.com/Ameth10/hackaton-package-mln.git#upm/v0.2.0
```

| Version | Fin de l'adresse | Nouveautés |
|---|---|---|
| 1.0.0 | `#upm/v1.0.0` | Les 4 feedbacks, l'Inspector « Tester », la démo stand de tir, les tests |
| 0.3.2 | `#upm/v0.3.2` | Installation par la branche upm (adresse plus courte) |
| 0.3.1 | `#upm/v0.3.1` | Documentation complète du package, nettoyage du modèle Unity |
| 0.3.0 | `#upm/v0.3.0` | Licence open source MIT |
| 0.2.0 | `#upm/v0.2.0` | Nom « MLN Feedbacks », compatible Unity 6000.0 et plus |
| 0.1.0 | `#upm/v0.1.0` | Première version : structure du package |

Le détail de chaque version est dans le [CHANGELOG](com.niangahmed821.hackatonpackagepluginmln/CHANGELOG.md).

## Organisation du dépôt

```
hackaton-package-mln/        (branche main)
├── README.md          ← cette page
├── LICENSE.md         ← licence MIT
├── .github/workflows/ ← mise à jour automatique de la branche upm
├── Docs/
│   └── images/        ← captures et GIF affichés sur cette page
├── HackatonPackage/   ← projet Unity qui sert à développer et tester le package
└── com.niangahmed821.hackatonpackagepluginmln/   ← le package lui-même
    ├── package.json   ← nom, version et description du package
    ├── README.md      ← résumé du package
    ├── CHANGELOG.md   ← historique des versions
    ├── LICENSE.md     ← licence (livrée avec le package)
    ├── Third Party Notices.md   ← composants tiers utilisés (aucun)
    ├── Runtime/       ← code des feedbacks, utilisé en jeu
    ├── Editor/        ← code de l'éditeur Unity (Inspector)
    ├── Tests/         ← tests automatiques
    ├── Samples~/      ← scène de démonstration, importable à la demande
    └── Documentation~/   ← documentation complète du package
```

**Deux branches :**

| Branche | Contenu | Pour qui |
|---|---|---|
| `main` | Tout le dépôt : projet Unity, package, docs | Les développeurs du package |
| `upm` | Uniquement le package, à la racine | Unity, pour l'installation (`#upm`) |

La branche `upm` est **mise à jour automatiquement** par GitHub Actions à chaque push sur `main` : on ne la modifie jamais à la main.

**Où mettre quoi :**

| Fichier | Où le mettre |
|---|---|
| Captures d'écran, GIF pour cette page | `Docs/images/` |
| Démo jouable (`.exe`), vidéos | Page **Releases** de GitHub, **pas dans Git** (fichiers trop lourds) |
| Code des feedbacks | `com.niangahmed821.hackatonpackagepluginmln/Runtime/` |

## Pour les développeurs

**Ouvrir le projet :** dans Unity Hub, fais **Add > Add project from disk** et choisis le dossier **`HackatonPackage/`**. N'ouvre jamais la racine du dépôt : Unity y créerait un nouveau projet vide.

Le projet utilise le package en local : toute modification du package est visible directement dans Unity.

**La démo :** on la modifie dans `HackatonPackage/Assets/Demo/`, puis on recopie ce dossier (avec les fichiers `.meta`) dans `com.niangahmed821.hackatonpackagepluginmln/Samples~/Demo/` avant de publier une version.

**Les tests :** dans Unity, **Window > General > Test Runner**, onglet **PlayMode**, puis **Run All**.

**Publier une nouvelle version :**

1. Change `"version"` dans `com.niangahmed821.hackatonpackagepluginmln/package.json`.
2. Ajoute une entrée dans le `CHANGELOG.md` du package.
3. Ajoute une ligne au tableau « Changer de version » de cette page.
4. Fais le commit, puis le tag : `git tag -a v0.4.0 -m "Version 0.4.0"`.
5. Pousse les deux : `git push origin main v0.4.0`.
6. C'est tout : GitHub Actions met à jour la branche `upm` et crée le tag `upm/v0.4.0` (onglet **Actions** du dépôt pour vérifier). En cas de souci, relance-le à la main : **Actions > Branche upm > Run workflow**.
7. Si besoin, crée une Release sur GitHub et joins-y le `.zip` de la démo.

## Licence

MLN Feedbacks est sous **licence MIT** : tout le monde peut l'utiliser, le modifier et le partager, y compris dans un projet commercial, à condition de garder la mention de l'auteur. Voir [LICENSE.md](LICENSE.md).
