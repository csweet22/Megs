using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldNavigator : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            bool reverse = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            SelectNextField(reverse);
        }
    }

    void SelectNextField(bool reverse)
    {
        GameObject current = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if (current == null) return;

        Selectable currentSelectable = current.GetComponent<Selectable>();
        if (currentSelectable == null) return;

        Selectable nextSelectable =
            reverse ? currentSelectable.FindSelectableOnUp() : currentSelectable.FindSelectableOnDown();

        // If no vertical neighbors, try horizontal navigation
        if (nextSelectable == null){
            nextSelectable =
                reverse ? currentSelectable.FindSelectableOnLeft() : currentSelectable.FindSelectableOnRight();
        }

        if (nextSelectable != null){
            nextSelectable.Select();

            // For TMP_InputField, also activate the input field
            TMP_InputField tmpInputField = nextSelectable.GetComponent<TMP_InputField>();
            if (tmpInputField != null){
                tmpInputField.OnSelect(null);
            }
        }
    }
}