using Base.Game.Signal;
using System;

namespace Base.Game.Data
{
    public class Player
    {
        private PlayerData _playerData;
        public int Coin 
        {
            get => _playerData.coin;
            set
            {
                if(value != _playerData.coin)
                {
                    _playerData.coin = value;
                    SignalBus<SignalPlayerDataChange, PlayerData>.Instance.Fire(_playerData);
                }
                SignalBus<SignalTotalCoinChange, int>.Instance.Fire(value);
            }
        }

        public Player(PlayerData playerData)
        {
            _playerData = playerData;
            Coin = _playerData.coin;
            Registration();
        }

        ~Player()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalAddCoin, int>.Instance.Register(OnCoinAdded);
        }

        private void UnRegistration()
        {
            SignalBus<SignalAddCoin, int>.Instance.UnRegister(OnCoinAdded);
        }

        private void OnCoinAdded(int obj)
        {
            Coin += obj;
        }

    }
}