using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField] GameObject tooltip;
   private bool mouseOver;

   void Update()
   {
      if (mouseOver)
      {
         tooltip.SetActive(true);
      }
      else
      {
         tooltip.SetActive(false);
      }
   }

   public void OnPointerEnter(PointerEventData eventData)
   {
      print("mouse over");
      mouseOver = true;
   }

   public void OnPointerExit(PointerEventData eventData)
   {
      print("mouse gone");
      mouseOver = false;
   }
}
