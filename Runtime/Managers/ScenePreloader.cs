using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityExtensions.Runtime.Managers
{
    public class ScenePreloader : MonoBehaviour
    {
        #region Attributes

        public static ScenePreloader instance { get; set; }

        [Header("UI")]
        public Slider progressBar;

        public Text progressLabel;

        public bool useFade = true;

        [SerializeField]
        private Image m_fadeImage;

        [Header("Parameters")]
        public bool autoLoad = true;

        public bool preloadInBackground;
        public bool usePlayerPrefs = true;
        public string sceneToLoad;

        private AsyncOperation m_asyncOperation;

        private Animator m_animator;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float progress
        {
            get { return progressBar.value; }
        }

        public bool processing { get; private set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            m_animator = m_fadeImage.gameObject.GetComponent<Animator>();

            if (autoLoad)
            {
                Load();
            }
        }

        #endregion Inherited Methods

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Load()
        {
            if (!processing)
            {
                if (preloadInBackground)
                {
                    // Good for loading in the background while the game is playing
                    Application.backgroundLoadingPriority = ThreadPriority.Low;
                }
                else
                {
                    // Good for fast loading when showing progress bars
                    Application.backgroundLoadingPriority = ThreadPriority.High;
                }

                StartCoroutine(Process_Coroutine());
            }
        }

        private IEnumerator Process_Coroutine()
        {
            processing = true;

            if (useFade)
            {
                yield return new WaitForSeconds(2f);
            }

            if (usePlayerPrefs)
            {
                sceneToLoad = PlayerPrefs.GetString("SceneToLoad", "MainMenu");

                m_asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            }
            else
            {
                m_asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            }

            m_asyncOperation.allowSceneActivation = false;

            // Wait until finished and get progress
            while (!m_asyncOperation.isDone)
            {
                progressBar.value = m_asyncOperation.progress;
                progressLabel.text = Mathf.RoundToInt(progress) * 100 + "%";

                if (progress >= 0.9f)
                {
                    // Almost done
                    progressBar.value = 1.0f;
                    progressLabel.text = Mathf.RoundToInt(progress) * 100 + "%";

                    if (useFade)
                    {
                        m_animator.SetTrigger("Fade Out");

                        yield return new WaitForSeconds(2f);
                    }

                    m_asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            yield return m_asyncOperation;
        }

        #endregion Other Methods

        #endregion Methods
    }
}