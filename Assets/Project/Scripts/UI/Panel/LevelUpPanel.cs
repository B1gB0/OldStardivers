using System;
using System.Collections.Generic;
using Project.Game.Scripts;
using Project.Scripts.Cards.ScriptableObjects;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.UI
{
    public class LevelUpPanel : MonoBehaviour, IView
    {
        private const String Damage = nameof(Damage);
        private const String RangeAttack = nameof(RangeAttack);
        private const String FireRate = nameof(FireRate);
        private const String BulletSpeed = nameof(BulletSpeed);
        
        private const String Gun = nameof(Gun);
        private const String Mines = nameof(Mines);
        
        private const int MinIndex = 0;
        private const int Remainder = 0;
        private const int Multiplicity = 5;

        private readonly Random _random = new ();

        [SerializeField] private List<CardView> _cardsView = new ();
        [SerializeField] private List<ImprovementCard> _improvementCards = new ();
        [SerializeField] private List<WeaponCard> _weaponCards = new ();

        private List<ImprovementCard> _currentImprovementCards;

        private PauseService _pauseService;
        private WeaponFactory _weaponFactory;
        private WeaponHolder _weaponHolder;

        private void OnEnable()
        {
            foreach (CardView cardView in _cardsView)
            {
                cardView.GetImprovementButtonClicked += OnButtonClicked;
            }
        }

        private void OnDisable()
        {
            foreach (CardView cardView in _cardsView)
            {
                cardView.GetImprovementButtonClicked -= OnButtonClicked;
            }
        }

        public void GetServices(PauseService pauseService, WeaponFactory weaponFactory, WeaponHolder weaponHolder)
        {
            _pauseService = pauseService;
            _weaponFactory = weaponFactory;
            _weaponHolder = weaponHolder;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnLevelUpgraded(int currentLevel)
        {
            if (currentLevel % Multiplicity == Remainder)
            {
                foreach (CardView cardView in _cardsView)
                {
                    cardView.GetCard(_weaponCards[_random.Next(MinIndex, _weaponCards.Count)]);
                }
            }
            else
            {
                foreach (ImprovementCard card in _improvementCards)
                {
                    foreach (Weapon weapon in _weaponHolder.Weapons)
                    {
                        if (card.TypeWeapon == weapon.name)
                        {
                            Debug.Log(weapon.name);
                            _currentImprovementCards.Add(card);
                        }
                    }
                }
                
                foreach (CardView cardView in _cardsView)
                {
                    cardView.GetCard(_currentImprovementCards[_random.Next(MinIndex, _currentImprovementCards.Count)]);
                }
            }

            Show();
            
            _pauseService.StopGame();
        }

        private void OnButtonClicked(Card card, CardView cardView)
        {
            //_weaponCards.Remove(card);

            if (card is ImprovementCard improvementCard)
            {
                // switch (improvementCard.TypeCharacteristics)
                // {
                //     case Damage
                //         
                // }
            }

            _pauseService.PlayGame();
            
            Hide();
        }
    }
}