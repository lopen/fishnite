using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Random=UnityEngine.Random;

public class DialogueGen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueObject;
   
    float waitTime = 0.08f;
    public string fullDialogue = "AMOGUS NEED FISH AAAA";
    private string currentChar = "";

    private TMP_Text textMesh;
    private Mesh mesh;
    private Vector3[] textVertices;
    private AudioSource speechAudio;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = dialogueText.GetComponent<TMP_Text>();
        speechAudio = dialogueText.GetComponent<AudioSource>();
        StartCoroutine(GenText());
    }

    void Update() {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        textVertices = mesh.vertices;
        for (int i = 0; i < textVertices.Length; i++) {
            Vector3 offset = BounceChar(Time.time + i);
            textVertices[i] = textVertices[i] + offset;
        }
        mesh.vertices = textVertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 BounceChar(float waitTime) {
        return new Vector2(0.0f, Mathf.Cos(20.0f*waitTime));
    }

    IEnumerator GenText() {
        for (int i = 0; i < fullDialogue.Length; i++) {
            currentChar = fullDialogue.Substring(0,i);
            dialogueText.text = currentChar;
            speechAudio.pitch = Random.Range(1.0f, 3.0f);
            speechAudio.Play();
            yield return new WaitForSeconds(waitTime);
            Destroy(dialogueObject, 3f);
        }
    }
}
