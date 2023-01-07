using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Polyperfect.Animals
{
    public class AnimalViewerManager : MonoBehaviour
    {
        [Header("UI Bindings")]
        public GameObject contentTile;
        public GameObject meshTileContentParent, animationTileContentParent, environmentTileContentParent;
        public Text meshText, animationText, environmentText;
        public Toggle meshToggle, animationToggle, environmentToggle, menuToggle;
        public ScrollRect scrollRect;
        public RectTransform mainMenuRectTransform;

        [Header("Procedural UI")]
        public Camera faceCamera;
        Transform[] animals;
        public GameObject animalParent;
        Transform previousCharacter;
        List<Sprite> sprites = new List<Sprite>();

        [Header("Animation Bindings")]
        public float animationBlendSpeed = .1f;
        public Icons[] AnimatonIcons;

        [System.Serializable]
        public class Icons
        {
            public string animationNameIdentfier;
            public Sprite iconTexture;
        }

        public AnimationCurve menuFadeInOutCurve;

        [Header("Character Name Text")]
        public float textFadeInSpeed = 1;
        public Gradient colorGradient;
        public TextMesh characterNameText;

        [Header("Environments")]
        public List<GameObject> environments = new List<GameObject>();
        public Icons[] environmentIcons;

        public string pathToCurrentFolder;

        public TMPro.TMP_InputField inputField;

        // Use this for initialization
        void Start()
        {
            textMaterial = characterNameText.GetComponent<MeshRenderer>().material;
            //Allow the menu to toggle in and out
            menuToggle.onValueChanged.AddListener((x) => StartCoroutine(SlideAway(menuFadeInOutCurve, mainMenuRectTransform, 1, x)));

            //Allow 
            meshToggle.onValueChanged.AddListener((x) => SwitchMenu(meshToggle, meshTileContentParent, meshText));
            animationToggle.onValueChanged.AddListener((x) => SwitchMenu(animationToggle, animationTileContentParent, animationText));
            environmentToggle.onValueChanged.AddListener((x) => SwitchMenu(environmentToggle, environmentTileContentParent, environmentText));

            SwitchMenu(meshToggle, meshTileContentParent, meshText);

            //Find all the skinned mesh renders under the rig
            animals = new Transform[animalParent.transform.childCount];
            int index = 0;
            foreach (Transform t in animalParent.transform)
            {
                animals[index] = t;
                index++;

            }

            //Generate some pictures of the faces of each skinned mesh renderer
            StartCoroutine(TakePictures(animals));

            inputField.onValueChanged.AddListener(FilterCharacters);
        }

        void FilterCharacters(string value)
        {
            if(value == "" || value == null)
            {
                foreach (Transform t in meshTileContentParent.transform)
                {
                    t.gameObject.SetActive(true);
                }
            }
            else
            {

                foreach (Transform t in meshTileContentParent.transform)
                {
                    if (!t.name.ToLower().Contains(value.ToLower()))
                    {
                        t.gameObject.SetActive(false);
                    }
                }
            }

        }

        IEnumerator SlideAway(AnimationCurve curve, RectTransform objectToMove, float duration, bool isForward)
        {
            float elapsedTime = 0;
            var endPos = isForward ? new Vector2(objectToMove.anchoredPosition.x + objectToMove.rect.width, objectToMove.anchoredPosition.y) :
                new Vector2(objectToMove.anchoredPosition.x - objectToMove.rect.width, objectToMove.anchoredPosition.y);

            while (elapsedTime < duration)
            {
                objectToMove.anchoredPosition = Vector2.LerpUnclamped(objectToMove.anchoredPosition, endPos, curve.Evaluate((elapsedTime / duration)));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            objectToMove.anchoredPosition = endPos;
        }

        IEnumerator FadeColor(Material material, float duration)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                material.SetColor("_Color", colorGradient.Evaluate((elapsedTime / duration)));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            material.SetColor("_Color", Color.clear);
        }

        void MenuToggle(Animator myMenu, Toggle toggle)
        {
            myMenu.SetBool("Toggle", toggle.isOn);
        }

        void SwitchMenu(Toggle toggle, GameObject contentToTurnOn, Text text)
        {
            if (toggle != meshToggle)
                meshToggle.isOn = false;
            if (toggle != animationToggle)
                animationToggle.isOn = false;
            if (toggle != environmentToggle)
                environmentToggle.isOn = false;

            meshTileContentParent.gameObject.SetActive(false);
            animationTileContentParent.gameObject.SetActive(false);
            environmentTileContentParent.gameObject.SetActive(false);
            meshText.enabled = false;
            animationText.enabled = false;
            environmentText.enabled = false;

            scrollRect.content = contentToTurnOn.GetComponent<RectTransform>();
            contentToTurnOn.gameObject.SetActive(true);
            text.enabled = true;
        }

        IEnumerator TakePictures(Transform[] animals)
        {
            //Choose a random character that will be enabled at the start
            var RandomStartCharacter = animals[Random.Range(0, animals.Length)];

            //We need to turn all the meshes off so we can get some pictures of the faces
            foreach (var mesh in animals)
            {
                mesh.gameObject.SetActive(false);
            }

            //Loop through all skinned Mesh Renderers
            foreach (var animalTransform in animals)
            {
                //An extra wait to ensure we don't accidently take a picture of two characters
                yield return new WaitForEndOfFrame();
                //Turn on the face
                animalTransform.gameObject.SetActive(true);
                SetCameraForAnimal(animalTransform);

                yield return new WaitForEndOfFrame();

                //Create a render texture from the face camera
                RenderTexture.active = faceCamera.targetTexture;
                //Store the texture so that it does not change mid way through this function
                var faceRenderTexture = faceCamera.targetTexture;

                //All done rendering so we can turn off the skinned Mesh now
                animalTransform.gameObject.SetActive(false);

                //Create a new texture2D
                var newTexture = new Texture2D(faceRenderTexture.width, faceRenderTexture.height);
                newTexture.ReadPixels(new Rect(0, 0, faceRenderTexture.width, faceRenderTexture.height), 0, 0);
                newTexture.Apply();

                //Create a sprite out of the texture2D
                Rect rec = new Rect(0, 0, newTexture.width, newTexture.height);
                var newSprite = Sprite.Create(newTexture, rec, new Vector2(0, 0), 1);
                newSprite.name = animalTransform.name;
                sprites.Add(newSprite);

                //Spawn some tiles for the faces and assign them with textures and the name of the character
                var newTile = Instantiate(contentTile, meshTileContentParent.transform);
                newTile.GetComponentsInChildren<Image>().First(x => x.name == "Face Sprite").sprite = newSprite;
                newTile.GetComponentInChildren<Text>().text = animalTransform.transform.name;
                newTile.GetComponent<Button>().onClick.AddListener(() => SelectTile(animalTransform));
                newTile.transform.name = animalTransform.transform.name;
            }

            //Manually set a character to start on
            previousCharacter = RandomStartCharacter;
            RandomStartCharacter.gameObject.SetActive(true);
            SetCameraForAnimal(RandomStartCharacter.transform);
            GetAnimationsForCurrentAnimal(previousCharacter.GetComponent<Animator>());
            SetEnvironments();
        }

        void SetCameraForAnimal(Transform animalTransform)
        {
            var skinnedMesh = animalTransform.GetComponentInChildren<SkinnedMeshRenderer>();
            var bounds = skinnedMesh.bounds;
            var magnitude = (bounds.size.magnitude * 1.2f);
            faceCamera.transform.position = bounds.center + Vector3.forward * magnitude + Vector3.right * magnitude;
            faceCamera.transform.LookAt(bounds.center);
            faceCamera.orthographicSize = (bounds.size / 2).magnitude;

            var mainCam = Camera.main;
            mainCam.transform.position = bounds.center + Vector3.forward * magnitude + Vector3.right * magnitude;
            mainCam.transform.LookAt(bounds.center);
            mainCam.orthographicSize = (bounds.size * .7f).magnitude;
        }

        Coroutine previous;
        Material textMaterial;

        void SelectTile(Transform animal)
        {
            if (previous != null)
                StopCoroutine(previous);

            if (previousCharacter != null)
            {
                previousCharacter.gameObject.SetActive(false);
            }

            animal.gameObject.SetActive(true);
            previousCharacter = animal;

            characterNameText.text = animal.name;
            previous = StartCoroutine(FadeColor(textMaterial, textFadeInSpeed));
            GetAnimationsForCurrentAnimal(animal.GetComponent<Animator>());
            SetCameraForAnimal(animal);
        }

        void GetAnimationsForCurrentAnimal(Animator currentAnimalAnimator)
        {
            if (currentAnimalAnimator == null) return;

            foreach (Transform child in animationTileContentParent.transform)
            {
                Destroy(child.gameObject);
            }

            var controller = currentAnimalAnimator.runtimeAnimatorController;
            var clips = controller.animationClips;
            
            #if UNITY_EDITOR
            AnimatorController animatorController = currentAnimalAnimator.runtimeAnimatorController as AnimatorController;

            if (animatorController != null)
            {
                //Make new buttons
                foreach (var parameter in animatorController.parameters)
                {
                    //Spawn some tiles for the faces and assign them with textures and the name of the character
                    var newTile = Instantiate(contentTile, animationTileContentParent.transform);
                    newTile.GetComponentInChildren<Text>().text = parameter.name;
                    newTile.GetComponent<Button>().onClick.AddListener(() => SelectAnimation(parameter.name, currentAnimalAnimator));

                    if (AnimatonIcons.Length > 0)
                    {
                        foreach (var icon in AnimatonIcons)
                        {
                            if (parameter.name.Contains(icon.animationNameIdentfier))
                            {
                                newTile.GetComponentsInChildren<Image>().First(x => x.name == "Face Sprite").sprite = icon.iconTexture;
                            }
                        }
                    }
                }
                currentAnimalAnimator.enabled = true;
            }
            #endif
        }

        void SetEnvironments()
        {
            if (environments.Count == 0) return;

            var randomStartEnvironment = environments[Random.Range(0, environments.Count)];

            foreach (var environment in environments)
            {
                environment.SetActive(false);

                //Spawn some tiles for the faces and assign them with textures and the name of the character
                var newTile = Instantiate(contentTile, environmentTileContentParent.transform);
                newTile.GetComponentInChildren<Text>().text = environment.name;
                newTile.GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (var item in environments)
                    {
                        item.SetActive(false);
                    }

                    environment.SetActive(!environment.activeSelf);
                });


                foreach (var icon in environmentIcons)
                {
                    if (environment.name.Contains(icon.animationNameIdentfier))
                    {
                        newTile.GetComponentsInChildren<Image>().First(x => x.name == "Face Sprite").sprite = icon.iconTexture;
                    }
                }
            }

            randomStartEnvironment.SetActive(true);
        }

        void SelectAnimation(string name, Animator animator)
        {
            #if UNITY_EDITOR
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

            if(animatorController != null)
            {
                foreach (var param in animatorController.parameters)
                {
                    animator.SetBool(param.name, false);
                }

                animator.SetBool(name, true);
            }
            #endif
        }
        
    }
}
