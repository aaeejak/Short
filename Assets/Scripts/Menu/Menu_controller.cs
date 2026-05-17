using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{
    public Button[] buttons; // Inspector에서 Start, Setting, Exit 순서로 할당
    private int currentIndex = 0;

    void Start()
    {
        if (buttons.Length > 0)
        {
            // 버튼 자체 네비게이션 비활성화 (StandaloneInputModule과 충돌 방지)
            foreach (Button btn in buttons)
            {
                Navigation nav = btn.navigation;
                nav.mode = Navigation.Mode.None;
                btn.navigation = nav;
            }

            currentIndex = 0;
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
        }
    }

    void Update()
    {
        if (buttons.Length == 0) return;

        // 마우스 호버 등으로 선택이 바뀌었을 때 currentIndex 동기화
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected != null && selected != buttons[currentIndex].gameObject)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].gameObject == selected)
                {
                    currentIndex = i;
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    public void NewGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}