using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
   public GameObject DialogBox;

   public GameObject option1;
   public GameObject option2;
   public GameObject option3;
   public GameObject option4;
   public GameObject option5;
   public GameObject option6;

   public static DialogUI Instance;

   private DialogType? nextDialog = null;

   void Awake()
   {
     Instance = this;
     DialogBox.SetActive(false);
   }

   public void Show(DialogType first, DialogType second)
   {
     nextDialog = second;
     Show(first);
   }

   public void Show(DialogType dialog)
   {
     DialogBox.SetActive(true);

    option1.SetActive(false);
    option2.SetActive(false);
    option3.SetActive(false);
    option4.SetActive(false);
    option5.SetActive(false);
    option6.SetActive(false);

    switch (dialog)
    {
        case DialogType.Option1:
            option1.SetActive(true);
            break;

        case DialogType.Option2:
            option2.SetActive(true);
            break;

        case DialogType.Option3:
            option3.SetActive(true);
            break;

        case DialogType.Option4:
            option4.SetActive(true);
            break;

        case DialogType.Option5:
            option5.SetActive(true);
            break;

        case DialogType.Option6:
            option6.SetActive(true);
            break;
    }
   }

     public void Continue()
     {
          if (nextDialog.HasValue)
          {
               DialogType dialog = nextDialog.Value;
               nextDialog = null;
               Show(dialog);
          }
          else Hide();
     }

   public void Hide()
   {
     DialogBox.SetActive(false);
     MouseController.Instance.LockMouse();
     Time.timeScale = 1;
   }
}
