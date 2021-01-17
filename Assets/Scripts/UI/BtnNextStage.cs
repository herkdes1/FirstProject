namespace Base.UI
{
    using Base.Game.Signal;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class BtnNextStage : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SignalBus<SignalFade, bool>.Instance.Fire(true);
        }
    }
}