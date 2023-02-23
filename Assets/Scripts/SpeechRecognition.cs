using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class SpeechRecognition : MonoBehaviour
{   
    public UIController uiController;

    public GameController gameController;

    private PlayerController playerController;

    private KeywordRecognizer recognizeWord;

    private ConfidenceLevel confidence = ConfidenceLevel.Low;

    private Dictionary<string, Accion> KeywordAction = new Dictionary<string, Accion>();

    //crear Delegado para la acción a ejecutar
    private delegate void Accion();

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        KeywordAction.Add("correr", playerController.Run);
        KeywordAction.Add("parar", playerController.Stop);
        KeywordAction.Add("abajo", playerController.Bend);
        KeywordAction.Add("arriba",playerController.Rise);
        KeywordAction.Add("saltar", playerController.Jump);
        KeywordAction.Add("agachar", playerController.Bend);
        KeywordAction.Add("levantarse", playerController.Rise);
        KeywordAction.Add("continuar", uiController.HidenTutorial);
        KeywordAction.Add("tutorial", uiController.ShowTutorial);
        KeywordAction.Add("salir", gameController.Restart);
        KeywordAction.Add("reiniciar", gameController.Restart);
        recognizeWord = new KeywordRecognizer(KeywordAction.Keys.ToArray(), confidence);
        recognizeWord.OnPhraseRecognized += OnKeywordsRecognized;
        recognizeWord.Start();
    }

    void OnDestroy()
    {
        if (recognizeWord != null && recognizeWord.IsRunning)
        {
            recognizeWord.Stop();
            recognizeWord.Dispose();
        }
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction[args.text].Invoke();
    }
}
