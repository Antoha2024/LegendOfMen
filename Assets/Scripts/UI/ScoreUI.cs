using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Leopotam.EcsLite;
using Client;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private EcsWorld _world;
    private int _playerEntity = -1;
    private int _lastScore = -1;

    void Start()
    {
        SetupTextSettings();
        Debug.Log("ScoreUI: Инициализация завершена.");
    }

    void Update()
    {
        // 🔥 ИЩЕМ ECSStartup КАЖДЫЙ КАДР, ПОКА НЕ НАЙДЕМ
        if (_world == null)
        {
            FindEcsStartup();
            if (_world == null) return;
        }

        // 🔥 ИЩЕМ ИГРОКА КАЖДЫЙ КАДР, ПОКА НЕ НАЙДЕМ
        if (_playerEntity == -1)
        {
            FindPlayerEntity();
        }

        // Обновляем текст, если игрок найден
        if (_world != null && _playerEntity != -1)
        {
            var scorePool = _world.GetPool<ScoreComponent>();
            
            if (scorePool.Has(_playerEntity))
            {
                ref var score = ref scorePool.Get(_playerEntity);
                
                if (score.Value != _lastScore)
                {
                    _scoreText.text = $"Палок: {score.Value}";
                    _lastScore = score.Value;
                    Debug.Log($"ScoreUI: Текст обновлен до {score.Value}");
                }
            }
            else
            {
                if (_lastScore != 0)
                {
                    _scoreText.text = "Палок: 0";
                    _lastScore = 0;
                    Debug.Log("ScoreUI: У игрока нет ScoreComponent");
                }
            }
        }
        else
        {
            if (_scoreText != null && _lastScore != -999)
            {
                _scoreText.text = "Поиск...";
                _lastScore = -999;
            }
        }
    }

    private void FindEcsStartup()
    {
        var startup = FindAnyObjectByType<EcsStartup>();
        if (startup != null)
        {
            _world = startup.GetWorld();
            if (_world != null)
            {
                Debug.Log("ScoreUI: ✅ EcsStartup найден!");
            }
        }
    }

    private void FindPlayerEntity()
    {
        if (_world == null) return;
        
        var filter = _world.Filter<UnitCmp>().End();
        foreach (var entity in filter)
        {
            var scorePool = _world.GetPool<ScoreComponent>();
            if (scorePool.Has(entity))
            {
                _playerEntity = entity;
                Debug.Log($"ScoreUI: ✅ Найден игрок с сущностью {_playerEntity}!");
                
                ref var score = ref scorePool.Get(_playerEntity);
                Debug.Log($"ScoreUI: У игрока есть счет = {score.Value}");
                return;
            }
        }
    }

    private void SetupTextSettings()
    {
        if (_scoreText == null)
        {
            Debug.LogError("ScoreUI: Текст не назначен!");
            return;
        }

        _scoreText.enableAutoSizing = true;
        _scoreText.fontSizeMin = 12;
        _scoreText.fontSizeMax = 48;
        _scoreText.overflowMode = TextOverflowModes.Overflow;
        _scoreText.alignment = TextAlignmentOptions.Right;

        ContentSizeFitter fitter = _scoreText.GetComponent<ContentSizeFitter>();
        if (fitter != null)
        {
            DestroyImmediate(fitter);
        }
    }
}