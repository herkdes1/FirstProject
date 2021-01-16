namespace Base.UI
{
    using Base.Game.Manager;
    using Base.Game.Signal;
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TxtCoin : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            Registration();
        }

        private void Start()
        {
            _text.text = FileManager.Instance.Player.Coin.ToString();
        }

        private void OnDestroy()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalTotalCoinChange, int>.Instance.Register(OnTotalCoinChanged);
        }

        private void OnTotalCoinChanged(int obj)
        {
            _text.text = obj.ToString();
        }

        private void UnRegistration()
        {
            SignalBus<SignalTotalCoinChange, int>.Instance.UnRegister(OnTotalCoinChanged);
        }

    }
}