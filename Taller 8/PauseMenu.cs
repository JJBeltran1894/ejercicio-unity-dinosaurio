using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   
    [Header("Referencia UI")]

    public GameObject panelPause;

    [Header("Estado Pausa")]
    
    public bool juegoPausado = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            if(juegoPausado){
                Reanudar();

            }else{
                Pausar();
            }
    }

    public void Reanudar(){
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void Pausar(){
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Reiniciar(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

        public void IrAlMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
