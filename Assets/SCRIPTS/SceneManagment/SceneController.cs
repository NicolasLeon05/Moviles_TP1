using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public List<Level> levels;
    [SerializeField] private Level _bootLevel;

    private List<SceneRef> _loadedScenes = new();
    private List<SceneRef> _persistentLoadedScenes = new();

    private SceneRef _currentActiveScene;
    private SceneRef _previousActiveScene;
    public SceneRef CurrentActiveScene => _currentActiveScene;
    public SceneRef PreviousActiveScene => _previousActiveScene;

    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveBootScenes();
    }

    private void Start()
    {
        LoadLevel(levels[0]);
    }

    /// <summary>
    /// Loads the given level with loading screen and unloads all non-persistent scenes currently loaded.
    /// </summary>
    public void LoadLevel(Level level)
    {
        StartCoroutine(LoadLevelRoutine(level));
    }

    private IEnumerator LoadLevelRoutine(Level level)
    {
        int loadingSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        yield return SceneManager.LoadSceneAsync(loadingSceneIndex, LoadSceneMode.Additive);

        Scene loadingScene = SceneManager.GetSceneByBuildIndex(loadingSceneIndex);
        if (loadingScene.IsValid())
            SceneManager.SetActiveScene(loadingScene);

        foreach (var scene in _loadedScenes)
        {
            if (!scene.IsPersistent && !level.scenes.Contains(scene))
                yield return UnloadSceneByIndexRoutine(scene.Index);
        }

        foreach (var scene in level.scenes)
        {
            if (!_loadedScenes.Contains(scene) && !_persistentLoadedScenes.Contains(scene))
            {
                yield return LoadAdditiveByRefRoutine(scene);
                _loadedScenes.Add(scene);

                if (scene.IsPersistent)
                    _persistentLoadedScenes.Add(scene);
            }
        }

        yield return new WaitForSeconds(2f);

        yield return UnloadSceneByIndexRoutine(loadingSceneIndex);

        if (level.scenes.Count > 0)
            SetSceneActive(level.scenes[0].Index);
    }


    public void AddLevel(Level level)
    {
        foreach (var scene in level.scenes)
        {
            if (!_loadedScenes.Contains(scene) && !_persistentLoadedScenes.Contains(scene))
            {
                LoadAdditiveByRef(scene);
                _loadedScenes.Add(scene);

                if (scene.IsPersistent)
                    _persistentLoadedScenes.Add(scene);
            }
        }
    }

    public void UnloadAllScenes()
    {
        var scenesToUnload = new List<SceneRef>(_loadedScenes);

        foreach (var scene in scenesToUnload)
        {
            if (scene.Index != SceneManager.GetActiveScene().buildIndex)
                UnloadSceneByIndex(scene.Index);
        }

        _loadedScenes.Clear();
        _persistentLoadedScenes.Clear();
    }

    public void UnloadNonPersistentScenes()
    {
        foreach (var scene in _loadedScenes)
        {
            if (scene.IsActive)
            {
                SetSceneActive(scene.Index);
                break;
            }
        }

        foreach (var scene in _loadedScenes)
        {
            if (!scene.IsPersistent)
                UnloadSceneByIndex(scene.Index);
        }

        _loadedScenes.RemoveAll(scene => !scene.IsPersistent);
    }

    public void SaveBootScenes()
    {
        foreach (var scene in _bootLevel.scenes)
        {
            if (!_loadedScenes.Contains(scene) && !_persistentLoadedScenes.Contains(scene))
                _loadedScenes.Add(scene);

            if (scene.IsPersistent && !_persistentLoadedScenes.Contains(scene))
                _persistentLoadedScenes.Add(scene);
        }
    }

    public void LoadAdditiveByRef(SceneRef scene)
    {
        StartCoroutine(LoadAdditiveByRefRoutine(scene));
    }

    private IEnumerator LoadAdditiveByRefRoutine(SceneRef scene)
    {
        if (scene.Index < SceneManager.sceneCountInBuildSettings)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.Index, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
                yield return null;

            Scene newScene = SceneManager.GetSceneByBuildIndex(scene.Index);
            if (newScene.IsValid() && scene.IsActive)
                SetSceneActive(scene.Index);
        }
    }

    public void UnloadSceneByIndex(int index)
    {
        StartCoroutine(UnloadSceneByIndexRoutine(index));
    }

    private IEnumerator UnloadSceneByIndexRoutine(int index)
    {
        if (index < 0 || index >= SceneManager.sceneCountInBuildSettings)
            yield break;

        Scene scene = SceneManager.GetSceneByBuildIndex(index);
        if (!scene.isLoaded)
            yield break;

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(index);
        if (asyncUnload == null)
            yield break;

        while (!asyncUnload.isDone)
            yield return null;
    }

    public void SetSceneActive(int index)
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(index);
        if (scene.IsValid())
        {
            SceneManager.SetActiveScene(scene);

            SceneRef sceneRef = _loadedScenes.Find(s => s.Index == index);
            if (sceneRef != null)
            {
                _previousActiveScene = _currentActiveScene;
                _currentActiveScene = sceneRef;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
